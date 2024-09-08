﻿using TaskManager.Domain.Tasks;

namespace TaskManager.Application.Tasks.Dtos;

public sealed record NewTaskDto(
    string Title,
    string Description,
    DateTime? DueDate,
    Status? Status,
    Priority? Priority);