using MavveErp.Api.Domain.Commands;
using MavveErp.Api.Domain.Commands.Contracts;
using MavveErp.Api.Domain.Entities;
using MavveErp.Api.Domain.Handlers.Contracts;
using MavveErp.Api.Domain.Repositories;
using MavveErp.Api.Security;

namespace MavveErp.Api.Domain.Handlers
{
  public class UserHandler :
    IHandler<UserCreateCommand>,
    IHandler<UserUpdateCommand>,
    IHandler<UserUpdatePassawordCommand>,
    IHandler<UserDeleteCommand>
  {
    private readonly IUserRepository _userRepository;

    public UserHandler(IUserRepository userRepository)
    {
      _userRepository = userRepository;
    }

    public ICommandResult Handle(UserCreateCommand command)
    {
      var user = new User(command.Username, command.Email, HashingBCrypt.HashPassword(command.Password));
      if (!user.IsValid)
        return new GenericCommandResult(false, "Dados inválido", user.ValidationResult.Errors);

      _userRepository.Add(user);

      return new GenericCommandResult(true, "Usuário cadastrado com sucesso", user);
    }
    public ICommandResult Handle(UserUpdateCommand command)
    {
      var user = _userRepository.GetById(command.Id);
      user.Update(command.Username, command.Email);
      if (!user.IsValid)
        return new GenericCommandResult(false, "Dados inválido", user.ValidationResult.Errors);

      _userRepository.Update(user);

      return new GenericCommandResult(true, "Usuário atualizado com sucesso", user);
    }
    public ICommandResult Handle(UserUpdatePassawordCommand command)
    {
      if (command.Password != command.ConfirmPassword)
        return new GenericCommandResult(false, "Confirmação de senha diferente da senha", null);

      var user = _userRepository.GetById(command.Id);

      user.UpdatePassword(HashingBCrypt.HashPassword(command.Password));
      if (!user.IsValid)
        return new GenericCommandResult(false, "Dados inválido", user.ValidationResult.Errors);

      _userRepository.Update(user);

      return new GenericCommandResult(true, "Senha do usuário atualizado com sucesso", null);
    }

    public ICommandResult Handle(UserDeleteCommand command)
    {
      var user = _userRepository.GetById(command.Id);

      if (user == null)
        return new GenericCommandResult(false, "Usuário não localizado", null);

      _userRepository.Delete(user);

      return new GenericCommandResult(true, "Usuário excluído com sucesso", null);
    }
  }
}
