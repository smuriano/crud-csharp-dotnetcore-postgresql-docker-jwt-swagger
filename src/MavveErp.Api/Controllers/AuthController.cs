using System.Threading.Tasks;
using MavveErp.Api.Domain.Commands;
using MavveErp.Api.Domain.Repositories;
using MavveErp.Api.Security;
using Microsoft.AspNetCore.Mvc;

namespace MavveErp.Api.Controllers
{
  [ApiController]
  [Route("v1/[controller]")]
  public class AuthController : ControllerBase
  {
    private readonly IUserRepository _userRepository;

    public AuthController(IUserRepository userRepository)
    {
      _userRepository = userRepository;
    }

    [HttpPost]
    public ActionResult<dynamic> Authenticate([FromBody] AuthCommand command)
    {
      var user = _userRepository.GetByEmail(command.Email);

      if (user == null)
        return NotFound(new { message = "Usuário ou senha inválidos" });

      if (!HashingBCrypt.ValidatePassword(command.Password, user.Password))
        return NotFound(new { message = "Usuário ou senha inválidos" });

      var token = TokenService.GenerateToken(user);

      user.HidePassword();

      return Ok(new { user = user, token = token });
    }
  }
}