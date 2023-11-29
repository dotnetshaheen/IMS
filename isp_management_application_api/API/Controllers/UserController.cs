using Application.Features.UserFeature;
namespace API.Controllers;
public class UserController : BaseController
{
    /// <summary>
    /// Create User
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateUserCommand command)
    {
        var response = await _mediator.Send(command);
        return StatusCode(response.StatusCode, response);
    }
}
