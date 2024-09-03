using TaskManager.Domain.Tasks;
using TaskManager.Persistence.Core;
using Task = TaskManager.Domain.Tasks.Task;

namespace TaskManager.Persistence.Tasks;

public sealed class TaskRepository(ApplicationDbContext dbContext) : Repository<Task>(dbContext), ITaskRepository;