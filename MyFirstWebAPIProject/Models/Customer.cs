namespace MyFirstWebAPIProject.Models;

public class Customer
{
    public int CustomerId { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public List<Order> Orders { get; set; }
}
