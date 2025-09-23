using System.ComponentModel.DataAnnotations;

namespace AutoMapperComplexMappingDemo.Models;

public class Customer
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string FirstName { get; set; }

    [Required, MaxLength(100)]
    public string LastName { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string PhoneNumber { get; set; }

    public Address Address { get; set; }

    public List<Order> Orders { get; set; }
}
