using ContentNegotiationDemo.DTOs;
using ContentNegotiationDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContentNegotiationDemo.Controllers;
[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private static readonly List<Employee> listEmployees = new List<Employee>()
    {
        new Employee(){ Id = 1, Name = "Anurag", Age = 28, Salary = 1000, Gender = "Male", Department = "IT" },
        new Employee(){ Id = 2, Name = "Pranaya", Age = 28, Salary = 2000, Gender = "Male", Department = "IT" },
    };

    [HttpGet]
    public ActionResult<List<Employee>> GetEmployees ()
    {
        var employeesDTO = listEmployees.Select(emp => new EmployeeDTO
        {
            Name = emp.Name,
            Age = emp.Age,
            Gender = emp.Gender,
            Department = emp.Department,
        }).ToList();

        return Ok(employeesDTO);
    }

    [HttpPost]
    public ActionResult<Employee> AddEmployee ( EmployeeDTO employeeDTO )
    {
        if (employeeDTO != null)
        {
            var newEmployee = new Employee()
            {
                Id = listEmployees.Count() + 1,
                Name = employeeDTO.Name,
                Salary = 3000,
                Age = employeeDTO.Age,
                Gender = employeeDTO.Gender,
                Department = employeeDTO.Department,
            };

            listEmployees.Add(newEmployee);
            return Ok(newEmployee);
        }
        return BadRequest();
    }

}
