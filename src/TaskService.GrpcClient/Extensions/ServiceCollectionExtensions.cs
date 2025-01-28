using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TaskService.GrpcClient.Clients;
using TaskService.GrpcClient.Clients.Interfaces;
using TaskService.GrpcClient.Options;

namespace TaskService.GrpcClient.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTaskServiceGrpcClient(
        this IServiceCollection collection)
    {
        collection.AddGrpcClient<Tasks.TaskService.Contracts.TaskService.TaskServiceClient>((sp, o) =>
        {
            IOptions<TaskServiceClientOptions> options = sp.GetRequiredService<IOptions<TaskServiceClientOptions>>();
            o.Address = new Uri(options.Value.GrpcServerUrl);
        });

        collection.AddScoped<ITaskClient, TaskClient>();
        return collection;
    }
}