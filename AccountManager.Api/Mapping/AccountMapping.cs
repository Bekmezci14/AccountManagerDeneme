using System;
using AccountManager.Api.Dtos;
using AccountManager.Api.Entities;

namespace AccountManager.Api.Mapping;

public static class AccountMapping
{
    public static Account ToEntity(this CreateAccountDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        return new Account
        {
            UserName = dto.UserName,
            Password = dto.Password,
            Email = dto.Email,
            IpAddress = dto.IpAddress,
            CategoryId = dto.CategoryId
        };
    }

    public static Account ToEntity(this UpdateAccountDto dto, int id)
    {
        ArgumentNullException.ThrowIfNull(dto);

        return new Account
        {
            Id = id,
            UserName = dto.UserName,
            Password = dto.Password,
            Email = dto.Email,
            IpAddress = dto.IpAddress,
            CategoryId = dto.CategoryId
        };
    }

    public static AccountSummaryDto ToSummaryDto(this Account entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        return new AccountSummaryDto(
            entity.Id,
            entity.UserName,
            entity.Password,
            entity.Email,
            entity.IpAddress,
            entity.Category!.Name);
    }

    public static AccountDetailDto ToDetailsDto(this Account entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        return new AccountDetailDto(
            entity.Id,
            entity.UserName,
            entity.Password,
            entity.Email,
            entity.IpAddress,
            entity.CategoryId);
    }
}
