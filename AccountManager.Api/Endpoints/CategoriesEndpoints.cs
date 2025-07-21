using System;
using AccountManager.Api.Data;
using AccountManager.Api.Dtos;
using AccountManager.Api.Entities;
using AccountManager.Api.Mapping;
using Microsoft.EntityFrameworkCore;

namespace AccountManager.Api.Endpoints;

public static class CategoriesEndpoints
{
    public static RouteGroupBuilder MapCategoriesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("categories").WithParameterValidation();

        group.MapGet("/", async (AccountManagerContext dbContext) =>
            await dbContext.Categories.Select(category => category.ToDto()).AsNoTracking().ToListAsync());


        group.MapGet("/{id}", async (int id, AccountManagerContext dbContext) => 
        {
            var category = await dbContext.Categories.FindAsync(id);
            return category is null ? Results.NotFound() : Results.Ok(category.ToDto());
        }).WithName("GetCategory");


        group.MapPost("/", async (CreateCategoryDto newCategory, AccountManagerContext dbContext) =>
        {
            if (string.IsNullOrEmpty(newCategory.Name))
            {
                return Results.BadRequest("Name is required.");
            }

            Category category = newCategory.ToEntity();

            
            if (dbContext.Categories.Any(c => c.Name == category.Name))
            {
                return Results.BadRequest("Category with this name already exists.");
            }

            dbContext.Categories.Add(category);
            await dbContext.SaveChangesAsync();

            return Results.CreatedAtRoute("GetCategory", new { id = category.Id }, category.ToDto());
        });


        group.MapPut("/{id}", async (int id, CreateCategoryDto updatedCategory, AccountManagerContext dbContext) =>
        {
            Category? existingCategory = await dbContext.Categories.FindAsync(id);

            if (existingCategory is null)
            {
                return Results.NotFound();
            }

            dbContext.Entry(existingCategory).CurrentValues.SetValues(updatedCategory);
            await dbContext.SaveChangesAsync();
            return Results.NoContent();
        });


        group.MapDelete("/{id}", async (int id, AccountManagerContext dbContext) =>
        {
            var deletedCount = await dbContext.Categories.Where(c => c.Id == id).ExecuteDeleteAsync();

            if (deletedCount == 0)
            {
                return Results.NotFound();
            }

            return Results.NoContent();
        });
        
        return group;
    }
}
