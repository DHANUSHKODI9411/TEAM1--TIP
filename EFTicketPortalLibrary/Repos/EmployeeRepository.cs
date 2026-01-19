using EFTicketPortalLibrary.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EFTicketPortalLibrary.Repos
{
    public class EmployeeRepository : IEmployeeRepository
    {
        TicketPortalDbContext context = new TicketPortalDbContext();

        // ADD EMPLOYEE
        public async Task AddEmployeeAsync(Employee employee)
        {
            try
            {
                await context.Employees.AddAsync(employee);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                SqlException sqlException = ex.InnerException as SqlException;
                int errorNumber = sqlException.Number;

                switch (errorNumber)
                {
                    case 2627:
                    case 2601:
                        throw new TicketException("Employee ID or Email already exists", 501);

                    default:
                        throw new TicketException(sqlException.Message, 599);
                }
            }
        }

        // DELETE EMPLOYEE
        public async Task DeleteEmployeeAsync(string employeeId)
        {
            Employee emp2del = await context.Employees
                .Include("CreatedTickets")
                .Include("AssignedTickets")
                .FirstOrDefaultAsync(e => e.EmployeeId == employeeId);

            if (emp2del == null)
                throw new TicketException("No such Employee ID", 502);

            if (emp2del.CreatedTickets.Count == 0 && emp2del.AssignedTickets.Count == 0)
            {
                context.Employees.Remove(emp2del);
                await context.SaveChangesAsync();
            }
            else
            {
                throw new TicketException("Cannot delete employee with active tickets", 503);
            }
        }

        // GET ALL EMPLOYEES
        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            List<Employee> employees = await context.Employees.ToListAsync();
            return employees;
        }

        // GET EMPLOYEE BY ID
        public async Task<Employee> GetEmployeeByIdAsync(string employeeId)
        {
            try
            {
                Employee emp = await context.Employees
                    .FirstAsync(e => e.EmployeeId == employeeId);

                return emp;
            }
            catch (InvalidOperationException)
            {
                throw new TicketException("No such Employee ID", 502);
            }
        }

        // UPDATE EMPLOYEE
        public async Task UpdateEmployeeAsync(Employee employee)
        {
            Employee emp2edit = await GetEmployeeByIdAsync(employee.EmployeeId);

            try
            {
                emp2edit.EmployeeName = employee.EmployeeName;
                emp2edit.Email = employee.Email;
                emp2edit.Password = employee.Password;
                emp2edit.Role = employee.Role;

                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                SqlException sqlException = ex.InnerException as SqlException;
                throw new TicketException(sqlException.Message, 599);
            }
        }
    }
}
