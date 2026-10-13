using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newsletter.Ai.Agents.Abstractions;
using Newsletter.Core.Enums;
using Newsletter.Core.Models;
using Newsletter.Core.Repositories.Abstractions;
using Newsletter.Core.Services.Abstraction;

namespace Newsletter.Infra.Services;

public class NewsletterService(ILogger<NewsletterService> logger,
                               IArticleRepository articleRepository,
                               [FromKeyedServices(AgentType.TitleGenerator)]
                               IAgent<IEnumerable<Article>, string> titleGeneratorAgent,
                               [FromKeyedServices(AgentType.NewsletterGenerator)]
                               IAgent<IEnumerable<Article>, string> newsletterGeneratorAgent,
                               ISubscriberRepository subscriberRepository,
                               IEmailServices emailService
                               ) : INewsletterServices
{

    public async Task SendAsync(CancellationToken cancellationToken)
    {
        //recupera os posts da semana
        logger.LogInformation("Recuperando os posts da semana...");
        var posts = await articleRepository.GetFromLastWeekAsync(cancellationToken);
        if (!posts.Any())
            return;

        //Gera um título para a newsletter
        logger.LogInformation("Gerando o título da newsletter...");
        var subject = await titleGeneratorAgent.RunAsync(posts, cancellationToken);

        //Gera o conteúdo da newsletter
        logger.LogInformation("Gerando o conteúdo da newsletter...");
        var body = await newsletterGeneratorAgent.RunAsync(posts, cancellationToken);

        //Recupera os assinantes
        logger.LogInformation("Recuperando os assinantes...");
        var subscribers = await subscriberRepository.GetAllAsync(cancellationToken);

        //Envia a newsletter para cada assinante
        logger.LogInformation("Enviando a newsletter para os assinantes...");
        foreach (var subscriber in subscribers)
            await emailService.SendAsync(subscriber.Name, subscriber.Email, subject, body, cancellationToken);

        logger.LogInformation("Newsletter enviada com sucesso para {Count} assinantes.", subscribers.Count());

    }

}
