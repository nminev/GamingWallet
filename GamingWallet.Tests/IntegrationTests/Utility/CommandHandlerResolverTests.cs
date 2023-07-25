using GamingWallet.Commands;
using GamingWallet.Models;
using GamingWallet.Services;
using GamingWallet.Services.ServiceInterfaces;
using GamingWallet.Utility;
using Microsoft.Extensions.DependencyInjection;

namespace GamingWallet.Tests.IntegrationTests.Utility;
public class CommandHandlerResolverTests
{
    [Fact]
    public void Resolve_ShouldReturnCorrectHandler_WhenHandlerIsRegistered()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddTransient<ICommandHandler<DepositCommand>, DepositCommandHandler>();
        serviceCollection.AddSingleton<IWallet, Wallet>();
        serviceCollection.AddTransient<IWalletService, WalletService>();
        serviceCollection.AddSingleton<IUserOutputService, UserOutputService>();
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var resolver = new CommandHandlerResolver(serviceProvider);

        // Act
        var handler = resolver.Resolve<DepositCommand>();

        // Assert
        Assert.IsType<DepositCommandHandler>(handler);
    }

    [Fact]
    public void Resolve_ShouldReturnCorrectHandler_WhenPlayCommandHandlerIsRegistered()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddTransient<ICommandHandler<PlayCommand>, PlayCommandHandler>();
        serviceCollection.AddSingleton<IWallet, Wallet>();
        serviceCollection.AddTransient<IWalletService, WalletService>();
        serviceCollection.AddSingleton<IUserOutputService, UserOutputService>();
        serviceCollection.AddTransient<IRoundService, RoundService>();
        serviceCollection.AddTransient<IRandomNumberGenerator, RandomNumberGenerator>();
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var resolver = new CommandHandlerResolver(serviceProvider);

        // Act
        var handler = resolver.Resolve<PlayCommand>();

        // Assert
        Assert.IsType<PlayCommandHandler>(handler);
    }

    [Fact]
    public void Resolve_ShouldReturnCorrectHandler_WhenWithdrawCommandHandlerIsRegistered()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddTransient<ICommandHandler<WithdrawCommand>, WithdrawCommandHandler>();
        serviceCollection.AddSingleton<IWallet, Wallet>();
        serviceCollection.AddTransient<IWalletService, WalletService>();
        serviceCollection.AddSingleton<IUserOutputService, UserOutputService>();
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var resolver = new CommandHandlerResolver(serviceProvider);

        // Act
        var handler = resolver.Resolve<WithdrawCommand>();

        // Assert
        Assert.IsType<WithdrawCommandHandler>(handler);
    }

}
