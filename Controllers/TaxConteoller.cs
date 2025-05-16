using Microsoft.AspNetCore.Mvc;
using MunicipalityTax.Data;
using MunicipalityTax.Dtos;
using MunicipalityTax.Services;

namespace MunicipalityTax.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaxController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ITaxService _taxService;
        public TaxController(AppDbContext context, ITaxService taxService)
        {
            _context = context;
            _taxService = taxService;
        }
        //get tax rate for a specific municipality and date
        //the shorter date range has higher priority
        [HttpGet]
        public async Task<IActionResult> GetTaxRate([FromQuery] string municipality, [FromQuery] DateOnly date)
        {
            try
            {
                var taxRate = await _taxService.GetTaxRate(municipality, date);
                return Ok(taxRate);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        //add a new tax record
        [HttpPost("add")]
        public async Task<IActionResult> AddTaxRecord([FromBody] AddTaxRequestDto request)
        {
            try
            {
                await _taxService.AddTaxRecord(request);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
    };
}
