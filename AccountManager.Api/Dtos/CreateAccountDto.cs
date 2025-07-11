using System.ComponentModel.DataAnnotations;

namespace AccountManager.Api.Dtos;

public record class CreateAccountDto(
    [Required] [StringLength(50)]
    string UserName,
    [Required]
    string Password,
    string Email,
    string IpAddress,
    int CategoryId
    );