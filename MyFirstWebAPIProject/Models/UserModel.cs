﻿namespace MyFirstWebAPIProject.Models;

public class UserModel
{
    public string Name { get; set; }
    public string Email { get; set; }
    public IFormFile ProfilePicture { get; set; }
}
