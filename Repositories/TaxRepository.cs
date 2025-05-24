using Microsoft.EntityFrameworkCore;
using MunicipalityTax.Data;
using MunicipalityTax.Dtos;
using MunicipalityTax.Models;


namespace MunicipalityTax.Repositories
{
    public interface ITaxRepository
    {
        Task<List<Tax>> AllTaxesWithInputs(String municipality, DateOnly date);
        Task AddTaxRecord(Tax tax);
        Task<bool> TaxRecordExists(AddTaxRequestDto request);
        Task<bool> MunicipalityExists(int municipalityId);
    }
    public class TaxRepository : ITaxRepository
    {
        private readonly AppDbContext _context;
        public TaxRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Tax>> AllTaxesWithInputs(String municipality, DateOnly date)
        {
            return await _context.Taxes
                .Include(t => t.Municipality)
                .Where(t => t.Municipality.MunicipalityName == municipality
                            && t.startDate <= date
                            && t.endDate >= date)
                .ToListAsync();
        }

        public async Task AddTaxRecord(Tax tax)
        {
            _context.Taxes.Add(tax);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> TaxRecordExists(AddTaxRequestDto request)
        {
            return await _context.Taxes.AnyAsync(t => t.MunicipalityId == request.MunicipalityId
                                                       && t.startDate == request.startDate
                                                       && t.endDate == request.endDate);
        }

        public async Task<bool> MunicipalityExists(int municipalityId)
        {
            return await _context.Municipalities.AnyAsync(m => m.Id == municipalityId);
        }
    }
}
