using Microsoft.AspNetCore.Mvc;
using MyFirstWebAPIProject.Models;

namespace MyFirstWebAPIProject.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpPost]
    public IActionResult CreateUser ( [FromBody] UserModel user )
    {
        var response = new
        {
            Success = true,
            Message = $"User {user.Name} created successfully!",
            Code = StatusCodes.Status200OK
        };

        return Ok(response);
    }


    [HttpGet]
    public IActionResult GetResource ( [FromHeader(Name = "Custom-Header")] string customHeader )
    {
        if (customHeader == null)
        {
            return BadRequest("Custom-Header is Missing");
        }
        return Ok("Request Processed Successfully");
    }

}
