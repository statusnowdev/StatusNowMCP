using System;
using System.ComponentModel;
using System.Text.Json;
using ModelContextProtocol.Server;

namespace StatusNowFancyMCP;

[McpServerToolType]
public class StatusNowMCPTools
{
    [McpServerTool, Description("Get a list of alerts")]
    public static async Task<string> GetAlertList(StatusNowService statusNowService, string accesstoken, Int64 accountid)
    {

        StatusNowService objstatusNowService = new StatusNowService(accesstoken,accountid);

        var alerts = await objstatusNowService.GetAlerts();
        return JsonSerializer.Serialize(alerts);
    }

    [McpServerTool, Description("Get a list of monitors")]
    public static async Task<string> GetMonitorList(StatusNowService statusNowService,string accesstoken, Int64 accountid)
    {

        StatusNowService objstatusNowService = new StatusNowService(accesstoken, accountid);

        var monitors = await objstatusNowService.GetMonitors();
        return JsonSerializer.Serialize(monitors);
    }

}