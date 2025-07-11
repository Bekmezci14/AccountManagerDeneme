using System;
using AccountManager.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace AccountManager.Api.Data;

public class AccountManagerContext(DbContextOptions<AccountManagerContext> options) : DbContext(options)
{
    public DbSet<Account> Accounts => Set<Account>();

    public DbSet<Category> Categories => Set<Category>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(
            new { Id = 1, Name = "Default" }
        );
    }
}
