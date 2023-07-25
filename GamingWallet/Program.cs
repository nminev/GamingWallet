using GamingWallet.Services.ServiceInterfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GamingWallet;

/// <summary>
/// Contains the entry point for the application.
/// </summary>
public class Program
{
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    /// <param name="args">An array of command-line arguments.</param>
    public static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        var gameService = host.Services.GetRequiredService<IGameService>();
        gameService.RunGame();
    }

    /// <summary>
    /// Creates a host builder with pre-configured defaults.
    /// </summary>
    /// <param name="args">An array of command-line arguments.</param>
    /// <returns>An instance of <see cref="IHostBuilder"/> used to configure and run a host.</returns>
    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) =>
            {
                Startup startup = new Startup();
                startup.ConfigureServices(services);
            });
}
