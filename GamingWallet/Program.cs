using GamingWallet.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class Program
{
    public static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<Worker>();
            })
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                // configure app settings if needed
            })
            .ConfigureServices((_, services) =>
            {
                Startup startup = new Startup();
                startup.ConfigureServices(services);
            });
}
