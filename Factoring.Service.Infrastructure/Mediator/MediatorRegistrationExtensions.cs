using Factoring.Service.Application.Common;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Factoring.Service.Infrastructure.Mediator;

public static class MediatorRegistrationExtensions
{
    public static IServiceCollection AddFactoringMediator(this IServiceCollection services, Assembly assemblyToScan)
    {
        // Register mediator itself
        services.AddScoped<IMediator, FactoringMediator>();

        // Register all IRequestHandler<TRequest, TResult>
        var handlerInterface = typeof(IRequestHandler<,>);
        
        var types = assemblyToScan.GetTypes()
            .Where(t => !t.IsAbstract && !t.IsInterface)
            .Select(t => new {
                Type = t,
                Interfaces = t.GetInterfaces()
            })
            .ToList();

        foreach (var t in types)
        {
            foreach (var i in t.Interfaces)
            {
                if (i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterface)
                {
                    services.AddScoped(i, t.Type);
                }
            }
        }

        return services;
    }
}