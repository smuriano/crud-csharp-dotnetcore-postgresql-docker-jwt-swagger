using System;
using System.Collections.Generic;
using MavveErp.Api.Domain.Commands;
using MavveErp.Api.Domain.Entities;
using MavveErp.Api.Domain.Handlers;
using MavveErp.Api.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MavveErp.Api.Controllers
{
  [ApiController]
  [Route("v1/[controller]")]
  [Authorize]
  public class UsersController : ControllerBase
  {
    private readonly IUserRepository _userRepository;
    private readonly UserHandler _handler;

    public UsersController(IUserRepository userRepository, UserHandler handler)
    {
      _userRepository = userRepository;
      _handler = handler;
    }

    [HttpGet]
    public ActionResult<IEnumerable<User>> GetAll()
    {
      return Ok(_userRepository.GetAll());
    }

    [HttpGet("{id:Guid}")]
    public ActionResult<IEnumerable<User>> Get(Guid id)
    {
      return Ok(_userRepository.GetById(id));
    }

    [HttpPost]
    [AllowAnonymous]
    public ActionResult<GenericCommandResult> Create([FromBody] UserCreateCommand command)
    {
      return Ok((GenericCommandResult)_handler.Handle(command));
    }

    [HttpPut]
    public ActionResult<GenericCommandResult> Update([FromBody] UserUpdateCommand command)
    {
      return Ok((GenericCommandResult)_handler.Handle(command));
    }

    [Route("updatePass")]
    [HttpPut]
    public ActionResult<GenericCommandResult> UpdatePass([FromBody] UserUpdatePassawordCommand command)
    {
      return Ok((GenericCommandResult)_handler.Handle(command));
    }

    [HttpDelete]
    public ActionResult<GenericCommandResult> Delete([FromBody] UserDeleteCommand command)
    {
      return Ok((GenericCommandResult)_handler.Handle(command));
    }
  }
}