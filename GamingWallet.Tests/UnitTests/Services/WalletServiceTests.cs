using GamingWallet.Models;
using GamingWallet.Services;

namespace GamingWallet.Tests.UnitTests.Services;

public class WalletServiceTests
{
    [Fact]
    public void TestDeposit()
    {
        // Arrange
        var wallet = new WalletService(new Wallet());

        // Act
        wallet.Deposit(10);

        // Assert
        Assert.Equal(10, wallet.Balance());
    }

    [Fact]
    public void TestWithdraw()
    {

        // Arrange
        var wallet = new WalletService(new Wallet());
        wallet.Deposit(10);

        // Act
        var result = wallet.Withdraw(5);

        // Assert
        Assert.True(result.Success);
        Assert.Equal(5, wallet.Balance());
    }

    [Fact]
    public void TestWithdrawInsufficientFunds()
    {
        // Arrange
        var wallet = new WalletService(new Wallet());

        // Act
        var result = wallet.Withdraw(5);

        // Assert
        Assert.False(result.Success);
        Assert.Equal(0, wallet.Balance());
    }
}
