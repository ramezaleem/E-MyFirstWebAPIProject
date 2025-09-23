using Microsoft.AspNetCore.Mvc;
using ValidationDemo.Models;

namespace ValidationDemo.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private static readonly List<User> Users = new List<User>();

    // GET api/Users
    [HttpGet]
    public IActionResult GetAllUsers ()
    {
        return Ok(Users);
    }

    // GET api/Users/{id}
    [HttpGet("{id}")]
    public IActionResult GetUserById ( int id )
    {
        var user = Users.FirstOrDefault(u => u.Id == id);
        if (user == null)
            return NotFound();
        return Ok(user);
    }

    // POST api/Users
    [HttpPost]
    public IActionResult CreateUser ( [FromBody] User user )
    {
        // ModelState.IsValid بيراجع البيانات مع Data Annotations
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        // إعطاء Id تلقائي وزود المستخدم للقائمة
        user.Id = Users.Count + 1;
        Users.Add(user);

        // نرجع CREATED مع بيانات المستخدم ورابط جلب التفاصيل
        return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
    }


}
