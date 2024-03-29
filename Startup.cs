﻿using Elastic.Apm.Azure.Functions;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Sen.InProcess.Startup))]

namespace Sen.InProcess;

public class Options
{
}

internal class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.AddElasticApm();
    }
}