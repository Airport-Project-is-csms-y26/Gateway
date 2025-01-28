using Flights.FlightsService.Contracts;
using FlightService.GrpcClient.Clients.Interfaces;
using HttpGateway.Dtos.Flight;
using Microsoft.AspNetCore.Mvc;
using FlightStatus = HttpGateway.Dtos.Flight.FlightStatus;

namespace HttpGateway.Controllers;

[ApiController]
[Route("flights")]
public class FlightController : ControllerBase
{
    private readonly IFlightClient _flightClient;

    public FlightController(IFlightClient flightClient)
    {
        _flightClient = flightClient;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CreateFlight(
        [FromQuery] string from,
        [FromQuery] string to,
        [FromQuery] long planeNumber,
        [FromQuery] DateTimeOffset departureTime,
        CancellationToken cancellationToken)
    {
        await _flightClient.CreateFlight(from, to, planeNumber, departureTime, cancellationToken);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<FlightDto>>> GetFlights(
        [FromQuery] int cursor,
        [FromQuery] int pageSize,
        [FromQuery] long[] flightIds,
        CancellationToken cancellationToken)
    {
        Flight[] flights = await _flightClient
            .GetFlights(
                pageSize,
                cursor,
                flightIds,
                cancellationToken)
            .ToArrayAsync(cancellationToken);
        return Ok(flights.Select(FlightMapper.ToDto).ToArray());
    }

    [HttpPut("{flightId}/status")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ChangeFlightStatus(
        [FromRoute] long flightId,
        [FromQuery] FlightStatus status,
        CancellationToken cancellationToken)
    {
        await _flightClient.ChangeFlightStatus(flightId, FlightStatusMapper.ToProtoFlightStatus(status), cancellationToken);
        return StatusCode(StatusCodes.Status200OK);
    }
}