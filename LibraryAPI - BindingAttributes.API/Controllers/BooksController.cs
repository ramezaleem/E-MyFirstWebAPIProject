using LibraryAPI___Learning.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI___Learning.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    [HttpPost]
    public IActionResult CreateBook ( Book request )
    {
        var response = new
        {
            BookId = request.Id,
            Category = request.Category,
            UserId = request.UserId,
            Title = request.Details.Title,
            Author = request.Details.Author,
            Year = request.Details.Year
        };

        return Ok(response);
    }

}
