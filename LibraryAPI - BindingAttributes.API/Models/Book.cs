using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI___Learning.Models;
public class Book
{

    [FromRoute(Name = "id")]           // هياخد قيمة من route parameter اسمه id
    public int Id { get; set; }

    [FromQuery]                       // هيجيبها من query string
    public string Category { get; set; }

    [FromHeader(Name = "X-User-Id")]  // هيجيبها من هيدر بالاسم دا
    public string UserId { get; set; }

    [FromBody]                       // هيجيبها من جسم الطلب (Body)
    public BookDetails Details { get; set; }
}
