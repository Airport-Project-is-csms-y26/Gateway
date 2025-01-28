using Tasks.TaskService.Contracts;
using Task = Tasks.TaskService.Contracts.Task;

namespace TaskService.GrpcClient.Clients.Interfaces;

public interface ITaskClient
{
    Task<CreateTaskResponse> CreateTask(
        long flightId,
        long planeNumber,
        TaskType type,
        string executor,
        DateTimeOffset startTime,
        CancellationToken cancellationToken);

    Task<UpdateTaskStateResponse> ChangeTaskStatus(long taskId, TaskState status, CancellationToken cancellationToken);

    IAsyncEnumerable<Task> GetTasks(
        int pageSize,
        int cursor,
        long[] ids,
        CancellationToken cancellationToken);
}