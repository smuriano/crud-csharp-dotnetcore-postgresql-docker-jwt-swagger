using System;
using MavveErp.Api.Domain.Commands.Contracts;

namespace MavveErp.Api.Domain.Commands
{
  public class UserDeleteCommand : ICommand
  {
    public UserDeleteCommand() { }
    public UserDeleteCommand(Guid id)
    {
      Id = id;
    }

    public Guid Id { get; set; }
  }
}