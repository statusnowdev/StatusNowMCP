
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;
using System.ComponentModel;

using StatusNowFancyMCP;
using Microsoft.Extensions.Configuration;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.Configure<StatusNowOptions>(builder.Configuration);

builder.Logging.AddConsole(consoleLogOptions =>
{
    // Configure all logs to go to stderr
    consoleLogOptions.LogToStandardErrorThreshold = LogLevel.Debug;
});
builder.Services
    .AddMcpServer()
    .WithStdioServerTransport()
    .WithToolsFromAssembly();

//var options = builder.Configuration.Get<StatusNowOptions>();
//StatusNowService objstatusNowService = new StatusNowService();

//builder.Services.AddSingleton<StatusNowService>(objstatusNowService);
builder.Services.AddSingleton<StatusNowService>();

await builder.Build().RunAsync();

//Used to read arguments from mcp.json file
public class StatusNowOptions
{
    public Int64 AccountId { get; set; }
    public string AccountToken { get; set; } = string.Empty;
}