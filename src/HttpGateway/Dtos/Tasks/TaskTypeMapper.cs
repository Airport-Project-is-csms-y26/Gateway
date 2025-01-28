using InvalidCastException = System.InvalidCastException;

namespace HttpGateway.Dtos.Tasks;

public static class TaskTypeMapper
{
    public static TaskType ToTaskType(global::Tasks.TaskService.Contracts.TaskType type)
    {
        return type switch
        {
            global::Tasks.TaskService.Contracts.TaskType.TypeNone => throw new InvalidCastException("TaskType is not defined. Possible server error!"),
            global::Tasks.TaskService.Contracts.TaskType.TypeTechnicalInspection => TaskType.TechnicalInspection,
            global::Tasks.TaskService.Contracts.TaskType.TypeRefueling => TaskType.Refueling,
            global::Tasks.TaskService.Contracts.TaskType.TypeCleaning => TaskType.Cleaning,
            global::Tasks.TaskService.Contracts.TaskType.TypeRepair => TaskType.Repair,
            global::Tasks.TaskService.Contracts.TaskType.TypeLoading => TaskType.Loading,
            _ => throw new InvalidCastException($"Unknown TaskTypeProto value: {type}"),
        };
    }

    public static global::Tasks.TaskService.Contracts.TaskType ToProtoTaskType(TaskType type)
    {
        return type switch
        {
            TaskType.TechnicalInspection => global::Tasks.TaskService.Contracts.TaskType.TypeTechnicalInspection,
            TaskType.Refueling => global::Tasks.TaskService.Contracts.TaskType.TypeRefueling,
            TaskType.Cleaning => global::Tasks.TaskService.Contracts.TaskType.TypeCleaning,
            TaskType.Repair => global::Tasks.TaskService.Contracts.TaskType.TypeRepair,
            TaskType.Loading => global::Tasks.TaskService.Contracts.TaskType.TypeLoading,
            _ => throw new InvalidCastException($"Unknown TaskType value: {type}"),
        };
    }
}