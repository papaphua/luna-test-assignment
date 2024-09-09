using System.ComponentModel.DataAnnotations;
using TaskManager.Domain.Tasks;

namespace TaskManager.Application.Tasks.Dtos;

public sealed record TaskDto(
    [Required] [Length(1, 100)] string Title,
    string? Description,
    DateTime? DueDate,
    Status? Status,
    Priority? Priority);