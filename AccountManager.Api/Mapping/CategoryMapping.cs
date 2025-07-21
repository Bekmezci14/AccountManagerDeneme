using System;
using AccountManager.Api.Dtos;
using AccountManager.Api.Entities;

namespace AccountManager.Api.Mapping;

public static class CategoryMapping
{
    public static CategoryDto ToDto(this Category category)
    {
        return new CategoryDto(category.Id, category.Name);
    }

    public static Category ToEntity(this CreateCategoryDto createCategoryDto)
    {
        return new Category
        {
            Name = createCategoryDto.Name
        };
    }
        
}
