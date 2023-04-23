using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Artemis.Api.Core;
using Microsoft.AspNetCore.Mvc;
using Artemis.Api.Models;

namespace Artemis.Api;
public class EnterTavern: ArtemisAzureFunction
{
    private readonly TavernService _tavernService;
    public EnterTavern(TavernService tavernService)
    {
        _tavernService = tavernService;
    }

    [FunctionName("EnterTavern")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req)
    {
        var model = await _tavernService.OpenTavernDoorAsync();
        return Ok(model);
    }

    [FunctionName("Action")]
    public async Task<IActionResult> Action(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req)
    {
        var nextAction = DeserializeBody<NextActionModel>(req.Body);

        var model = await _tavernService.NextActionAsync(nextAction);
        return Ok(model);
    }
}
