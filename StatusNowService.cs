using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net.Http.Headers;

namespace StatusNowFancyMCP;

public class StatusNowService
{

    public string _accesstoken = Environment.GetEnvironmentVariable("accesstoken") != null ? Convert.ToString(Environment.GetEnvironmentVariable("accesstoken")) : "";
    public Int64 _accountid = Environment.GetEnvironmentVariable("accountid") != null ? Convert.ToInt64(Environment.GetEnvironmentVariable("accountid")) : 0;


    public StatusNowService()
    {
        //// Correctly initialize the fields
        //_accesstoken = accesstoken; 
        //_accountid = accountid;

        Console.WriteLine("StatusNowService constructor called");
        Console.WriteLine("AccessToken: " + _accesstoken);
        Console.WriteLine("AccountId: " + _accountid);
    }

    public async Task<List<MonitorsModel>> GetMonitors()
    {

        Console.WriteLine("Get Monitors Called with AccessToken: " + _accesstoken + " and AccountId: " + _accountid);

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

        Console.WriteLine("Get Alerts Called with AccessToken: " + _accesstoken + " and AccountId: " + _accountid);

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

    public async Task<string> PauseMonitor(Int64 monitorid)
    {

        Console.WriteLine("Pause Monitor Called with AccessToken: " + _accesstoken + " and AccountId: " + _accountid + " and MonitorId: " + monitorid);

        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("X-Api-Key", _accesstoken);
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("https://statusnow.dev/api/Monitors/" + monitorid + "/Pause?accountid=" + _accountid),
        };

        using (var response = await client.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();

            return body;
        }

    }   

    public async Task<string> ResumeMonitor(Int64 monitorid) {

        Console.WriteLine("Resume Monitor Called with AccessToken: " + _accesstoken + " and AccountId: " + _accountid + " and MonitorId: " + monitorid );

        //Default to 3min interval
        string cronfrequency = "*/3 * * * *";

        //if (cronfrequency == null || cronfrequency == "") {
        //    cronfrequency = "*/3 * * * *"; //Default to 3 minutes if no frequency is provided
        //}
        //if (cronfrequency =="0 * * * *") {
        //    cronfrequency = "*/3 * * * *"; //Default to 3 minutes if no frequency is provided
        //}

        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("X-Api-Key", _accesstoken);
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("https://statusnow.dev/api/Monitors/" + monitorid + "/Resume?accountid=" + _accountid + "&frequency=" + Uri.EscapeDataString(cronfrequency)),
        };

        using (var response = await client.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();

            return body;
        }

    }

    public async Task<string> CertCheck(string urltocheck)
    {

        Console.WriteLine("Cert Check Called with AccessToken: " + _accesstoken + " and AccountId: " + _accountid);

        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("X-Api-Key", _accesstoken);
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri("https://statusnow.dev/api/Monitors/CertCheck?url=" + urltocheck),
        };

        using (var response = await client.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();

            if (body == "" || body == "[]"){
                body = "Success: No certificate issues found";
            }

            return body;
        }

    }

    public async Task<string> DomainCheck(string urltocheck)
    {

        Console.WriteLine("Domain Check Called with AccessToken: " + _accesstoken + " and AccountId: " + _accountid);

        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("X-Api-Key", _accesstoken);
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri("https://statusnow.dev/api/Monitors/DomainCheck?url=" + urltocheck),
        };

        using (var response = await client.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();

            if (body == "" || body == "[]"){
                body = "Success: No domain issues found";
            }

            return body;
        }

    }

    public async Task<string> PingCheck(string urltocheck)
    {

        Console.WriteLine("Ping Check Called with AccessToken: " + _accesstoken + " and AccountId: " + _accountid);

        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("X-Api-Key", _accesstoken);
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri("https://statusnow.dev/api/Monitors/PingCheck?url=" + urltocheck),
        };

        using (var response = await client.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();

            if (body == "" || body == "[]"){
                body = "Success: No ping/site issues found";
            }

            return body;
        }

    }

    public async Task<string> HttpCheck(string urltocheck)
    {

        Console.WriteLine("Http Check Called with AccessToken: " + _accesstoken + " and AccountId: " + _accountid);

        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("X-Api-Key", _accesstoken);
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri("https://statusnow.dev/api/Monitors/HttpCodeCheck?url=" + urltocheck),
        };

        using (var response = await client.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();

            if (body == "" || body == "[]"){
                body = "Success: No http issues found";
            }

            return body;
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