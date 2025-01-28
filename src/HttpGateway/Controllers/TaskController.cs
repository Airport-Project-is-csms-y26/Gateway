using HttpGateway.Dtos.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskService.GrpcClient.Clients.Interfaces;
using Task = Tasks.TaskService.Contracts.Task;
using TaskStatus = HttpGateway.Dtos.Tasks.TaskStatus;

namespace HttpGateway.Controllers;

[ApiController]
[Route("tasks")]
public class TaskController : ControllerBase
{
    private readonly ITaskClient _taskClient;

    public TaskController(ITaskClient taskClient)
    {
        _taskClient = taskClient;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> CreateTask(
        [FromQuery] long flightId,
        [FromQuery] string executor,
        [FromQuery] TaskType type,
        [FromQuery] long planeNumber,
        [FromQuery] DateTimeOffset startTime,
        CancellationToken cancellationToken)
    {
        await _taskClient.CreateTask(
            flightId,
            planeNumber,
            TaskTypeMapper.ToProtoTaskType(type),
            executor,
            startTime,
            cancellationToken);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TaskDto>>> GetTasks(
        [FromQuery] int cursor,
        [FromQuery] int pageSize,
        [FromQuery] long[] taskIds,
        CancellationToken cancellationToken)
    {
        Task[] flights = await _taskClient
            .GetTasks(
                pageSize,
                cursor,
                taskIds,
                cancellationToken)
            .ToArrayAsync(cancellationToken);
        return Ok(flights.Select(TaskMapper.ToDto).ToArray());
    }

    [HttpPut("{taskId}/status")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ChangeFlightStatus(
        [FromRoute] long taskId,
        [FromQuery] TaskStatus status,
        CancellationToken cancellationToken)
    {
        await _taskClient.ChangeTaskStatus(
            taskId,
            TaskStatusMapper.ToProtoTaskState(status),
            cancellationToken);
        return StatusCode(StatusCodes.Status200OK);
    }
}