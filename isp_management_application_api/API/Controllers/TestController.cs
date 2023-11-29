using Application.Features.TestFeature;

namespace API.Controllers;

public class TestController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var response = await _mediator.Send(new GetStaticDataQuery());
        return Ok(response);
    }
}
