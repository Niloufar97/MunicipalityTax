using Microsoft.EntityFrameworkCore;
using MunicipalityTax.Data;
using MunicipalityTax.Models;
using MunicipalityTax.Repositories;
using MunicipalityTax.Services;
using Xunit;

public class TaxServiceTests
{
    private AppDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        var context = new AppDbContext(options);

        context.Municipalities.Add(new Municipality { Id = 1, MunicipalityName = "TestCity" });

        context.Taxes.AddRange(
            new Tax
            {
                Id = 1,
                MunicipalityId = 1,
                startDate = new DateOnly(2025, 5, 1),
                endDate = new DateOnly(2025, 5, 31),
                taxRate = 0.4m
            },
            new Tax
            {
                Id = 2,
                MunicipalityId = 1,
                startDate = new DateOnly(2025, 1, 1),
                endDate = new DateOnly(2025, 12, 31),
                taxRate = 0.2m
            },
            new Tax
            {
                Id = 3,
                MunicipalityId = 1,
                startDate = new DateOnly(2025, 1, 1),
                endDate = new DateOnly(2025, 1, 1),
                taxRate = 0.1m
            },
            new Tax
            {
                Id = 4,
                MunicipalityId = 1,
                startDate = new DateOnly(2025, 12, 31),
                endDate = new DateOnly(2025, 12, 31),
                taxRate = 0.1m
            }
        );

        context.SaveChanges();
        return context;
    }

    private TaxService GetTaxService(AppDbContext context)
    {
        var repository = new TaxRepository(context);
        return new TaxService(repository);
    }

    [Fact]
    public async Task GetTaxRate_Jan_ShouldReturn_0_1()
    {
        var context = GetDbContext();
        var service = GetTaxService(context);
        var date = new DateOnly(2025, 1, 1);

        var rate = await service.GetTaxRate("TestCity", date);

        Assert.Equal(0.1m, rate);
    }

    [Fact]
    public async Task GetTaxRate_Mar_ShouldReturn_0_2()
    {
        var context = GetDbContext();
        var service = GetTaxService(context);
        var date = new DateOnly(2025, 3, 16);

        var rate = await service.GetTaxRate("TestCity", date);

        Assert.Equal(0.2m, rate);
    }

    [Fact]
    public async Task GetTaxRate_May_ShouldReturn_0_4()
    {
        var context = GetDbContext();
        var service = GetTaxService(context);
        var date = new DateOnly(2025, 5, 2);

        var rate = await service.GetTaxRate("TestCity", date);

        Assert.Equal(0.4m, rate);
    }

    [Fact]
    public async Task GetTaxRate_Jul_ShouldReturn_0_2()
    {
        var context = GetDbContext();
        var service = GetTaxService(context);
        var date = new DateOnly(2025, 7, 10);

        var rate = await service.GetTaxRate("TestCity", date);

        Assert.Equal(0.2m, rate);
    }

    [Fact]
    public async Task GetTaxRate_ThrowsException_WhenNoTaxFound()
    {
        var context = GetDbContext();
        var service = GetTaxService(context);
        var date = new DateOnly(2026, 1, 1);

        await Assert.ThrowsAsync<Exception>(() => service.GetTaxRate("TestCity", date));
    }
}
