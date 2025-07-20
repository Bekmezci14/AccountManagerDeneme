using System;
using AccountManager.Api.Dtos;
using AccountManager.Api.Entities;

namespace AccountManager.Api.Mapping;

public static class AccountMapping
{
    public static Account ToEntity(this CreateAccountDto dto)
    {
        if (dto == null) throw new ArgumentNullException(nameof(dto));

        return new Account
        {
            UserName = dto.UserName,
            Password = dto.Password,
            Email = dto.Email,
            IpAddress = dto.IpAddress,
            CategoryId = dto.CategoryId
        };
    }

    public static AccountDto ToDto(this Account entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        return new AccountDto(
            entity.Id,
            entity.UserName,
            entity.Password,
            entity.Email,
            entity.IpAddress,
            entity.Category?.Name ?? string.Empty);
    }
}
