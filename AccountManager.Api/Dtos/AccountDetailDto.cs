using System;

namespace AccountManager.Api.Dtos;

public record class AccountDetailDto(
    int Id,
    string UserName,
    string Password,
    string Email,
    string IpAddress,
    int CategoryId
    );