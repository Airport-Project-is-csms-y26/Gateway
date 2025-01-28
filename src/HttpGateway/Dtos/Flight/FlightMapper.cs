namespace HttpGateway.Dtos.Flight;

public static class FlightMapper
{
    public static FlightDto ToDto(Flights.FlightsService.Contracts.Flight flight)
    {
        return new FlightDto(
            flight.FlightId,
            flight.From,
            flight.To,
            flight.PlaneNumber,
            FlightStatusMapper.ToFlightStatus(flight.Status),
            flight.DepartureTime.ToDateTimeOffset());
    }
}