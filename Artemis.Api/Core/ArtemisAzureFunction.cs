using AITavern.Tavern.Converters;
using Artemis.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Artemis.Api.Core
{
    public class ArtemisAzureFunction
    {
        public IActionResult Ok<T>(T model)
        {
            
            return new ContentResult
            {
                ContentType = "application/json",
                Content = Serialize(model),
                StatusCode = 200
            };
        }

        public T DeserializeBody<T>(Stream stream)
        {
            return JsonSerializer.Deserialize<T>(stream, BuildOptions());
        }

        private string Serialize<T>(T model)
        {
            return JsonSerializer.Serialize(model, BuildOptions());
        }

        private JsonSerializerOptions BuildOptions()
        {            
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            options.Converters.Add(new ChatRoleJsonConverter());
            return options;
        }

    }
}
