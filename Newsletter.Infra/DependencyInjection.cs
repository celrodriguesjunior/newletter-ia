using Microsoft.Extensions.DependencyInjection;
using Newsletter.Core.Repositories.Abstractions;
using Newsletter.Core.Services.Abstraction;
using Newsletter.Infra.Repositories;
using Newsletter.Infra.Services;

namespace Newsletter.Infra;

public static class DependencyInjection
{

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<INewsletterServices, NewsletterService>();
        services.AddScoped<IEmailServices, EmailService>();
        return services;
    }


    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {

        services.AddScoped<IArticleRepository, ArticleRespository>();
        services.AddScoped<ISubscriberRepository, SubscriberRepository>();

        return services;
    }

}
