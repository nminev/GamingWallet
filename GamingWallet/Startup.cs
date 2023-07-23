using GamingWallet.Models;
using GamingWallet.Services;
using Microsoft.Extensions.DependencyInjection;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<IRandomNumberGenerator, RandomNumberGenerator>();
        services.AddTransient<IWallet, Wallet>();
        services.AddTransient<IGame, Game>();
        services.AddHostedService<Worker>();
    }

}
