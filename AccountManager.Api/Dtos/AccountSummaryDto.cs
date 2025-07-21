namespace AccountManager.Api.Dtos;

public record class AccountSummaryDto(
    int Id,
    string UserName,
    string Password,
    string Email,
    string IpAddress,
    string Category
    );
