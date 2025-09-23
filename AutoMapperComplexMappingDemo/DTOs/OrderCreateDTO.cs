using System.ComponentModel.DataAnnotations;

namespace AutoMapperComplexMappingDemo.DTOs;

public class OrderCreateDTO
{

    [Required]
    public int CustomerId { get; set; }
    [Required]
    public List<OrderItemCreateDTO> Items { get; set; }
}
