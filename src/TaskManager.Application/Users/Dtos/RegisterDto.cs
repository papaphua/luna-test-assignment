using System.ComponentModel.DataAnnotations;

namespace TaskManager.Application.Users.Dtos;

public sealed record RegisterDto(
    [Required] [MaxLength(100)] string Username,
    [Required] [MaxLength(100)] string Email,
    [Required] [Length(8, 24)] string Password);