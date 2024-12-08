using System.Reflection;
using Api.Infrastructure.Repositories;
using Api.Services;
using AutoMapper;

namespace Api.Extensions;

public static class ServiceExtensions
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IVehicleDetailRepository, VehicleDetailsRepository>();
        services.AddScoped<ISalesCommissionRepository, SalesCommissionRepository>();
        services.AddScoped<IVehicleOperationsService, VehicleOperationsService>();
        services.AddAutoMapperServices();
    }

    private static void AddAutoMapperServices(this IServiceCollection services)
    {
        var mapperConfig = new MapperConfiguration(mc => mc.AddMaps(Assembly.GetExecutingAssembly()));
        var mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);
    }
}