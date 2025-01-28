using FlightService.GrpcClient.Extensions;
using FlightService.GrpcClient.Options;
using HttpGateway.Util;
using PassengerService.GrpcClient.Extensions;
using PassengerService.GrpcClient.Options;
using TaskService.GrpcClient.Extensions;
using TaskService.GrpcClient.Options;
using TicketService.GrpcClient.Extensions;
using TicketService.GrpcClient.Options;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseKestrel();

builder.Services.Configure<PassengerServiceClientOptions>(builder.Configuration.GetSection("PassengerClient"));
builder.Services.Configure<FlightServiceClientOptions>(builder.Configuration.GetSection("FlightClient"));
builder.Services.Configure<TaskServiceClientOptions>(builder.Configuration.GetSection("TaskClient"));
builder.Services.Configure<TicketServiceClientOptions>(builder.Configuration.GetSection("TicketClient"));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt => opt.UseOneOfForPolymorphism());

builder.Services.AddTransient<ExceptionHandlerMiddleware>();

builder.Services.AddGrpcClients()
    .AddFlightServiceGrpcClient()
    .AddTaskServiceGrpcClient()
    .AddTicketGrpcClient();
WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseRouting();
app.MapControllers();

app.Run();