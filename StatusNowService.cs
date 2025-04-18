using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net.Http.Headers;

namespace StatusNowFancyMCP;

public class StatusNowService
{

    string _accesstoken = "";
    Int64 _accountid = 0;

    public StatusNowService(string accesstoken, Int64 accountid)
    {
        // Constructor logic here
        string _accesstoken = accesstoken; 
        Int64 _accountid = accountid;
    }

    public async Task<List<MonitorsModel>> GetMonitors()
    {

        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("X-Api-Key", _accesstoken);
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri("https://statusnow.dev/api/Monitors/List?accountid=" + _accountid),
        };
        using (var response = await client.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();

            var monitors = JsonSerializer.Deserialize<List<MonitorsModel>>(body, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return monitors;

        }

    }

    public async Task<List<AlertsModel>> GetAlerts()
    {

        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("X-Api-Key", _accesstoken);
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri("https://statusnow.dev/api/Alerts/List?accountid=" + _accountid + "&shownew=true&showopen=true&showclosed=false"),
        };
        using (var response = await client.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();

            var alerts = JsonSerializer.Deserialize<List<AlertsModel>>(body, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return alerts;
        }

    }

}




public class MonitorsModel {

    public Int64 id { get; set; }

    public Int64? accountid { get; set; }

    public string? name { get; set; }

    public bool? isactive { get; set; } //1 = active, 0 = inactive

    public string? monitortype { get; set; } //HTTPCODE, SSL CERT, DOMAIN EXPIRY, PING, TCP PORT

    public MonitorConfiguration? configuration { get; set; }

    public string? createdby { get; set; }

    public DateTime? createdat { get; set; }

    public string? updatedby { get; set; }

    public DateTime? updatedat { get; set; }

    public DateTime? lastusedat { get; set; }

    public MonitorStatus? laststatus { get; set; }

}

public class MonitorStatus {

    public string? laststatus { get; set; }

    public string? sslstatus { get; set; }

    public string? domainstatus { get; set; }

    public string? pingstatus { get; set; }

    public string? sslexpiry { get; set; }

    public string? domainexpiry { get; set; }

    public string? httpresponsecodestatus { get; set; }

    public string? tcpportstatus { get; set; }

}

public class MonitorConfiguration {

    public string? url { get; set; }

    public string? frequency { get; set; } //Cron Expression: https://en.wikipedia.org/wiki/Cron#CRON_expression

    //# * * * * * <command to execute>
    //# | | | | |
    //# | | | | day of the week (0–6) (Sunday to Saturday; 
    //# | | | month (1–12)             7 is also Sunday on some systems)
    //# | | day of the month (1–31)
    //# | hour (0–23)
    //# minute (0–59)

    public string? httpmethod { get; set; } //GET, POST, PATCH, PUT, HEAD

    public bool? certexpiry { get; set; } //True or False (Will check all SSL and Cert checks and issues, including expiry)

    public bool? domainexpiry { get; set; } //True or False (Will check all domain expiry dates)

    public bool? pingstatus { get; set; } //True or False (Will check url ping, response status and times)

    public Int32? pingresponsetarget { get; set; } //Target response time, if this is exceeded we have an issue. 3 seconds = 3000 ms

    public bool? httpresponsestatus { get; set; }  //True or False (Will check response code)

    public string? httpresponsecode { get; set; } //What code are we expecting? 200, 201, 202, 204, 301, 302, 304, 400, 401, 403, 404, 405, 500, 501, 502, 503, 504

    public bool? tcpportstatus { get; set; } //True or False - (Will check for port status on host)

    public int? tcpportresponsetarget { get; set; } //Target response time, if this is exceeded we have an issue. 3 seconds = 3000 ms

    public string? tcpporthost {  get; set; } //Host IP or DNS name

    public string? tcpports {  get; set; } //TCP Port numbers

}

public class AlertsModel
{

    public Int64 id { get; set; }

    public Int64? accountid { get; set; }

    public Int64? monitorid { get; set; }

    public string? runid { get; set; }

    public Int64? occurrences { get; set; } //Number of occurrences

    public string? name { get; set; }

    public string? alertstatus { get; set; } //NEW, OPEN, CLOSED

    public string? alerttype { get; set; } //HTTPCODE, SSL CERT, DOMAIN EXPIRY, PING, TCP PORT

    public DateTime? createdat { get; set; }

    public string? updatedby { get; set; }

    public DateTime? updatedat { get; set; }

    public DateTime? closedat { get; set; }

}