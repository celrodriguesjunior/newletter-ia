using Microsoft.Extensions.Logging;
using Newsletter.Core.Services.Abstraction;

namespace Newsletter.Infra.Services;

public class EmailService(ILogger<EmailService> logger) : IEmailServices
{
    public async Task SendAsync(string toName, string toEmail, string subject, string body, CancellationToken cancellationToken)
    {
        await Task.Delay(1000, cancellationToken); // Simulate sending email
        logger.LogInformation($"Email sent to {toName} ({toEmail}) with subject '{subject}'");
    }
}
