namespace Newsletter.Ai.Agents.Abstractions;

public interface IAgent<in TData, TResponse>
{
    Task<TResponse> RunAsync(TData data, CancellationToken cancellationToken);

}
