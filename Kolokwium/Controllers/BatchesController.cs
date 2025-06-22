using Kolokwium.Context;
using Kolokwium.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Kolokwium.Models;
//using Kolokwium.DTOs;

namespace Kolokwium.Controllers;

[Route("api/[controller]/{id}/batches")]
[ApiController]
public class BatchesController : ControllerBase
{
    private readonly TreeSchoolDbContext _context;

    public BatchesController(TreeSchoolDbContext context)
    {
        _context = context;
    }
[HttpGet]
    public async Task<IActionResult> GetBatches(int id)
    {
        var nursery = await _context.SeedingBatches
            .Include(n => n.BatchId)
            .FirstOrDefaultAsync(n => n.NurseryId == id);
        if (nursery == null)
        {
            return NotFound(new { Message = "Nursery not found" });
        }

        {
            var response = new BatchDto
            {
                BatchId = nursery.BatchId,
                Quantity = nursery.Quantity,
                SownDate = nursery.SownDate,
                ReadyDate = nursery.ReadyDate,
                Species = new SpeciesDto
                {
                    LatinName = _context.TreeSpecies.FirstOrDefault(ts => ts.SpeciesId == nursery.SpeciesId)!.LatinName,
                    GrowthTimeInYears = _context.TreeSpecies.FirstOrDefault(ts => ts.SpeciesId == nursery.SpeciesId)!
                        .GrowthTimeInYears
                },

                Responsible = await _context.Responsibles
                    .Where(r => r.BatchId == nursery.BatchId)
                    .Select(r => new ResponsibleDto
                    {
                        FirstName = _context.Employees.FirstOrDefault(e => e.EmployeeId == r.Employee)!.FirstName,
                        LastName = _context.Employees.FirstOrDefault(e => e.EmployeeId == r.Employee)!.LastName,
                        Role = r.Role
                    }).ToListAsync()
            };
            return Ok(response);

        }
    }
}