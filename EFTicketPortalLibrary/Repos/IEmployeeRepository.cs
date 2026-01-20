using System;
using EFTicketPortalLibrary.Models;

namespace EFTicketPortalLibrary.Repos;

public interface IEmployeeRepository
{
    Task AddEmployeeAsync(Employee employee);
    Task UpdateEmployeeAsync(string employeeId, Employee employee);
    Task DeleteEmployeeAsync(string employeeId);
    Task<Employee> GetEmployeeAsync(string employeeId);
    Task<IEnumerable<Employee>> GetAllEmployeesAsync();
}
