using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using PassengerService.GrpcClient.Clients;
using PassengerService.GrpcClient.Clients.Interfaces;
using PassengerService.GrpcClient.Options;

namespace PassengerService.GrpcClient.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGrpcClients(
        this IServiceCollection collection)
    {
        collection.AddGrpcClient<Passengers.PassengerService.Contracts.PassengerService.PassengerServiceClient>((sp, o) =>
        {
            IOptions<PassengerServiceClientOptions> options = sp.GetRequiredService<IOptions<PassengerServiceClientOptions>>();
            o.Address = new Uri(options.Value.GrpcServerUrl);
        });

        collection.AddScoped<IPassengerClient, PassengerClient>();
        return collection;
    }
}