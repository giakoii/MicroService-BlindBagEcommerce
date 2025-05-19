using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Messaging.RabbitMQ.Extensions;

public static class MassTransitConfiguration
{
    public static IServiceCollection AddMassTransitWithRabbitMQ(this IServiceCollection services, params Type[] consumerTypes)
    {
        services.AddMassTransit(x =>
        {
            foreach (var consumer in consumerTypes)
            {
                x.AddConsumer(consumer);
            }

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("localhost", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                foreach (var consumer in consumerTypes)
                {
                    var messageType = consumer.GetInterfaces()
                        .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IConsumer<>))
                        ?.GetGenericArguments()[0];

                    if (messageType != null)
                    {
                        cfg.ReceiveEndpoint(messageType.Name + "-queue", e =>
                        {
                            e.ConfigureConsumer(context, consumer);
                        });
                    }
                }
            });
        });

        return services;
    }
}