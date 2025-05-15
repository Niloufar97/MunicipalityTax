using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MunicipalityTax.Data;
using MunicipalityTax.Dtos;
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
        [HttpPost]
        public async Task<IActionResult> GetTaxRate([FromBody] TaxRequestDto request)
        {
            var taxes = await _context.Taxes
                                      .Include(t=> t.Municipality)
                                      .Where(t => t.Municipality.MunicipalityName == request.Municipality
                                                  && t.startDate <= request.Date
                                                  && t.endDate >= request.Date)                              
                                      .ToListAsync();
            if (!taxes.Any())
            {
                return NotFound("Tax rate not found for this inputs");
            }

            var taxWithHighPriority = taxes.OrderBy(t => (t.endDate.DayNumber - t.startDate.DayNumber)).First(); //shorter period has higher priority
            return Ok(taxWithHighPriority.taxRate);
        }
    }
}
