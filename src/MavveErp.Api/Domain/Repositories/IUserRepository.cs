using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MavveErp.Api.Domain.Entities;

namespace MavveErp.Api.Domain.Repositories
{
  public interface IUserRepository
  {
    IEnumerable<User> GetAll();
    User GetById(Guid id);
    User GetByEmail(string email);
    void Add(User user);
    void Update(User user);
    void Delete(User user);
  }
}