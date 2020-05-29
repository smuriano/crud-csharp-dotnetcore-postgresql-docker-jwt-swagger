using System;
using MavveErp.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MavveErp.Api.Infra.Contexts
{
  public class MavveErpContext : DbContext
  {
    public MavveErpContext(DbContextOptions<MavveErpContext> options) : base(options)
    {
      try
      {
        Database.Migrate();
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }
    }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<User>().ToTable("Users");
      modelBuilder.Entity<User>().Property(x => x.Id);
      modelBuilder.Entity<User>().Property(x => x.Username).HasMaxLength(20).HasColumnType("varchar(20)");
      modelBuilder.Entity<User>().Property(x => x.Email).HasMaxLength(250).HasColumnType("varchar(250)");
      modelBuilder.Entity<User>().Property(x => x.Password).HasMaxLength(250).HasColumnType("varchar(250)");
      modelBuilder.Entity<User>().Ignore(x => x.ValidationResult);
      modelBuilder.Entity<User>().Ignore(x => x.IsValid);
    }
  }
}