using System;
using System.Net;
using System.Net.NetworkInformation;
using Elastic.Apm.Azure.Functions;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

[assembly: FunctionsStartup(typeof(Sen.InProcess.Startup))]

namespace Sen.InProcess;

internal class Startup : FunctionsStartup
{
    public static string CanPingElasticServer;

    static string PingUrl(string url)
    {
        try
        {
            Ping ping = new Ping();
            IPAddress ipAddress = null;
            bool isValidIp = System.Net.IPAddress.TryParse(url, out ipAddress);

            PingReply reply = isValidIp ? ping.Send(url) : ping.Send(new Uri(url).Host);

            return $"Ping succeeded:{reply.Status.ToString()}";
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex}");
            return $"An error occurred: {ex}";
        }
    }

    public override void Configure(IFunctionsHostBuilder builder)
    {
        string elasticAddress = Environment.GetEnvironmentVariable("ELASTIC_APM_SERVER_URL") ??
                                "https://ukho-nonlive-elastic.apm.uksouth.azure.elastic-cloud.com";
        CanPingElasticServer = PingUrl(elasticAddress);

        builder.AddElasticApm();
    }
}