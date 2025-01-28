namespace HttpGateway.Dtos.Flight;

public record FlightDto(
    long Id,
    string From,
    string To,
    long PlaneNumber,
    FlightStatus Status,
    DateTimeOffset DepartureTime);