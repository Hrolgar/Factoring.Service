namespace Factoring.Service.Application.Common;

/// <summary>
///     Marker interface for Requests/Commands without a return value
/// </summary>
public interface IRequest { }
/// <summary>
///     Marker interface for Requests/Commands with a return value
/// </summary>
/// <typeparam name="TResult"></typeparam>
public interface IRequest<TResult> {}


/// <summary>
///    Handler for Requests/Commands without a return value
/// </summary>
/// <typeparam name="TRequest"></typeparam>
public interface IRequestHandler<in TRequest> where TRequest : IRequest
{
    Task Handle(TRequest request, CancellationToken cancellationToken);
}

/// <summary>
///   Handler for Requests/Commands with a return value
/// </summary>
/// <typeparam name="TCommand"></typeparam>
/// <typeparam name="TResult"></typeparam>
public interface IRequestHandler<in TCommand, TResult> where TCommand : IRequest<TResult>
{
    Task<TResult> Handle(TCommand command, CancellationToken ct);
}