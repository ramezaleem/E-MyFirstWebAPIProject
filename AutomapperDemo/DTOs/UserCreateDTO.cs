using System.ComponentModel.DataAnnotations;

namespace AutomapperDemo.DTOs;

public class UserCreateDTO
{

    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required, EmailAddress]
    public string Email { get; set; }
    [Phone]
    public string PhoneNumber { get; set; }
    // These are primitive properties that will be mapped into a complex Address object.
    [Required]
    public string Street { get; set; }
    [Required]
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }

}
