using Microsoft.EntityFrameworkCore;
using MunicipalityTax.Data;
using MunicipalityTax.Dtos;
using MunicipalityTax.Models;

namespace MunicipalityTax.Services
{

    public interface ITaxService
    {
        Task<decimal> GetTaxRate(string municipality, DateOnly date);
        Task AddTaxRecord(AddTaxRequestDto request);
    }
    public class TaxService : ITaxService
    {
        // Inject AppDbContext via dependency injection
        private readonly AppDbContext _context;
        public TaxService(AppDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Gets the applicable tax rate for a given municipality on a specific date.
        /// </summary>
        public async Task<decimal> GetTaxRate(string municipality, DateOnly date)
        {
            var taxes = await _context.Taxes
                                      .Include(t => t.Municipality)
                                      .Where(t => t.Municipality.MunicipalityName == municipality
                                                  && t.startDate <= date
                                                  && t.endDate >= date)
                                      .ToListAsync();
            if (!taxes.Any())
            {
                throw new Exception("No tax records found for the specified municipality and date.");
            }

            var taxWithHighPriority = taxes.OrderBy(t => t.endDate.DayNumber - t.startDate.DayNumber).First(); //shorter period has higher priority
            return taxWithHighPriority.taxRate;
        }
        /// <summary>
        /// Add a new tax record to the database.
        /// </summary>
        public async Task AddTaxRecord(AddTaxRequestDto request)
        {
            // Check if the tax record exists
            if (await TaxRecordExists(request))
            {
                throw new Exception("Tax record already exists for the specified municipality and date range.");
            }
            if (!await ValidateInputs(request))
            {
                throw new Exception("Inputs are incorrect!");
            }
            var newTaxRecord = new Tax
            {
                startDate = request.startDate,
                endDate = request.endDate,
                taxRate = request.taxRate,
                MunicipalityId = request.MunicipalityId,
            };
            _context.Taxes.Add(newTaxRecord);
            await _context.SaveChangesAsync();
        }

        private async Task<bool> TaxRecordExists(AddTaxRequestDto request)
        {
            return await _context.Taxes.AnyAsync(t => t.MunicipalityId == request.MunicipalityId && t.startDate == request.startDate && t.endDate == request.endDate);
        }
        // validate inputs for add a new tax record. can improved by returning more accureate error messages
        private async Task<bool> ValidateInputs(AddTaxRequestDto request)
        {
            //Check dates
            if (request.endDate < request.startDate)
                return false;
            //Check tax rate
            if (request.taxRate > 1 || request.taxRate < 0)
                return false;
            if (!await _context.Municipalities.AnyAsync(m => m.Id == request.MunicipalityId))
                return false;
            return true;
        }

    }
}
