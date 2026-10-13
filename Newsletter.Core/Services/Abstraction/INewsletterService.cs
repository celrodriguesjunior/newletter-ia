namespace Newsletter.Core.Services.Abstraction;

public interface INewsletterServices
{
    Task SendAsync(CancellationToken cancellationToken);
}
