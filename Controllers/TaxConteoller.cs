using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MunicipalityTax.Data;
using MunicipalityTax.Dtos;
using MunicipalityTax.Models;

namespace MunicipalityTax.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaxController : ControllerBase
    {
        private readonly AppDbContext _context;
        public TaxController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetTaxRate([FromQuery] string municipality, [FromQuery] DateOnly date)
        {
            var taxes = await _context.Taxes
                                      .Include(t=> t.Municipality)
                                      .Where(t => t.Municipality.MunicipalityName == municipality
                                                  && t.startDate <= date
                                                  && t.endDate >= date)                              
                                      .ToListAsync();
            if (!taxes.Any())
            {
                return NotFound("Tax rate not found for this inputs");
            }

            var taxWithHighPriority = taxes.OrderBy(t => (t.endDate.DayNumber - t.startDate.DayNumber)).First(); //shorter period has higher priority
            return Ok(taxWithHighPriority.taxRate);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddTax([FromBody] AddTaxRequestDto request)
        {
            var newTaxRecord = new Tax
            {
                startDate = request.startDate,
                endDate = request.endDate,
                taxRate = request.taxRate,
                MunicipalityId = request.MunicipalityId,
            };
            _context.Taxes.Add(newTaxRecord);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetTaxRate), new { municipality = request.MunicipalityId, date = request.startDate }, newTaxRecord);
        }
    };

   

}
