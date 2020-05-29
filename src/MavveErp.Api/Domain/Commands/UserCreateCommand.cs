using MavveErp.Api.Domain.Commands.Contracts;

namespace MavveErp.Api.Domain.Commands
{
  public class UserCreateCommand : ICommand
  {
    public UserCreateCommand() { }
    public UserCreateCommand(string username, string email, string password, string confirmPassword)
    {
      Username = username;
      Email = email;
      Password = password;
      ConfirmPassword = confirmPassword;
    }

    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
  }
}