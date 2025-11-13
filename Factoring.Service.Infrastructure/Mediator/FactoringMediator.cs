using Factoring.Service.Application.Common;
using Microsoft.Extensions.DependencyInjection;

namespace Factoring.Service.Infrastructure.Mediator;

public class FactoringMediator(IServiceProvider provider) : IMediator
{
    // Handles: IRequest<TResult>
    public Task<TResult> Send<TResult>(IRequest<TResult> request, CancellationToken ct = default)
    {
        var handlerType = typeof(IRequestHandler<,>)
            .MakeGenericType(request.GetType(), typeof(TResult));

        dynamic handler = provider.GetRequiredService(handlerType);
        return handler.Handle((dynamic)request, ct);
    }

    // Handles: IRequest (void commands)
    public Task Send(IRequest request, CancellationToken ct = default)
    {
        var handlerType = typeof(IRequestHandler<>)
            .MakeGenericType(request.GetType());

        dynamic handler = provider.GetRequiredService(handlerType);
        return handler.Handle((dynamic)request, ct);
    }
}