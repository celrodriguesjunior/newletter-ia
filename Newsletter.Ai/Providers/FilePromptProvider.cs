using Newsletter.Ai.Providers.Abstractions;

namespace Newsletter.Ai.Providers;

public class FilePromptProvider : IPromptProvider
{
    public async Task<string> GetPromptAsync(string agentName)
    {
        var assembly = typeof(FilePromptProvider).Assembly;
        var resourceName = $"Newsletter.Ai.Prompts.{agentName}.md";

        await using var stream = assembly.GetManifestResourceStream(resourceName);

        if(stream == null)
            throw new FileNotFoundException($"Prompt file for agent '{agentName}' not found.");

        using var reader = new StreamReader(stream);
        return await reader.ReadToEndAsync();
    }
}
