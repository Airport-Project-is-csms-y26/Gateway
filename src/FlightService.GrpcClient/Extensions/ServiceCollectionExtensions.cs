using Flights.FlightsService.Contracts;
using FlightService.GrpcClient.Clients;
using FlightService.GrpcClient.Clients.Interfaces;
using FlightService.GrpcClient.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace FlightService.GrpcClient.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFlightServiceGrpcClient(
        this IServiceCollection collection)
    {
        collection.AddGrpcClient<FlightsService.FlightsServiceClient>((sp, o) =>
        {
            IOptions<FlightServiceClientOptions> options = sp.GetRequiredService<IOptions<FlightServiceClientOptions>>();
            o.Address = new Uri(options.Value.GrpcServerUrl);
        });

        collection.AddScoped<IFlightClient, FlightClient>();
        return collection;
    }
}