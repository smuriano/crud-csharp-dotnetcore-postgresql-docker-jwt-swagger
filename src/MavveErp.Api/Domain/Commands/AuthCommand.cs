using MavveErp.Api.Domain.Commands.Contracts;

namespace MavveErp.Api.Domain.Commands
{
  public class AuthCommand : ICommand
  {
    public AuthCommand() { }
    public AuthCommand(string email, string password)
    {
      Email = email;
      Password = password;
    }

    public string Email { get; set; }
    public string Password { get; set; }
  }
}