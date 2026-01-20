using EFTicketPortalLibrary.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using EFTicketPortalLibrary.Models;  // For TicketPortalDbContext

namespace EFTicketPortalLibrary.Repos;

public class EmployeeRepository : IEmployeeRepository
{
    TicketPortalDbContext context = new TicketPortalDbContext();

    public async Task AddEmployeeAsync(Employee employee)
    {
        try
        {
            await context.Employees.AddAsync(employee);
            await context.SaveChangesAsync();
        }
        catch(DbUpdateException ex)
        {
            SqlException sqlException = ex.InnerException as SqlException;
            int errorNumber = sqlException.Number;
            switch(errorNumber)
            {
                case 2627: 
                case 2601: throw new TicketException("Employee ID or Email already exists", 501);
                default: throw new TicketException(sqlException.Message, 599);
            }
        }
    }

    public async Task DeleteEmployeeAsync(string employeeId)
    {
        Employee emp2del = await context.Employees
            .Include("CreatedTickets")
            .Include("AssignedTickets")
            .Include("CreatedEmpReplies")
            .Include("AssignedEmpReplies")
            .FirstOrDefaultAsync(emp => emp.EmployeeId == employeeId);
        
        if(emp2del == null)
            throw new TicketException("No such employee ID", 502);
        
        if (emp2del.CreatedTickets.Count == 0 && 
            emp2del.AssignedTickets.Count == 0 && 
            emp2del.CreatedEmpReplies.Count == 0 && 
            emp2del.AssignedEmpReplies.Count == 0)
        {
            context.Employees.Remove(emp2del);
            await context.SaveChangesAsync();
        }
        else
        {
            throw new TicketException("Cannot delete employee with associated tickets or replies", 503);
        }
    }

    public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
    {
        List<Employee> employees = await context.Employees.ToListAsync();
        return employees;
    }

    public async Task<Employee> GetEmployeeAsync(string employeeId)
    {
        try
        {
            Employee employee = await (from e in context.Employees 
                                     where e.EmployeeId == employeeId 
                                     select e).FirstAsync();
            return employee;
        }
        catch
        {
            throw new TicketException("No such employee ID", 502);
        }
    }

    public async Task UpdateEmployeeAsync(string employeeId, Employee employee)
    {
        Employee emp2edit = await GetEmployeeAsync(employeeId);
        try
        {
            emp2edit.EmployeeName = employee.EmployeeName;
            emp2edit.Email = employee.Email;
            emp2edit.Password = employee.Password;
            emp2edit.Role = employee.Role;
            await context.SaveChangesAsync();
        }
        catch(DbUpdateException ex)
        {
            SqlException sqlException = ex.InnerException as SqlException;
            throw new TicketException(sqlException.Message, 599);
        }
    }
}
