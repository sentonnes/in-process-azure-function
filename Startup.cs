using System;
using Elastic.Apm.Azure.Functions;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

[assembly: FunctionsStartup(typeof(Sen.InProcess.Startup))]

namespace Sen.InProcess;

public class Options
{
}

internal class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.Services.AddOptions<Options>()
            .Configure<IConfiguration>((settings, configuration) =>
            {
                configuration.GetSection("Options").Bind(settings);
            });

        builder.AddElasticApm();
    }
}