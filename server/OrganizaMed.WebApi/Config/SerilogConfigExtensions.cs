﻿using Serilog;

namespace OrganizaMed.WebApi.Config
{
    public static class SerilogConfigExtensions
    {
        public static void ConfigureSerilog(this IServiceCollection services, ILoggingBuilder logging)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            logging.ClearProviders();

            services.AddLogging(builder => builder.AddSerilog(dispose: true));
        }
    }
}
