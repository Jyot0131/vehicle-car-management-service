using System.Linq.Expressions;
using Api.Domain.Dtos;
using Api.Domain.Entities;
using Api.Domain.RequestMessages;
using Api.Domain.ResponseMessages;
using Api.Extensions;
using Api.Infrastructure.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Api.Services;

public class VehicleOperationsService : IVehicleOperationsService
{
    private readonly IVehicleDetailRepository _vehicleDetailRepository;
    private readonly ISalesCommissionRepository _salesCommissionRepository;
    private readonly IMapper _mapper;
    
    public VehicleOperationsService(IVehicleDetailRepository vehicleDetailRepository, IMapper mapper, ISalesCommissionRepository salesCommissionRepository)
    {
        _vehicleDetailRepository = vehicleDetailRepository;
        _salesCommissionRepository = salesCommissionRepository;
        _mapper = mapper;
    }
    
    public async Task<GetVehicleDetailResponseMessage> GetVehicleDetailByIdAsync(int id)
    {
        return _mapper.Map<GetVehicleDetailResponseMessage>(await _vehicleDetailRepository.GetVehicleByIdAsync(id));
    }

    public async Task<int> AddVehicleDetailsAsync(CreateVehicleRequestMessage requestMessage)
    {
        VehicleDetails vehicleDetails = _mapper.Map<VehicleDetails>(requestMessage);
        await _vehicleDetailRepository.AddVehicleDetailsAsync(vehicleDetails);
        return vehicleDetails.Id;
    }
    
    public async Task<List<VehicleDetails>> GetVehiclesDetailsAsync(ListAllVehicleRequestMessage requestMessage)
    {
        var result = _vehicleDetailRepository.GetAsync(x => x.Active);
        
        if (!requestMessage.ModelName.IsEmpty())
        {
            result = result.Where(x => x.ModelName == requestMessage.ModelName);
        }

        if (!requestMessage.ModelCode.IsEmpty())
        {
            result = result.Where(x => x.ModelCode == requestMessage.ModelCode);
        }

        if (!requestMessage.OrderBy.IsEmpty() && "asc".Equals(requestMessage.SortOrder))
        {
            result = result.OrderBy(x => x.ManufacturingDate);
            result = result.OrderBy(x => x.SortOrder);
        }
        else if (!requestMessage.OrderBy.IsEmpty() && "desc".Equals(requestMessage.SortOrder))
        {
            result = result.OrderByDescending(x => x.ManufacturingDate);
            result = result.OrderByDescending(x => x.SortOrder);
        }
        else
        {
            result = result.OrderByDescending(x => x.SortOrder);
        }

        return await result.AsNoTracking().ToListAsync();
    }

    public async Task<GetCommissionDetailsResponseMessage> GetCommissionDetailsBySalesmanIdAsync(int salesmanId)
    {
        List<VehicleDetails> vehicleDetails = await _vehicleDetailRepository.GetAsync(x => true).ToListAsync();

        var classAVehicles = vehicleDetails.Where(x => x.Class == "A-Class");
        var classBVehicles = vehicleDetails.Where(x => x.Class == "B-Class");
        var classCVehicles = vehicleDetails.Where(x => x.Class == "C-Class");
        
        List<CommissionDetails> commissionModel = await _salesCommissionRepository.GetCommissionDetailsAsync();

        List<CommissionDetailsDto> commissionDetailsDtos = await _salesCommissionRepository.GetCommissionBySalesmanIdAsync(salesmanId);

        Console.WriteLine("AnnualSaleAmount " + commissionDetailsDtos.FirstOrDefault().AnnualSaleAmount);

        return new GetCommissionDetailsResponseMessage
        {
            ClassWiseAdditionalCommission = new AdditionalCommission
            {
                ClassACommission = classAVehicles.Where(x => x.Brand == "Audi").Sum(x => x.Price) * commissionModel.FirstOrDefault(x => x.Brand == "Audi").AclassCommission / 100
                                   + classAVehicles.Where(x => x.Brand == "Jaguar").Sum(x => x.Price) * commissionModel.FirstOrDefault(x => x.Brand == "Jaguar").AclassCommission / 100
                                   + classAVehicles.Where(x => x.Brand == "Land rover").Sum(x => x.Price) * commissionModel.FirstOrDefault(x => x.Brand == "Land rover").AclassCommission / 100
                                   + classAVehicles.Where(x => x.Brand == "Renault").Sum(x => x.Price) * commissionModel.FirstOrDefault(x => x.Brand == "Renault").AclassCommission / 100,
                
                ClassBCommission = classBVehicles.Where(x => x.Brand == "Audi").Sum(x => x.Price) * commissionModel.FirstOrDefault(x => x.Brand == "Audi").BclassCommission / 100
                                   + classBVehicles.Where(x => x.Brand == "Jaguar").Sum(x => x.Price) * commissionModel.FirstOrDefault(x => x.Brand == "Jaguar").BclassCommission / 100
                                   + classBVehicles.Where(x => x.Brand == "Land rover").Sum(x => x.Price) * commissionModel.FirstOrDefault(x => x.Brand == "Land rover").BclassCommission / 100
                                   + classBVehicles.Where(x => x.Brand == "Renault").Sum(x => x.Price) * commissionModel.FirstOrDefault(x => x.Brand == "Renault").BclassCommission / 100,
                
                ClassCCommission = classCVehicles.Where(x => x.Brand == "Audi").Sum(x => x.Price) * commissionModel.FirstOrDefault(x => x.Brand == "Audi").CclassCommission / 100
                                   + classAVehicles.Where(x => x.Brand == "Jaguar").Sum(x => x.Price) * commissionModel.FirstOrDefault(x => x.Brand == "Jaguar").CclassCommission / 100
                                   + classCVehicles.Where(x => x.Brand == "Land rover").Sum(x => x.Price) * commissionModel.FirstOrDefault(x => x.Brand == "Land rover").CclassCommission / 100
                                   + classCVehicles.Where(x => x.Brand == "Renault").Sum(x => x.Price) * commissionModel.FirstOrDefault(x => x.Brand == "Renault").CclassCommission / 100,
            },
            BrandWiseFixedCommission = new FixedCommission
            {
                AudiCommission = commissionModel.FirstOrDefault(x => x.Brand == "Audi").FixedCommission * commissionDetailsDtos.Sum(x => x.AudiSale) / 100,
                JaguarCommission = commissionModel.FirstOrDefault(x => x.Brand == "Jaguar").FixedCommission * commissionDetailsDtos.Sum(x => x.JaguarSale) / 100,
                RenaultCommission = commissionModel.FirstOrDefault(x => x.Brand == "Renault").FixedCommission * commissionDetailsDtos.Sum(x => x.RenaultSale) / 100,
                LandRoverCommission = commissionModel.FirstOrDefault(x => x.Brand == "Land rover").FixedCommission * commissionDetailsDtos.Sum(x => x.LandRoverSale) / 100
            },
            ProfitWiseAdditionalCommission = commissionDetailsDtos.FirstOrDefault().AnnualSaleAmount > 500000 ? commissionDetailsDtos.FirstOrDefault().AnnualSaleAmount * 2 / 100 : 0
        };

    }
}

public interface IVehicleOperationsService
{
    Task<GetVehicleDetailResponseMessage> GetVehicleDetailByIdAsync(int id);
    Task<int> AddVehicleDetailsAsync(CreateVehicleRequestMessage requestMessage);
    Task<List<VehicleDetails>> GetVehiclesDetailsAsync(ListAllVehicleRequestMessage requestMessage);
    Task<GetCommissionDetailsResponseMessage> GetCommissionDetailsBySalesmanIdAsync(int salesmanId);
}