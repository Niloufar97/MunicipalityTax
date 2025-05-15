using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MunicipalityTax.Data;
using MunicipalityTax.Models;

namespace MunicipalityTax.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MunicipalityController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MunicipalityController(AppDbContext context)
        {
            _context = context;
        }

        // Get all Municipality
        [HttpGet]
        public async Task<IActionResult> GetAllMunicipality()
        {
            var municipalites = await _context.Municipalities
                .Include(m => m.Taxes)
                .ToListAsync();
            return Ok(municipalites);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddMunicipality([FromBody] Municipality municipality) //it is better to add a Dto for request
        {
           if(string.IsNullOrEmpty(municipality.MunicipalityName))
            {
                return BadRequest("Municipality name is required");
            }
            if (await _context.Municipalities.AnyAsync(m => m.MunicipalityName == municipality.MunicipalityName))
            {
                return BadRequest("Municipality already exists");
            }
            await _context.Municipalities.AddAsync(municipality);
            await _context.SaveChangesAsync();

            return Created();
        }
    }
}
