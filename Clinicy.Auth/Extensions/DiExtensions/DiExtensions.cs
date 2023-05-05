using Clinicy.Auth.Common.MappingProfiles;
using Clinicy.Auth.Extensions.ConfigurationExtensions;
using Clinicy.Auth.Extensions.JWTExtensions;
using Clinicy.Auth.Factories;
using Clinicy.Auth.Interfaces.Factories;
using Clinicy.Auth.Interfaces.Repositories;
using Clinicy.Auth.Interfaces.Services;
using Clinicy.Auth.Repositories;
using Clinicy.Auth.Services;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using massExt = Clinicy.Auth.Extensions.ConfigurationExtensions.ConfigurationExtensions;

namespace Clinicy.Auth.Extensions.DiExtensions;

public static class DiExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.AddScoped<IPatientRepository, PatientRepository>();
        serviceCollection.AddScoped<IPatientService, PatientService>();
        serviceCollection.AddScoped<ISenderService, SenderService>();
        serviceCollection.AddScoped<IAccessTokenService, AccessTokenService>();
        serviceCollection.AddSingleton(configuration.GetJwtConfiguration());
        serviceCollection.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
        
        serviceCollection.AddConfiguredMassTransit(configuration);

        serviceCollection.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options => { options.ConfigureJwtBearer(configuration); });

        serviceCollection.AddAutoMapper(expression => expression.AddProfile<AutoMapperProfile>());

        return serviceCollection;
    }
    
    private static void AddConfiguredMassTransit(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(configurator =>
        {
            configurator.UsingRabbitMq((context, factoryConfigurator) =>
                {
                    var rabbitConfig = configuration.GetRabbitMqConfiguration();
                    massExt.ConfigureRabbitMq(context, factoryConfigurator, rabbitConfig);
                });
        });
    }
}