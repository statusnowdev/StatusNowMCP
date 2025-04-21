using System;
using System.ComponentModel;
using System.Text.Json;
using ModelContextProtocol.Server;

namespace StatusNowFancyMCP;

[McpServerToolType]
public class StatusNowMCPTools
{
    [McpServerTool(Name = "GetAlertList"), Description("Get a list of alerts")]
    public static async Task<string> GetAlertList(
        StatusNowService statusNowService, 
        string accesstoken, 
        Int64 accountid)
    {
        StatusNowService objstatusNowService = new StatusNowService(accesstoken,accountid);

        var alerts = await objstatusNowService.GetAlerts();
        return JsonSerializer.Serialize(alerts);
    }

    [McpServerTool(Name = "GetMonitorList"), Description("Get a list of monitors")]
    public static async Task<string> GetMonitorList(
        StatusNowService statusNowService, 
        string accesstoken, 
        Int64 accountid)
    {

        StatusNowService objstatusNowService = new StatusNowService(accesstoken,accountid);

        var monitors = await objstatusNowService.GetMonitors();
        return JsonSerializer.Serialize(monitors);
    }

    [McpServerTool(Name = "PauseMonitor"), Description("Pause or stop the monitor")]
    public static async Task<string> PauseMonitor(
        StatusNowService statusNowService, 
        string accesstoken, 
        Int64 accountid, 
        [Description("Monitor Id used to stop or pause")] Int64 monitorid)
    {

        StatusNowService objstatusNowService = new StatusNowService(accesstoken,accountid);

        var result = await objstatusNowService.PauseMonitor(monitorid);
        return result;  

    }


    [McpServerTool(Name = "ResumeMonitor"), Description("Resume or start the monitor")]
    public static async Task<string> ResumeMonitor(
        StatusNowService statusNowService, 
        string accesstoken, 
        Int64 accountid,
        [Description("Monitor Id used to stop or pause")] Int64 monitorid, 
        [Description("The CRON formatted frequency which is limited to these options 3 minutes, 15 minutes, 30 minutes, hourly or daily")] string cronfrequency)
    {

        StatusNowService objstatusNowService = new StatusNowService(accesstoken,accountid);

        var result = await objstatusNowService.ResumeMonitor(monitorid, cronfrequency);
        return result;

    }


    [McpServerTool(Name = "URLCheck"), Description("Check if the URL has any SSL certifcate expiry or domain expiry or ping or response delay issues")]
    public static async Task<string> URLCheck(
        StatusNowService statusNowService, 
        string accesstoken, 
        Int64 accountid,
        [Description("URL or web address to investigate")] string url)
    {

        StatusNowService objstatusNowService = new StatusNowService(accesstoken,accountid);

        var result = await objstatusNowService.CertCheck(url);
        result += await objstatusNowService.DomainCheck(url);
        result += await objstatusNowService.PingCheck(url);       
        result += await objstatusNowService.HttpCheck(url);  
        
        return result;  

    }


}