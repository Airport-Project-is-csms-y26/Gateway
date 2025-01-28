namespace HttpGateway.Dtos.Ticket;

public record TicketDto(long Id, long FlightId, long PassengerId, long Place, bool IsRegistered);