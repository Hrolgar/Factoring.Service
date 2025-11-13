namespace Factoring.Service.Application.Common;

public interface IMediator
{
    Task<TResult> Send<TResult>(IRequest<TResult> request, CancellationToken ct = default);
    Task Send(IRequest request, CancellationToken ct = default);
}