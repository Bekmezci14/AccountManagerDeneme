using System.ComponentModel.DataAnnotations;

namespace AccountManager.Api.Dtos;

public record class CreateCategoryDto(
    [Required] [StringLength(50)]
    string Name
);
