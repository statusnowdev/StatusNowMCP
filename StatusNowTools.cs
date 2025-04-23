using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Microsoft.Extensions.AI;
using ModelContextProtocol;
using ModelContextProtocol.Protocol.Types;
using ModelContextProtocol.Server;

namespace StatusNowFancyMCP;

[McpServerToolType]
public class StatusNowMCPTools
{
    [McpServerTool(Name = "GetAlertList"), Description("Get a list of alerts")]
    public static async Task<object> GetAlertList(
        StatusNowService statusNowService,
        [Required] string accesstoken,
        [Required]  Int64 accountid)
    {

        try
        {

            //StatusNowService objstatusNowService = new StatusNowService(accesstoken, accountid);
            StatusNowService objstatusNowService = new StatusNowService();

            var alerts = await objstatusNowService.GetAlerts();
            return JsonSerializer.Serialize(alerts);

        }
        catch (Exception ex)
        {

            // Handle all other exceptions
            if (ex is McpException mcpEx)
            {
                throw; // Let the McpServer handle this specially
            }

            return Task.FromResult(new CallToolResponse()
            {
                Content = [new Content() { Text = ex.Message.ToString(), Type = "text" }]
            });

        }

    }


    [McpServerTool(Name = "GetMonitorList"), Description("Get a list of monitors")]
    public static async Task<object> GetMonitorList(
        StatusNowService statusNowService,
        [Required] string accesstoken,
        [Required] Int64 accountid)
    {

        try
        {

            StatusNowService objstatusNowService = new StatusNowService();

            var monitors = await objstatusNowService.GetMonitors();
            return JsonSerializer.Serialize(monitors);

        }
        catch (Exception ex)
        {

            // Handle all other exceptions
            if (ex is McpException mcpEx)
            {
                throw; // Let the McpServer handle this specially
            }

            return Task.FromResult(new CallToolResponse()
            {
                Content = [new Content() { Text = ex.Message.ToString(), Type = "text" }]
            });

        }
    }

   

    [McpServerTool(Name = "PauseMonitor"), Description("Pause or stop the monitor")]
    public static async Task<object> PauseMonitor(
        StatusNowService statusNowService,
        [Required] string accesstoken,
        [Required] Int64 accountid,
        [Required] [Description("Monitor Id used to stop or pause")] Int64 monitorid)
    {

        try
        {

            StatusNowService objstatusNowService = new StatusNowService();

            var result = await objstatusNowService.PauseMonitor(monitorid);
            return result;

        }
        catch (Exception ex)
        {

            // Handle all other exceptions
            if (ex is McpException mcpEx)
            {
                throw; // Let the McpServer handle this specially
            }

            return Task.FromResult(new CallToolResponse()
            {
                Content = [new Content() { Text = ex.Message.ToString(), Type = "text" }]
            });

        }

    }


    [McpServerTool(Name = "ResumeMonitor"), Description("Resume or start the monitor")]
    public static async Task<object> ResumeMonitor(
        StatusNowService statusNowService,
        [Required] string accesstoken,
        [Required] Int64 accountid,
        [Required] [Description("Monitor Id used to stop or pause")] Int64 monitorid)
    {

        try
        {

            StatusNowService objstatusNowService = new StatusNowService();

            var result = await objstatusNowService.ResumeMonitor(monitorid);
            return result;

        }
        catch (Exception ex)
        {

            // Handle all other exceptions
            if (ex is McpException mcpEx)
            {
                throw; // Let the McpServer handle this specially
            }

            return Task.FromResult(new CallToolResponse()
            {
                Content = [new Content() { Text = ex.Message.ToString(), Type = "text" }]
            });

        }

    }


    [McpServerTool(Name = "URLCheck"), Description("Check if the URL has any SSL certifcate expiry or domain expiry or ping or response delay issues")]
    public static async Task<object> URLCheck(
        StatusNowService statusNowService,
        [Required] string accesstoken,
        [Required] Int64 accountid,
        [Description("URL or web address to investigate")] string url)
    {

        try
        {

            StatusNowService objstatusNowService = new StatusNowService();

            var result = await objstatusNowService.CertCheck(url);
            result += await objstatusNowService.DomainCheck(url);
            result += await objstatusNowService.PingCheck(url);
            result += await objstatusNowService.HttpCheck(url);

            return result;

        }
        catch (Exception ex)
        {

            // Handle all other exceptions
            if (ex is McpException mcpEx)
            {
                throw; // Let the McpServer handle this specially
            }

            return Task.FromResult(new CallToolResponse()
            {
                Content = [new Content() { Text = ex.Message.ToString(), Type = "text" }]
            });

        }

    }


}