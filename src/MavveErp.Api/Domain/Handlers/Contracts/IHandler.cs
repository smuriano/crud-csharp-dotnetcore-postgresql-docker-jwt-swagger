using MavveErp.Api.Domain.Commands.Contracts;

namespace MavveErp.Api.Domain.Handlers.Contracts
{
  public interface IHandler<T> where T : ICommand
  {
    ICommandResult Handle(T command);
  }
}