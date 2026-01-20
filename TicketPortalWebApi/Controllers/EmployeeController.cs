using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EFTicketPortalLibrary.Models;
using EFTicketPortalLibrary.Repos;
using Microsoft.AspNetCore.Authorization;

namespace TicketPortalWebApi.Controllers;
    
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeRepository empRepo;

    public EmployeeController(IEmployeeRepository empRepository)
    {
        empRepo = empRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        List<Employee> employees = await empRepo.GetAllEmployeesAsync();
        return Ok(employees);
    }
    [HttpGet("{employeeId}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult> GetOne(string employeeId)
    {
        try
        {
            Employee employee = await empRepo.GetEmployeeAsync(employeeId);
            return Ok(employee);
        }
        catch(TicketException ex)
        {
            return NotFound(ex.Message);
        }
    }
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult> Add(Employee employee)
    {
        try
        {
            await empRepo.AddEmployeeAsync(employee);
            return Created($"api/employee/{employee.EmployeeId}",employee);
        }
        catch(TicketException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpDelete("{employeeId}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult> Delete(string employeeId)
    {
        try
        {
            await empRepo.DeleteEmployeeAsync(employeeId);
            return Ok();
        }
        catch(TicketException ex)
        {
            if (ex.ErrorNumber == 502)
            return NotFound(ex.Message);
            else
            return BadRequest(ex.Message);
        } 
    }
    [HttpPut("{employeeId}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]

    public async Task<ActionResult> Update(string employeeId, Employee employee)
    {
        try
        {
            await empRepo.UpdateEmployeeAsync(employeeId, employee);
            return Ok(employee);
        }
        catch(TicketException ex)
        {
            if(ex.ErrorNumber == 502)
            return NotFound(ex.Message);
            else
            return BadRequest(ex.Message);
        }
    }
    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] dynamic login)
    {
        string employeeId = login.employeeId;
        string password = login.password;
        try
        {
            var employee = await empRepo.LoginAsync(employeeId, password);
            return Ok(employee);
        }
        catch (TicketException ex)
        {
            if (ex.ErrorNumber == 502) 
            return NotFound(ex.Message);
            if (ex.ErrorNumber == 504) 
            return BadRequest(ex.Message);
            else
            return BadRequest(ex.Message);
        }
    }
}
