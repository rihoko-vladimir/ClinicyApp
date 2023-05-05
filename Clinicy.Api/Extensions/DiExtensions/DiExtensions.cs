using Clinicy.WebApi.Common.MappingProfiles;
using Clinicy.WebApi.Factories;
using Clinicy.WebApi.Interfaces.Factories;
using Clinicy.WebApi.Interfaces.Repositories;
using Clinicy.WebApi.Interfaces.Services;
using Clinicy.WebApi.Repositories;
using Clinicy.WebApi.Services;

namespace Clinicy.WebApi.Extensions.DiExtensions;

public static class DiExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.AddScoped<IDoctorsRepository, DoctorsRepository>();
        serviceCollection.AddScoped<IPatientsRepository, PatientsRepository>();
        serviceCollection.AddScoped<ITicketsRepository, TicketsRepository>();
        serviceCollection.AddScoped<IDoctorsService, DoctorsService>();
        serviceCollection.AddScoped<IPatientsService, PatientsService>();
        serviceCollection.AddScoped<ITicketsService, TicketsService>();
        serviceCollection.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();

        serviceCollection.AddAutoMapper(expression => expression.AddProfile<AutoMapperProfile>());

        return serviceCollection;
    }
}