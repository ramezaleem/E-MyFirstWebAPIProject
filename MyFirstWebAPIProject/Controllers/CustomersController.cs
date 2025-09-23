using Microsoft.AspNetCore.Mvc;
using MyFirstWebAPIProject.Models;

namespace MyFirstWebAPIProject.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    [HttpPost]
    public ActionResult<Customer> CreateCustomer ( [FromBody] Customer customer )
    {
        return Ok(customer);
    }


}
