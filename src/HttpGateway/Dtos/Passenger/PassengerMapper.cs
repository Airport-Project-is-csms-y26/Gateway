namespace HttpGateway.Dtos.Passenger;

public static class PassengerMapper
{
    public static PassengerDto ToDto(Passengers.PassengerService.Contracts.Passenger passenger)
    {
        return new PassengerDto(
            passenger.PassengerId,
            passenger.PassengerPassport,
            passenger.Name,
            passenger.Email,
            passenger.Birthday.ToDateTimeOffset(),
            passenger.IsBanned);
    }
}