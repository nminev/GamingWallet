using GamingWallet.Commands;
using GamingWallet.Models;
using GamingWallet.Services;
using GamingWallet.Services.ServiceInterfaces;
using GamingWallet.Utility;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<IRandomNumberGenerator, RandomNumberGenerator>();
        services.AddSingleton<IWallet, Wallet>();
        services.AddTransient<IWalletService, WalletService>();
        services.AddTransient<IRoundService, RoundService>();
        services.AddSingleton<IUserInputService,UserInputService>();
        services.AddSingleton<IUserOutputService, UserOutputService>();
        services.AddSingleton<IGameService, GameService>();
        services.AddSingleton<ICommandHandlerResolver, CommandHandlerResolver>();

        // Register all command handlers
        var commandHandlerTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommandHandler<>)));

        foreach (var type in commandHandlerTypes)
        {
            var command = type.GetInterfaces().First().GetGenericArguments().First();
            var serviceType = typeof(ICommandHandler<>).MakeGenericType(command);
            services.AddTransient(serviceType, type);
        }

        // Register all commands
        var commandTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.GetInterfaces().Contains(typeof(ICommand)));

        foreach (var type in commandTypes)
        {
            services.AddTransient(type);
        }
    }

}
