namespace Factoring.Service.Application.Common;

public interface IMediator
{
    /// <summary>
    ///   Sends a request/command without a return value
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task SendAsync(IRequest request, CancellationToken ct = default);
    
    /// <summary>
    ///  Sends a request/command with a return value
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <typeparam name="TResult"></typeparam>
    /// <returns></returns>
    Task<TResult> SendAsync<TResult>(IRequest<TResult> request, CancellationToken ct = default);
}