namespace AccountManager.Api.Dtos;

public record class AccountDto(
    int Id,
    string UserName,
    string Password,
    string Email,
    string IpAddress,
    string Category
    );
