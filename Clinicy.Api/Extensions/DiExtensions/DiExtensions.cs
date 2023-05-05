using Clinicy.WebApi.Common.MappingProfiles;
using Clinicy.WebApi.Extensions.JWTExtensions;
using Clinicy.WebApi.Factories;
using Clinicy.WebApi.Interfaces.Factories;
using Clinicy.WebApi.Interfaces.Repositories;
using Clinicy.WebApi.Interfaces.Services;
using Clinicy.WebApi.Repositories;
using Clinicy.WebApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;

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

        serviceCollection.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options => { options.ConfigureJwtBearer(configuration); });

        serviceCollection.AddAutoMapper(expression => expression.AddProfile<AutoMapperProfile>());

        return serviceCollection;
    }
}