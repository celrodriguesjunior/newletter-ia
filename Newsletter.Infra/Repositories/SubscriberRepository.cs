using Newsletter.Core.Models;
using Newsletter.Core.Repositories.Abstractions;

namespace Newsletter.Infra.Repositories;

public class SubscriberRepository : ISubscriberRepository
{
    public async Task<IEnumerable<Subscriber>> GetAllAsync(CancellationToken cancellationToken)
    {
        await Task.Delay(100, cancellationToken);

        return [
             new Subscriber("Teste 1","teste1@email.com"),
             new Subscriber("Teste 2","teste2@email.com"),
             new Subscriber("Teste 3","teste3@email.com"),
             new Subscriber("Teste 4","teste4@email.com"),
             new Subscriber("Teste 5","teste5@email.com"),
            ];

    }
}
