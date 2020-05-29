using System;
using MavveErp.Api.Domain.Commands.Contracts;

namespace MavveErp.Api.Domain.Commands
{
  public class UserUpdatePassawordCommand : ICommand
  {
    public UserUpdatePassawordCommand() { }
    public UserUpdatePassawordCommand(Guid id, string password, string confirmPassword)
    {
      Id = id;
      Password = password;
      ConfirmPassword = confirmPassword;
    }

    public Guid Id { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
  }
}