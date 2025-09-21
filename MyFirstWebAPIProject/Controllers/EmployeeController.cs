using Microsoft.AspNetCore.Mvc;

namespace MyFirstWebAPIProject.Controllers;
[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    [HttpGet]
    [Route("api/[controller]/[action]")]
    public string Get ()
    {
        return "Returning from EmployeeController Get Method";
    }

    [HttpGet]
    [Route("api/[controller]/[action]")]
    public string GetEmployee ()
    {
        return "Returning from EmployeeController GetEmployee Method";
    }
}
