using AITavern.Tavern.Converters;
using Artemis.Api.Models;
using Azure.AI.OpenAI;

namespace Artemis.Api;

public class TavernService 
{
    private readonly OpenAIClient _openAIClient;
    public TavernService(OpenAIClient openAIClient) 
    { 
        _openAIClient = openAIClient;
    }

    public async Task<TavernModel> OpenTavernDoorAsync()
    {
            try
            {
                var chatCompletionsOptions = new ChatCompletionsOptions();
                chatCompletionsOptions.Messages.Add(new ChatMessage(ChatRole.System, "Write me an interactive story about a tavern in a fantasy setting. Include a description, a short description optimized to generate an image with DALL-E and actions. It should be formatted as JSON like { 'description': '', 'dalle': '', 'options':['actionName':'', 'description': '']}"));
                chatCompletionsOptions.Messages.Add(new ChatMessage(ChatRole.User, "I enter the tavern."));

                var result = await _openAIClient.GetChatCompletionsAsync("gpt-3.5-turbo", chatCompletionsOptions);
                
                var options = new System.Text.Json.JsonSerializerOptions();
                options.Converters.Add(new ChatRoleJsonConverter());
                options.PropertyNameCaseInsensitive = true;
                var resultString = result.Value.Choices[0].Message.Content;
                var description = System.Text.Json.JsonSerializer.Deserialize<TavernModelDescription>(resultString, options);
                chatCompletionsOptions.Messages.Add(result.Value.Choices[0].Message);
                return new TavernModel(description, chatCompletionsOptions.Messages.ToList());
            }
            catch(Exception ex)
            {
                throw;
            }
    }

    public async Task<TavernModel> NextActionAsync(NextActionModel nextActionModel)
    {
        var chatCompletionsOptions = new ChatCompletionsOptions();
        foreach (var chatMessage in nextActionModel.Messages)
        {
            chatCompletionsOptions.Messages.Add(chatMessage);
        }

        chatCompletionsOptions.Messages.Add(new ChatMessage(ChatRole.User, nextActionModel.Prompt));
        var result = await _openAIClient.GetChatCompletionsAsync("gpt-3.5-turbo", chatCompletionsOptions);
        var options = new System.Text.Json.JsonSerializerOptions();
        options.Converters.Add(new ChatRoleJsonConverter());
        options.PropertyNameCaseInsensitive = true;
        var description = System.Text.Json.JsonSerializer.Deserialize<TavernModelDescription>(result.Value.Choices[0].Message.Content, options);
        chatCompletionsOptions.Messages.Add(result.Value.Choices[0].Message);
        return new TavernModel(description, chatCompletionsOptions.Messages.ToList());
    }
}