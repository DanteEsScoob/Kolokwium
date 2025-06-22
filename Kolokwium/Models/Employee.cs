using System.ComponentModel.DataAnnotations;

namespace Kolokwium.Models;

public class Employee
{
    [Key]
    public int EmployeeId { get; set; }
    [MaxLength(100)]
    public string FirstName { get; set; } = null!;
    [MaxLength(100)]
    public string LastName { get; set; } = null!;
    public DateTime HireDate { get; set; }
}