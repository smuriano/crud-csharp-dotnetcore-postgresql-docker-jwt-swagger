using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MavveErp.Api.Domain.Entities;
using MavveErp.Api.Domain.Repositories;
using MavveErp.Api.Infra.Contexts;
using Microsoft.EntityFrameworkCore;

namespace MavveErp.Api.Infra.Repositories
{
  public class UserRepository : IUserRepository
  {
    private readonly MavveErpContext _context;

    public UserRepository(MavveErpContext context)
    {
      _context = context;
    }


    public IEnumerable<User> GetAll()
    {
      return _context.Users.AsNoTracking().OrderBy(x => x.Email);
    }

    public User GetById(Guid id)
    {
      return _context.Users.AsNoTracking().FirstOrDefault(x => x.Id == id);
    }

    public User GetByEmail(string email)
    {
      return _context.Users.AsNoTracking().FirstOrDefault(x => x.Email == email);
    }

    public void Add(User user)
    {
      _context.Users.Add(user);
      _context.SaveChanges();
    }

    public void Update(User user)
    {
      _context.Entry(user).State = EntityState.Modified;
      _context.SaveChanges();
    }

    public void Delete(User user)
    {
      _context.Remove(user);
      _context.SaveChanges();
    }
  }
}