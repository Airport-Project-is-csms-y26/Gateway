using Tasks.TaskService.Contracts;

namespace HttpGateway.Dtos.Tasks;

public static class TaskStatusMapper
{
    public static TaskStatus ToTaskStatus(TaskState state)
    {
        return state switch
        {
            TaskState.StateNone => throw new InvalidCastException("TaskState is not defined. Possible server error!"),
            TaskState.StatePlanned => TaskStatus.Planned,
            TaskState.StateRunning => TaskStatus.Running,
            TaskState.StateCompleted => TaskStatus.Completed,
            TaskState.StateCancelled => TaskStatus.Cancelled,
            _ => throw new InvalidCastException($"Unknown TaskState value: {state}"),
        };
    }

    public static TaskState ToProtoTaskState(TaskStatus status)
    {
        return status switch
        {
            TaskStatus.Planned => TaskState.StatePlanned,
            TaskStatus.Running => TaskState.StateRunning,
            TaskStatus.Completed => TaskState.StateCompleted,
            TaskStatus.Cancelled => TaskState.StateCancelled,
            _ => throw new InvalidCastException($"Unknown TaskStatus value: {status}"),
        };
    }
}