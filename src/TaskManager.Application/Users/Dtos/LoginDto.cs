using System.ComponentModel.DataAnnotations;

namespace TaskManager.Application.Users.Dtos;

public sealed record LoginDto(
    [Required] [MaxLength(100)] string UsernameOrEmail,
    [Required] [Length(8, 24)] string Password);