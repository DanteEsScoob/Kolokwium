using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kolokwium.Models;

public class Responsible
{
    [Key]
    [ForeignKey(nameof(SeedingBatch))]
    public int BatchId { get; set; }
    [Key]
    [ForeignKey(nameof(Employee))]
    
    public int Employee{get;set;} 
    
    [MaxLength(100)]
    public string Role{get;set;} = null!;
}