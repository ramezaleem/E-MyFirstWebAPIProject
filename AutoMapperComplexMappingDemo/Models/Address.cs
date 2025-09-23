using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoMapperComplexMappingDemo.Models;

public class Address
{

    [Key]
    public int Id { get; set; }

    [Required, MaxLength(200)]
    public string Street { get; set; }

    [Required, MaxLength(100)]
    public string City { get; set; }

    [Required, MaxLength(10)]
    public string ZipCode { get; set; }

    public int CustomerId { get; set; }

    [ForeignKey("CustomerId")]
    public Customer Customer { get; set; } // علاقة بـ Customer
}
