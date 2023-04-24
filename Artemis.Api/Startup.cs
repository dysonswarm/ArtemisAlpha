using Artemis.Api.Models;
using Azure.AI.OpenAI;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

[assembly: FunctionsStartup(typeof(Artemis.Api.Startup))]

namespace Artemis.Api;
public class Startup : FunctionsStartup
{
    public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
    {
        builder.ConfigurationBuilder.AddEnvironmentVariables().AddUserSecrets<Startup>();
    }

    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.Services.AddOptions<OpenAIConfig>().Configure<IConfiguration>((settings, config) =>
        {
            config.GetSection("OpenAI").Bind(settings);
        });
        builder.Services.AddTransient(sp => new OpenAIClient(sp.GetRequiredService<IOptions<OpenAIConfig>>().Value.Key));
        builder.Services.AddSingleton<TavernService>();
    }
}