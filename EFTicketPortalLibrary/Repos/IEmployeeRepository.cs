using System;
using EFTicketPortalLibrary.Models;

namespace EFTicketPortalLibrary.Repos;

public interface IEmployeeRepository
{
    Task AddEmployeeAsync(Employee employee);
        Task UpdateEmployeeAsync(Employee employee);
        Task DeleteEmployeeAsync(string employeeId);

        Task<Employee> GetEmployeeByIdAsync(string employeeId);
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
}
