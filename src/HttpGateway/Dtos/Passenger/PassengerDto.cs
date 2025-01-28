namespace HttpGateway.Dtos.Passenger;

public record PassengerDto(long Id, long Passport, string Name, string Email, DateTimeOffset Birthday, bool IsBanned);