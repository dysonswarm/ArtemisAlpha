using Azure.AI.OpenAI;

namespace Artemis.Api.Models;

public class OpenAIConfig
{
    public string Key { get; set; }
}
public record TavernModel(TavernModelDescription TavernModelDescription, List<ChatMessage> Messages);
public record TavernModelDescription(string Description, List<TavernModelOption> Options);
public record TavernModelOption(string ActionName, string Description);
public class NextActionModel
{
    public NextActionModel() { }
    public NextActionModel(string prompt, List<ChatMessage> messages)
    {
        Prompt = prompt;
        Messages = messages;
    }
    public string Prompt { get; set; }
    public List<ChatMessage> Messages { get; set; }
}