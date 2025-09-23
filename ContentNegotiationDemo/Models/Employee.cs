namespace ContentNegotiationDemo.Models;

public class Employee
{
    public int Id { get; set; }  // خاصية حساسة
    public string Name { get; set; }
    public string Gender { get; set; }
    public int Age { get; set; }

    public int Salary { get; set; }  // خاصية حساسة
    public string Department { get; set; }
}
