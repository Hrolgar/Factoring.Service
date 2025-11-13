namespace Factoring.Service.Application.Common;

public interface IRequest<TResult> {}
public interface IRequest { }

public interface IRequestHandler<in TCommand, TResult>
    where TCommand : IRequest<TResult>
{
    Task<TResult> Handle(TCommand command, CancellationToken ct);
}

public interface IRequestHandler<in TRequest>
    where TRequest : IRequest
{
    Task Handle(TRequest request, CancellationToken cancellationToken);
}