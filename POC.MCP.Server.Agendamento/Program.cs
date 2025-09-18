using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Protocol;
using POC.MCP.Server.Agendamento.Clients;

var builder = Host.CreateApplicationBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole(opt =>
{
    opt.LogToStandardErrorThreshold = LogLevel.Debug;
});

var serverInfo = new Implementation { Name = "AgendamentoMCP", Version = "1.0.0" };
builder.Services
    .AddMcpServer(opt =>
    {
        opt.ServerInfo = serverInfo;
    })
    .WithStdioServerTransport()
    .WithToolsFromAssembly();

builder.Services.AddHttpClient<AgendamentoClient>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7245");
});

var app = builder.Build();

await app.RunAsync();
