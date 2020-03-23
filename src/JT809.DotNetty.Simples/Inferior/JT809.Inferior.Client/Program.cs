using JT809.DotNetty.Core;
using JT809.DotNetty.Core.Handlers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using JT809.Protocol;
using JT809.Protocol.Interfaces;

namespace JT809.Inferior.Client
{
    public class DefaultConfig : JT809GlobalConfigBase
    {
        public override string ConfigId => "test";
    }
    class Program
    {
        static async Task Main(string[] args)
        {
            var serverHostBuilder = new HostBuilder()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.SetBasePath(AppDomain.CurrentDomain.BaseDirectory);
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                })
                .ConfigureLogging((context, logging) =>
                {
                    logging.AddConsole();
                    logging.SetMinimumLevel(LogLevel.Trace);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<ILoggerFactory, LoggerFactory>();
                    services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
                    services.AddJT809Configure(new DefaultConfig
                    {
                        HeaderOptions = new Protocol.Configs.JT809HeaderOptions
                        {
                            MsgGNSSCENTERID = 100210,
                        }
                    });
                    services.AddSingleton<JT809Serializer>();
                    services.AddJT809Core(hostContext.Configuration)
                            .AddJT809InferiorPlatformClient();
                    services.AddHostedService<JT809InferiorService>();
                });
            await serverHostBuilder.RunConsoleAsync();
        }
    }
}
