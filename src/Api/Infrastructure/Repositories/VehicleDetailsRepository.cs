using System.Linq.Expressions;
using Api.Domain.Entities;
using Api.Infrastructure.Persistent;
using Microsoft.EntityFrameworkCore;

namespace Api.Infrastructure.Repositories;

public class VehicleDetailsRepository : IVehicleDetailRepository
{
    private readonly TestDbContext _dbContext;

    public VehicleDetailsRepository(TestDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<VehicleDetails> GetVehicleByIdAsync(int id)
    {
        return await _dbContext.VehicleDetails.FirstOrDefaultAsync(x => x.Id == id);
    }
    
    public async Task AddVehicleDetailsAsync(VehicleDetails vehicleDetails)
    {
        _dbContext.VehicleDetails.Add(vehicleDetails);
        await _dbContext.SaveChangesAsync();
    }

    public IQueryable<VehicleDetails> GetAsync(Expression<Func<VehicleDetails, bool>> predicate)
    {
        return _dbContext.VehicleDetails.Where(predicate);
    }
}

public interface IVehicleDetailRepository
{
    Task<VehicleDetails> GetVehicleByIdAsync(int id);
    Task AddVehicleDetailsAsync(VehicleDetails vehicleDetails);
    IQueryable<VehicleDetails> GetAsync(Expression<Func<VehicleDetails, bool>> predicate);
}