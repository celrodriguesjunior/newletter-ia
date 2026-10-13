namespace Newsletter.Core.Services.Abstraction;

public interface IEmailServices
{
    Task SendAsync(string toName, string toEmail, string subject, string body, CancellationToken cancellationToken);
}
