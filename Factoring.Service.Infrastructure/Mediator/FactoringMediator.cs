using Factoring.Service.Application.Common;
using Microsoft.Extensions.DependencyInjection;

namespace Factoring.Service.Infrastructure.Mediator;

public class FactoringMediator(IServiceProvider provider) : IMediator
{
    /// <summary>
    ///     Sends a request/command with a return value
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <typeparam name="TResult"></typeparam>
    /// <returns></returns>
    public Task<TResult> Send<TResult>(IRequest<TResult> request, CancellationToken ct = default)
    {
        var handlerType = typeof(IRequestHandler<,>)
            .MakeGenericType(request.GetType(), typeof(TResult));

        dynamic handler = provider.GetRequiredService(handlerType);
        return handler.Handle((dynamic)request, ct);
    }

    /// <summary>
    ///    Sends a request/command without a return value
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public Task Send(IRequest request, CancellationToken ct = default)
    {
        var handlerType = typeof(IRequestHandler<>)
            .MakeGenericType(request.GetType());

        dynamic handler = provider.GetRequiredService(handlerType);
        return handler.Handle((dynamic)request, ct);
    }
}