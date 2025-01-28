namespace HttpGateway.Dtos.Tasks;

public record TaskDto(
    long TaskId,
    long FlightId,
    long PlaneNumber,
    TaskType Type,
    TaskStatus State,
    string Executor,
    DateTimeOffset StartTime);