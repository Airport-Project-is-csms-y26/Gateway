using HttpGateway.Dtos.Ticket;
using Microsoft.AspNetCore.Mvc;
using Tickets.TicketsService.Contracts;
using TicketService.GrpcClient.Clients.Interfaces;

namespace HttpGateway.Controllers;

[ApiController]
[Route("tickets")]
public class TicketController : ControllerBase
{
    private readonly ITicketClient _ticketClient;

    public TicketController(ITicketClient ticketClient)
    {
        _ticketClient = ticketClient;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> CreateTicket(
        [FromQuery] long passengerId,
        [FromQuery] long flightId,
        [FromQuery] long place,
        CancellationToken cancellationToken)
    {
        await _ticketClient.CreateTicket(passengerId, flightId, place, cancellationToken);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TicketDto>>> GetTickets(
        [FromQuery] int cursor,
        [FromQuery] int pageSize,
        [FromQuery] long[] ticketIds,
        [FromQuery] long[] passengerIds,
        [FromQuery] long[] flightIds,
        CancellationToken cancellationToken)
    {
        Ticket[] tickets = await _ticketClient
            .GetTickets(
                pageSize,
                cursor,
                ticketIds,
                flightIds,
                passengerIds,
                cancellationToken)
            .ToArrayAsync(cancellationToken);
        return Ok(tickets.Select(TicketsMapper.ToTicketDto).ToArray());
    }

    [HttpPut("{ticketId}/register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RegisterPassengerOnFlight(
        [FromRoute] long ticketId,
        CancellationToken cancellationToken)
    {
        await _ticketClient.RegisterPassengerOnFlight(ticketId, cancellationToken);
        return StatusCode(StatusCodes.Status200OK);
    }
}