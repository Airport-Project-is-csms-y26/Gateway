using Google.Protobuf.WellKnownTypes;
using System.Runtime.CompilerServices;
using Tasks.TaskService.Contracts;
using TaskService.GrpcClient.Clients.Interfaces;
using Task = Tasks.TaskService.Contracts.Task;

namespace TaskService.GrpcClient.Clients;

public class TaskClient : ITaskClient
{
    private readonly Tasks.TaskService.Contracts.TaskService.TaskServiceClient _client;

    public TaskClient(Tasks.TaskService.Contracts.TaskService.TaskServiceClient client)
    {
        _client = client;
    }

    public async Task<CreateTaskResponse> CreateTask(
        long flightId,
        long planeNumber,
        TaskType type,
        string executor,
        DateTimeOffset startTime,
        CancellationToken cancellationToken)
    {
        var request = new CreateTaskRequest
        {
            FlightId = flightId,
            PlaneNumber = planeNumber,
            StartTime = Timestamp.FromDateTime(startTime.UtcDateTime),
            Type = type,
            Executor = executor,
        };

        return await _client.CreateTaskAsync(request, cancellationToken: cancellationToken);
    }

    public async Task<UpdateTaskStateResponse> ChangeTaskStatus(long taskId, TaskState status, CancellationToken cancellationToken)
    {
        var request = new UpdateTaskStateRequest
        {
            TaskId = taskId,
            NewState = status,
        };

        return await _client.UpdateTaskStateAsync(request, cancellationToken: cancellationToken);
    }

    public async IAsyncEnumerable<Task> GetTasks(
        int pageSize,
        int cursor,
        long[] ids,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var request = new GetTasksRequest
        {
            Cursor = cursor,
            PageSize = pageSize,
            Ids = { ids },
        };

        GetTasksResponse response =
            await _client.GetTasksAsync(request, cancellationToken: cancellationToken);

        foreach (Task task in response.Tasks)
        {
            yield return task;
        }
    }
}