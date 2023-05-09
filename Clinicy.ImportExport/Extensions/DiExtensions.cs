using Clinicy.ImportExport.Extensions.ConfigurationExtensions;
using Clinicy.ImportExport.Consumers;
using MassTransit;
using massExt = Clinicy.ImportExport.Extensions.ConfigurationExtensions.ConfigurationExtensions;

namespace Clinicy.ImportExport.Extensions;

public static class DiExtensions
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddConfiguredMassTransit(configuration);
    }

    //Метод доя настройки Message Broker'a
    private static void AddConfiguredMassTransit(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(configurator =>
        {
            configurator.AddConsumer<ExportConsumer>();

            configurator.UsingRabbitMq((context, factoryConfigurator) =>
            {
                var rabbitConfig = configuration.GetRabbitMqConfiguration();
                ConfigurationExtensions.ConfigurationExtensions.ConfigureRabbitMq(context, factoryConfigurator, rabbitConfig);
            });
        });
    }
}