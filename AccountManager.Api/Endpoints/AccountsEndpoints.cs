using System;
using AccountManager.Api.Data;
using AccountManager.Api.Dtos;
using AccountManager.Api.Entities;

namespace AccountManager.Api.Endpoints;

public static class AccountsEndpoints
{
    private static readonly List<AccountDto> accounts = [new AccountDto(1, "ArkBek14", "Bekmezci14", "arkbek14@gmail.com", "111.111.111", "Default"),
                            new AccountDto(2, "AtakanTevfik", "12345678", "atakant@gmail.com", "111.111.112", "Default")];


    public static RouteGroupBuilder MapAccountEndpoints(this WebApplication app)
    {

        var group = app.MapGroup("accounts").WithParameterValidation();

        //GET /accounts
        group.MapGet("/", () => accounts);


        //GET /accounts/id
        group.MapGet("/{id}", (int id) =>
            {
                var account = accounts.Find(account => account.Id == id);
                return account is null ? Results.NotFound() : Results.Ok(account);
            }).WithName("GetAccount");


        //Post /accounts
        group.MapPost("/", (CreateAccountDto newaccount, AccountManagerContext dbContext) =>
        {
            if (string.IsNullOrEmpty(newaccount.UserName))
            {
                return Results.BadRequest("Name is required.");
            }

            Account account = new()
            {
                UserName = newaccount.UserName,
                Password = newaccount.Password,
                Email = newaccount.Email,
                IpAddress = newaccount.IpAddress,
                CategoryId = newaccount.CategoryId,
                Category = dbContext.Categories.Find(newaccount.CategoryId)
            };
            
            dbContext.Accounts.Add(account);
            dbContext.SaveChanges();

            AccountDto accountDto = new(
                account.Id,
                account.UserName,
                account.Password,
                account.Email,
                account.IpAddress,
                account.Category!.Name
            );

            return Results.CreatedAtRoute("GetAccount", new { id = account.Id }, accountDto);
        });


        //Put /accounts/id
        group.MapPut("/{id}", (int id, UpdateAccountDto updatedaccount, AccountManagerContext dbContext) =>
        {
            var index = accounts.FindIndex(account => account.Id == id);

            if (index == -1)
            {
                return Results.NotFound();
            }

            accounts[index] = new AccountDto(
                id,
                updatedaccount.UserName,
                updatedaccount.Email,
                updatedaccount.Password,
                updatedaccount.IpAddress,
                dbContext.Categories.Find(updatedaccount.CategoryId)!.Name
            );
            return Results.NoContent();
        });

        //Delete /accounts/id
        group.MapDelete("/{id}", (int id) =>
        {
            accounts.RemoveAll(account => account.Id == id);

            return Results.NoContent();
        });

        return group;
    }
}
