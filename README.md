# StatusNow.Dev - MCP Server for StatusNow.dev Website and Server Monitoring Tools
Monitor your websites, servers, and services with our comprehensive monitoring solution. Track SSL expiration, domain expiration, website uptime, response times, ping monitoring, and port monitoring.

Free account sign-up: https://statusnow.dev/sign-up.html

<hr>

Download the MCP Server and run it locally (dotnet) on Windows, Linux or Mac or via a Docker image.

<code>
{
    "servers": {
        "statusnowmcp": {
            "type": "stdio",
            "command": "dotnet",
            "args": [
                "run",
                "--project",
                "<PATH_TO_PROJECT>\\StatusNowMCP.csproj",
                "--accountid",
                "<YOUR_ACCOUNTID>",
                "--accounttoken",
                "<YOUR_STATUSNOW_ACCESSTOKEN>"
            ]
        }
    }
}
</code>

You can run the MCP Inspector using this command:

<code>
In your Terminal >>  npx @modelcontectprotocol./inspector dotnet run
</code>
