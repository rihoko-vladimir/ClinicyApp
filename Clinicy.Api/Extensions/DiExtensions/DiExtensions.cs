using Clinicy.WebApi.Common.MappingProfiles;
using Clinicy.WebApi.Common.SqlMappers;
using Clinicy.WebApi.Consumers;
using Clinicy.WebApi.Extensions.ConfigurationExtensions;
using Clinicy.WebApi.Extensions.JWTExtensions;
using Clinicy.WebApi.Factories;
using Clinicy.WebApi.Interfaces.Factories;
using Clinicy.WebApi.Interfaces.Repositories;
using Clinicy.WebApi.Interfaces.Services;
using Clinicy.WebApi.Repositories;
using Clinicy.WebApi.Services;
using Dapper;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using massExt = Clinicy.WebApi.Extensions.ConfigurationExtensions.ConfigurationExtensions;

namespace Clinicy.WebApi.Extensions.DiExtensions;

public static class DiExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        //Добавление сервисов в DI контейнер
        serviceCollection.AddScoped<IDoctorsRepository, DoctorsRepository>();
        serviceCollection.AddScoped<IPatientsRepository, PatientsRepository>();
        serviceCollection.AddScoped<ITicketsRepository, TicketsRepository>();
        serviceCollection.AddScoped<IDoctorsService, DoctorsService>();
        serviceCollection.AddScoped<IPatientsService, PatientsService>();
        serviceCollection.AddScoped<ITicketsService, TicketsService>();
        serviceCollection.AddScoped<IAccessTokenService, AccessTokenService>();
        serviceCollection.AddScoped<ISenderService, SenderService>();
        serviceCollection.AddSingleton(configuration.GetJwtConfiguration());
        serviceCollection.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
        serviceCollection.AddConfiguredMassTransit(configuration);

        SqlMapper.AddTypeHandler(new TrimmedStringMapper());

        serviceCollection.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options => { options.ConfigureJwtBearer(configuration); });

        serviceCollection.AddAutoMapper(expression => expression.AddProfile<AutoMapperProfile>());

        return serviceCollection;
    }

    //Метод доя настройки Message Broker'a
    private static void AddConfiguredMassTransit(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(configurator =>
        {
            configurator.AddConsumer<RegisterNewPatientConsumer>();

            configurator.UsingRabbitMq((context, factoryConfigurator) =>
            {
                var rabbitConfig = configuration.GetRabbitMqConfiguration();
                massExt.ConfigureRabbitMq(context, factoryConfigurator, rabbitConfig);
            });
        });
    }
}