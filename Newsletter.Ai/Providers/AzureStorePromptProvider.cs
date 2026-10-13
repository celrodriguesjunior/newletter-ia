using Newsletter.Ai.Providers.Abstractions;

namespace Newsletter.Ai.Providers;

public class AzureStorePromptProvider : IPromptProvider
{
    public Task<string> GetPromptAsync(string agentName)
    {
        throw new NotImplementedException();
    }
}
