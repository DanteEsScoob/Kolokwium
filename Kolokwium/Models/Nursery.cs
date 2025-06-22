using System.ComponentModel.DataAnnotations;

namespace Kolokwium.Models;

public class Nursery
{
    [Key]
    public int NurseryID {get; set;}
    [MaxLength(100)]
    public string Name { get; set; }
    public DateTime EstablishedDate { get; set; }
    
}