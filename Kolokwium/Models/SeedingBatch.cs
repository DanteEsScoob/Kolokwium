using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Kolokwium.Models;

public class SeedingBatch
{
    [Key]
    public int BatchId { get; set; }
    [ForeignKey(nameof(Nursery))]
    public int NurseryId { get; set; }
    [ForeignKey(nameof(TreeSpecies))]
    public int SpeciesId { get; set; }
    public int Quantity { get; set; }
    public DateTime SownDate { get; set; }
  
    public DateTime? ReadyDate { get; set; }
    
}