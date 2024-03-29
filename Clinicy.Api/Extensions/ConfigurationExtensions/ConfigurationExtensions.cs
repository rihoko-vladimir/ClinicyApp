using Clinicy.WebApi.Consumers;
using MassTransit;
using Shared.Models.Models.Configurations;
using Shared.Models.QueueNames;

namespace Clinicy.WebApi.Extensions.ConfigurationExtensions;

public static class ConfigurationExtensions
{
    //Код тут сделан чисто для удобства получения конфигурационных файлов + для автоконфигурации message broker'a
    public static JwtConfiguration GetJwtConfiguration(this IConfiguration configuration)
    {
        var section = configuration.GetSection(JwtConfiguration.Key);
        var jwtConfiguration = new JwtConfiguration();
        section.Bind(jwtConfiguration);

        return jwtConfiguration;
    }

    public static RabbitMqConfiguration GetRabbitMqConfiguration(this IConfiguration configuration)
    {
        var section = configuration.GetSection(RabbitMqConfiguration.Key);
        var rabbitConfiguration = new RabbitMqConfiguration();
        section.Bind(rabbitConfiguration);

        return rabbitConfiguration;
    }

    public static void ConfigureRabbitMq(
        IBusRegistrationContext context,
        IRabbitMqBusFactoryConfigurator factoryConfigurator,
        RabbitMqConfiguration rabbitConfig)
    {
        factoryConfigurator.ConfigureEndpoints(context);

        factoryConfigurator.Host(rabbitConfig.Host, rabbitConfig.VirtualHost, hostConfigurator =>
        {
            hostConfigurator.Username(rabbitConfig.UserName);
            hostConfigurator.Password(rabbitConfig.Password);
        });
    }

    private static void ConfigureEndpoints(
        this IBusFactoryConfigurator factoryConfigurator,
        IBusRegistrationContext context)
    {
        factoryConfigurator.ReceiveEndpoint(QueueNames.RegisterUser,
            endpointConfigurator => { endpointConfigurator.ConfigureConsumer<RegisterNewPatientConsumer>(context); });
    }
}