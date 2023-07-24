using GamingWallet.Models;
using GamingWallet.Services;
using GamingWallet.Services.ServiceInterfaces;
using GamingWallet.Utility;
using Microsoft.Extensions.DependencyInjection;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<IRandomNumberGenerator, RandomNumberGenerator>();
        services.AddTransient<IWallet, Wallet>();
        services.AddTransient<IWalletService, WalletService>();
        services.AddTransient<IRoundService, RoundService>();
        services.AddSingleton<IUserInputService,UserInputService>();
        services.AddSingleton<IUserOutputService, UserOutputService>();
        services.AddSingleton<IGameService, GameService>();
    }

}
