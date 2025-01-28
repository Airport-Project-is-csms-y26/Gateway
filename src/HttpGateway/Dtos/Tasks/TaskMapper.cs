using Task = Tasks.TaskService.Contracts.Task;

namespace HttpGateway.Dtos.Tasks;

public static class TaskMapper
{
    public static TaskDto ToDto(Task task)
    {
        return new TaskDto(
            task.TaskId,
            task.FlightId,
            task.PlaneNumber,
            TaskTypeMapper.ToTaskType(task.Type),
            TaskStatusMapper.ToTaskStatus(task.State),
            task.Executor,
            task.StartTime.ToDateTimeOffset());
    }
}