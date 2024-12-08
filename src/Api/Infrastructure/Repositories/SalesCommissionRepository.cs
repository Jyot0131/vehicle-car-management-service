using Api.Domain.Dtos;
using Api.Domain.Entities;
using Api.Infrastructure.Persistent;
using Microsoft.EntityFrameworkCore;

namespace Api.Infrastructure.Repositories;

public class SalesCommissionRepository : ISalesCommissionRepository
{
    private readonly TestDbContext _dbContext;

    public SalesCommissionRepository(TestDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<CommissionDetailsDto>> GetCommissionBySalesmanIdAsync(int salesmanId)
    {
        var temp = from monthlySaleDetails in _dbContext.MonthlySaleDetails
            join salesmanDetails in _dbContext.SalesmanDetails on monthlySaleDetails.SalesmanId equals salesmanDetails.Id
            select new CommissionDetailsDto
            {
                SalesmanId = salesmanDetails.Id,
                SalesmanName = salesmanDetails.FirstName + " " + salesmanDetails.LastName,
                Class = monthlySaleDetails.Class,
                AudiSale = monthlySaleDetails.AudiSale,
                JaguarSale = monthlySaleDetails.JaguarSale,
                RenaultSale = monthlySaleDetails.RenaultSale,
                LandRoverSale = monthlySaleDetails.LandRoverSale,
                AnnualSaleAmount = salesmanDetails.AnnualSaleAmount
            };

        return await temp.Where(x => x.SalesmanId == salesmanId).ToListAsync();
    }

    public async Task<List<CommissionDetails>> GetCommissionDetailsAsync()
    {
        return await _dbContext.CommissionDetails.ToListAsync();
    }
}

public interface ISalesCommissionRepository
{
    Task<List<CommissionDetailsDto>> GetCommissionBySalesmanIdAsync(int salesmanId);
    Task<List<CommissionDetails>> GetCommissionDetailsAsync();
}