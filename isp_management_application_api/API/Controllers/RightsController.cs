using Application.Features.AuthFeature;

namespace API.Controllers;

public class RightsController : BaseController
{
    [HttpGet("sync-app-feature")]
    public async Task<IActionResult> SyncAppFeatures()
    {
        var response = await _mediator.Send(new SynchronizeAppFeatureCommand());
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("sync-right")]
    public async Task<IActionResult> SyncRoleRights()
    {
        var response = await _mediator.Send(new SynchoronizeRightsCommand());
        return StatusCode(response.StatusCode, response);
    }
}
