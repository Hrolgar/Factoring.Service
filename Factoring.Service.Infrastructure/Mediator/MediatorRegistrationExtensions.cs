using System.Reflection;
using Factoring.Service.Application.Common;
using Microsoft.Extensions.DependencyInjection;

namespace Factoring.Service.Infrastructure.Mediator;

public static class MediatorRegistrationExtensions
{
    public static IServiceCollection AddFactoringMediator(this IServiceCollection services, Assembly assemblyToScan)
    {
        // Register mediator itself
        services.AddScoped<IMediator, FactoringMediator>();

        // Register all IRequestHandler<TRequest, TResult>
        var handlerInterface = typeof(IRequestHandler<,>);
        
        var types = assemblyToScan.GetTypes()
            .Where(t => t is { IsAbstract: false, IsInterface: false })
            .ToList();

        foreach (var type in types)
        {
            foreach (var interace in type.GetInterfaces())
            {
                if (!interace.IsGenericType) continue;

                var genericDef = interace.GetGenericTypeDefinition();

                // Generic request handler: IRequestHandler<TRequest, TResult>
                if (genericDef == typeof(IRequestHandler<,>))
                {
                    services.AddScoped(interace, type);
                }

                // Non-generic request handler: IRequestHandler<TRequest>
                if (genericDef == typeof(IRequestHandler<>))
                {
                    services.AddScoped(interace, type);
                }
            }
        }

        return services;
    }
}