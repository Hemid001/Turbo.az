using Serilog;
using Serilog.Sinks.MSSqlServer;

namespace TurboProject.APILayer
{
    public static class ApiLayerConfig
    {
        public static void AddHybridLogging(this ILoggingBuilder loggingBuilder, IConfiguration configuration)
        {
            var logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
            .WriteTo.MSSqlServer(
        connectionString: configuration.GetConnectionString("Default"),
        sinkOptions: new MSSqlServerSinkOptions { TableName = "Logs", AutoCreateSqlTable = true },
        restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error
             )
    .CreateLogger();

            loggingBuilder.ClearProviders();
            loggingBuilder.AddSerilog(logger);

        }

        public static void AddRedisCache(this IServiceCollection services, IConfiguration configuration)
        {
            var redisConnection = configuration.GetConnectionString("Redis");

            if (string.IsNullOrEmpty(redisConnection))
                throw new ArgumentNullException("Redis connection string is not set in configuration.");

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = redisConnection;
                options.InstanceName = "TurboProject_";
            });
        }

    }
}
