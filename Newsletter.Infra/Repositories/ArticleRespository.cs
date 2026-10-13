using Newsletter.Core.Models;
using Newsletter.Core.Repositories.Abstractions;

namespace Newsletter.Infra.Repositories;

public class ArticleRespository : IArticleRepository
{
    public async Task<IEnumerable<Article>> GetFromLastWeekAsync(CancellationToken cancellationToken)
    {
        await Task.Delay(100, cancellationToken);
        return [
            new Article("Artigo 1", "https://example.com/artigo1", "Conteúdo 1", DateTime.Now.AddDays(-1)),
            new Article("Artigo 2", "https://example.com/artigo2", "Conteúdo 2", DateTime.Now.AddDays(-2)),
            new Article("Artigo 3", "https://example.com/artigo3", "Conteúdo 3", DateTime.Now.AddDays(-3)),
            new Article("Artigo 4", "https://example.com/artigo4", "Conteúdo 4", DateTime.Now.AddDays(-4)),
            new Article("Artigo 5", "https://example.com/artigo5", "Conteúdo 5'", DateTime.Now.AddDays(-5))
        ];
    }
}
