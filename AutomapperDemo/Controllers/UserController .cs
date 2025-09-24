using AutoMapper;
using AutomapperDemo.Data;
using AutomapperDemo.DTOs;
using AutomapperDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutomapperDemo.Controllers;

public class UserController : ControllerBase
{

    private readonly IMapper _mapper;
    private readonly UserDbContext _context;
    public UserController ( IMapper mapper, UserDbContext context )
    {
        _mapper = mapper;
        _context = context;
    }
    // GET: api/user/GetUsers
    [HttpGet("GetUsers")]
    public async Task<ActionResult<List<UserDTO>>> GetUsers ()
    {
        List<User> users = await _context.Users
            .AsNoTracking()
            .Include(u => u.Address)
            .ToListAsync();
        if (users == null || users.Count == 0)
        {
            return NotFound("No users found.");
        }
        var userDTOs = _mapper.Map<List<UserDTO>>(users);
        return Ok(userDTOs);
    }
    // GET: api/user/GetUserById/{id}
    [HttpGet("GetUserById/{id}")]
    public async Task<ActionResult<UserDTO>> GetUserById ( int id )
    {
        var user = await _context.Users
            .AsNoTracking()
            .Include(u => u.Address)
            .FirstOrDefaultAsync(u => u.UserId == id);
        if (user == null)
        {
            return NotFound($"User with ID {id} not found.");
        }
        // Map User entity to UserDTO
        var userDTO = _mapper.Map<UserDTO>(user);
        return Ok(userDTO);
    }
    // POST: api/user/CreateUser
    [HttpPost("CreateUser")]
    public async Task<ActionResult<UserDTO>> CreateUser ( [FromBody] UserCreateDTO userCreateDTO )
    {
        User user = _mapper.Map<User>(userCreateDTO);
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        var userDTO = _mapper.Map<UserDTO>(user);
        return Ok(userDTO);
    }
}

