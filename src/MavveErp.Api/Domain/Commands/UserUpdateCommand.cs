using System;
using MavveErp.Api.Domain.Commands.Contracts;

namespace MavveErp.Api.Domain.Commands
{
  public class UserUpdateCommand : ICommand
  {
    public UserUpdateCommand() { }
    public UserUpdateCommand(Guid id, string username, string email)
    {
      Id = id;
      Username = username;
      Email = email;
    }

    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
  }
}