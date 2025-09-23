using Microsoft.AspNetCore.Mvc;
using MyFirstWebAPIProject.Models;

namespace MyFirstWebAPIProject.Controllers;
[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{

    [Route("{EmployeeName:alpha:minlength(5):maxlength(10)}")]
    [HttpGet]
    public string GetEmployeeDetails ( string EmployeeName )
    {
        return $"Employee {EmployeeName}";
    }

    [HttpGet("Name")]
    public string GetName ()
    {
        return "Return from GetName";
    }

    [HttpGet("Details")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Employee GetEmployee ()
    {
        return new Employee { Id = 1, Name = "Ahmed", Age = 30 };
    }

    [HttpGet("All")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IEnumerable<Employee> GetEmployees ()
    {
        return new List<Employee> {
        new Employee { Id=1, Name="Ahmed", Age=30 },
        new Employee { Id=2, Name="Saleh", Age=25 }
        };
    }


}