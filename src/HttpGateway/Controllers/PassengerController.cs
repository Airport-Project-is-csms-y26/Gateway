using HttpGateway.Dtos.Passenger;
using HttpGateway.Dtos.Ticket;
using Microsoft.AspNetCore.Mvc;
using Passengers.PassengerService.Contracts;
using PassengerService.GrpcClient.Clients.Interfaces;

namespace HttpGateway.Controllers;

[ApiController]
[Route("passengers")]
public class PassengerController : ControllerBase
{
    private readonly IPassengerClient _passengerClient;

    public PassengerController(IPassengerClient passengerClient)
    {
        _passengerClient = passengerClient;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult> CreatePassenger(
        [FromQuery] string name,
        [FromQuery] string email,
        [FromQuery] long passport,
        [FromQuery] DateTimeOffset birthDay,
        CancellationToken cancellationToken)
    {
        await _passengerClient.CreatePassenger(name, passport, email, birthDay, cancellationToken);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TicketDto>>> GetPassengers(
        [FromQuery] int cursor,
        [FromQuery] int pageSize,
        [FromQuery] long[] passengerIds,
        [FromQuery] long[] passportIds,
        [FromQuery] string[] emails,
        [FromQuery] string? name,
        CancellationToken cancellationToken)
    {
        Passenger[] passengers = await _passengerClient
            .GetPassengers(
                cursor,
                pageSize,
                passengerIds,
                passportIds,
                emails,
                name,
                cancellationToken)
            .ToArrayAsync(cancellationToken);
        return Ok(passengers.Select(PassengerMapper.ToDto).ToArray());
    }

    [HttpPut("{passengerId}/ban")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> BanPassenger(
        [FromRoute] long passengerId,
        CancellationToken cancellationToken)
    {
        await _passengerClient.BanPassenger(passengerId, cancellationToken);
        return StatusCode(StatusCodes.Status200OK);
    }
}