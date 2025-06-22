using Kolokwium.DTOs;
using Kolokwium.Models;
using Microsoft.EntityFrameworkCore;

namespace Kolokwium.Context;

public class TreeSchoolDbContext : DbContext
{
    public TreeSchoolDbContext(DbContextOptions<TreeSchoolDbContext> options): base(options){}
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Nursery> Nurseries { get; set; }
    public DbSet<SeedingBatch> SeedingBatches { get; set; }
    public DbSet<Responsible> Responsibles { get; set; }
    public DbSet<TreeSpecies> TreeSpecies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Responsible>()
            .HasKey(r => new { r.BatchId, r.Employee });
        modelBuilder.Entity<SeedingBatch>().HasKey(sb => sb.BatchId);
        
        var  nursery = new List<Nursery>
        {
            new Nursery { NurseryID = 1, Name = "Green Haven", EstablishedDate = new DateTime(2000, 5, 20) },
            new Nursery { NurseryID = 2, Name = "Tree Top Nursery", EstablishedDate = new DateTime(2010, 3, 15) }
        };
        var treeSpecies = new List<TreeSpecies>
        {
            new TreeSpecies { SpeciesId = 1, LatinName = "Quercus robur", GrowthTimeInYears = 20 },
            new TreeSpecies { SpeciesId = 2, LatinName = "Pinus sylvestris", GrowthTimeInYears = 15 }
        };
        var seedingBatches = new List<SeedingBatch>
        {
            new SeedingBatch { BatchId = 1, NurseryId = 1, SpeciesId = 1, Quantity = 100, SownDate = new DateTime(2023, 4, 1), ReadyDate = new DateTime(2025, 4, 1) },
            new SeedingBatch { BatchId = 2, NurseryId = 2, SpeciesId = 2, Quantity = 200, SownDate = new DateTime(2023, 5, 1), ReadyDate = new DateTime(2025, 5, 1) }
        };
        var responsibles = new List<Responsible>
        {
            new Responsible { BatchId = 1, Employee = 1, Role = "Manager" },
            new Responsible { BatchId = 2, Employee = 2, Role = "Supervisor" }
        };
        var employees = new List<Employee>
        {
            new Employee { EmployeeId = 1, FirstName = "John", LastName = "Doe" },
            new Employee { EmployeeId = 2, FirstName = "Jane", LastName = "Smith" }
        };
        
        
        
        
        
    }
}   