using System.ComponentModel.DataAnnotations;

namespace AutomapperDemo.Models;

public class User
{

    public int UserId { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required, EmailAddress]
    public string Email { get; set; }
    [Phone]
    public string PhoneNumber { get; set; }
    // Complex type: Address object contains detailed address information.
    public Address Address { get; set; }


}
