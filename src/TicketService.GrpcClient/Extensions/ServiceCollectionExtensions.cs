using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Tickets.TicketsService.Contracts;
using TicketService.GrpcClient.Clients;
using TicketService.GrpcClient.Clients.Interfaces;
using TicketService.GrpcClient.Options;

namespace TicketService.GrpcClient.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTicketGrpcClient(
        this IServiceCollection collection)
    {
        collection.AddGrpcClient<TicketsService.TicketsServiceClient>((sp, o) =>
        {
            IOptions<TicketServiceClientOptions> options = sp.GetRequiredService<IOptions<TicketServiceClientOptions>>();
            o.Address = new Uri(options.Value.GrpcServerUrl);
        });

        collection.AddScoped<ITicketClient, TicketClient>();
        return collection;
    }
}