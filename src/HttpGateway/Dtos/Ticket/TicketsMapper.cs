namespace HttpGateway.Dtos.Ticket;

public static class TicketsMapper
{
    public static TicketDto ToTicketDto(Tickets.TicketsService.Contracts.Ticket ticket)
    {
        return new TicketDto(
            ticket.TicketId,
            ticket.TicketPassengerId,
            ticket.TicketPassengerId,
            ticket.TicketPlace,
            ticket.TicketRegistered);
    }
}