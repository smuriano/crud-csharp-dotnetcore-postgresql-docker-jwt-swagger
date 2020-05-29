using System;
using FluentValidation;

namespace MavveErp.Api.Domain.Entities
{
  public class User : Entity
  {
    public User(string username, string email, string password)
    {
      Username = username;
      Email = email;
      Password = password;

      Validate(this, new UserValidator());
    }

    public string Username { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }

    public void Update(string username, string email)
    {
      Username = username;
      Email = email;
    }

    public void UpdatePassword(string password)
    {
      Password = password;
    }

    public void HidePassword()
    {
      Password = "";
    }
  }

  public class UserValidator : AbstractValidator<User>
  {
    public UserValidator()
    {
      RuleFor(x => x.Id)
          .NotEqual(Guid.Empty).WithMessage("Deve ser informado o Id do usuário");

      RuleFor(x => x.Username)
          .NotEmpty().WithMessage("Deve ser informado o nome do usuário")
          .MaximumLength(20).WithMessage("Nome do usuário deve ter no máximo 20 caracteres");

      RuleFor(x => x.Email)
          .Cascade(CascadeMode.StopOnFirstFailure)
          .NotEmpty().WithMessage("Deve ser informado o e=mail do usuário")
          .EmailAddress().WithMessage("E-mail inválido");

      RuleFor(x => x.Password)
          .NotEmpty().WithMessage("Deve ser informado a senha do usuário")
          .MinimumLength(6).WithMessage("A senha deve ter no minímo de 6 caracteres");
    }
  }
}