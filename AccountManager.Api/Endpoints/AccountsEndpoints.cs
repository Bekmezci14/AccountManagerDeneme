using System;
using AccountManager.Api.Data;
using AccountManager.Api.Dtos;
using AccountManager.Api.Entities;
using AccountManager.Api.Mapping;
using Microsoft.EntityFrameworkCore;

namespace AccountManager.Api.Endpoints;

public static class AccountsEndpoints
{
    public static RouteGroupBuilder MapAccountEndpoints(this WebApplication app)
    {

        var group = app.MapGroup("accounts").WithParameterValidation();

        //GET /accounts
        group.MapGet("/", async (AccountManagerContext dbContext) =>
            await dbContext.Accounts.Include(account => account.Category).Select(account => account.ToSummaryDto()).AsNoTracking().ToListAsync());
            // this line retrieves all accounts and includes their categories, mapping them to summary DTOs.


        //GET /accounts/id
        group.MapGet("/{id}", async (int id, AccountManagerContext dbContext) =>
            {
                Account? account = await dbContext.Accounts.FindAsync(id);
                return account is null ? Results.NotFound() : Results.Ok(account.ToDetailsDto());
            }).WithName("GetAccount");


        //Post /accounts
        group.MapPost("/", async (CreateAccountDto newaccount, AccountManagerContext dbContext) =>
        {
            if (string.IsNullOrEmpty(newaccount.UserName))
            {
                return Results.BadRequest("Name is required.");
            }

            Account account = newaccount.ToEntity();
            //account.Category = dbContext.Categories.Find(newaccount.CategoryId);
            
            dbContext.Accounts.Add(account);
            await dbContext.SaveChangesAsync();

            return Results.CreatedAtRoute("GetAccount", new { id = account.Id }, account.ToDetailsDto());
        });


        //Put /accounts/id
        group.MapPut("/{id}", async (int id, UpdateAccountDto updatedaccount, AccountManagerContext dbContext) =>
        {
            Account? existingAccount = await dbContext.Accounts.FindAsync(id);

            if (existingAccount is null)
            {
                return Results.NotFound();
            }

            dbContext.Entry(existingAccount).CurrentValues.SetValues(updatedaccount);
            await dbContext.SaveChangesAsync();
            
            return Results.NoContent();
        });

        //Delete /accounts/id
        group.MapDelete("/{id}", async (int id, AccountManagerContext dbContext) =>
        {
            var deletedCount = await dbContext.Accounts.Where(account => account.Id == id).ExecuteDeleteAsync();
            // batch delete 

            if (deletedCount == 0)
            {
                return Results.NotFound();
            }

            return Results.NoContent();
        });

        return group;
    }
}
