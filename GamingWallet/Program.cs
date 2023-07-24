using GamingWallet.Services.ServiceInterfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class Program
{
    public static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        var gameService = host.Services.GetRequiredService<IGameService>();
        gameService.RunGame();

        host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) =>
            {
                Startup startup = new Startup();
                startup.ConfigureServices(services);
            });
}
