using Microsoft.AspNetCore.Mvc;
using MyFirstWebAPIProject.Models;

namespace MyFirstWebAPIProject.Controllers;
[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    [HttpPost]
    public ActionResult<Order> CreateOrder ( [FromBody] Order order )
    {
        return Ok(order);
    }

}
