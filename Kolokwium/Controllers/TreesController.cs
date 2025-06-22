using System.Transactions;
using Kolokwium.Context;
using Kolokwium.DTOs;
using Kolokwium.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kolokwium.Controllers;
[Microsoft.AspNetCore.Components.Route("api/batches")]
[ApiController]
public class TreesController : ControllerBase
{
    private readonly TreeSchoolDbContext _context;

    public TreesController(TreeSchoolDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> PostTree([FromBody] POSTTreesDTO postTreesDto)
    {
        if(postTreesDto is not { Quantity: > 0 } || string.IsNullOrEmpty(postTreesDto.Species) || string.IsNullOrEmpty(postTreesDto.Nursery))
        {
            return BadRequest(new { Message = "Invalid data provided" });
        }
        if(postTreesDto.Nursery == null)
        {
            return BadRequest(new { Message = "Nursery or cannot be null" });
        }

        await using var species = await _context.Database.BeginTransactionAsync();
        try
        {
            var newSeedingBatch = new SeedingBatch
            {
                Quantity = postTreesDto.Quantity,
                SpeciesId =
                    _context.TreeSpecies.FirstOrDefault(ts => ts.LatinName == postTreesDto.Species)?.SpeciesId ?? 0,
                NurseryId = _context.Nurseries.FirstOrDefault(n => n.Name == postTreesDto.Nursery)?.NurseryID ?? 0,
                SownDate = DateTime.Now,
                ReadyDate = DateTime.Now.AddYears(10) // Assuming growth time is 10 years
            };
            _context.Add(newSeedingBatch);
            var newNursery = new Nursery
            {
                Name = postTreesDto.Nursery,
                EstablishedDate = DateTime.Now
            };
            var newTreeSpecies = new TreeSpecies
            {
                LatinName = postTreesDto.Species,
                GrowthTimeInYears = 10
            };
            _context.Add(newTreeSpecies);
            await _context.SaveChangesAsync();
            await species.CommitAsync();
            return CreatedAtAction(nameof(PostTree), new { id = newSeedingBatch.BatchId }, newSeedingBatch);
        }
        catch (Exception e)
        {
            await species.RollbackAsync();
            return StatusCode(500, e);
        }

    }
}