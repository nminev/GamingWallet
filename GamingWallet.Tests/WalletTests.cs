using GamingWallet.Models;

namespace GamingWallet.Tests;
    
public class WalletTests
{
    [Fact]
    public void TestDeposit()
    {
        var wallet = new WalletService(new Wallet());
        wallet.Deposit(10);
        Assert.Equal(10, wallet.Balance());
    }

    [Fact]
    public void TestWithdraw()
    {
        var wallet = new WalletService(new Wallet());
        wallet.Deposit(10);
        var result = wallet.Withdraw(5);
        Assert.True(result.Success);
        Assert.Equal(5, wallet.Balance());
    }

    [Fact]
    public void TestWithdrawInsufficientFunds()
    {
        var wallet = new WalletService(new Wallet());
        var result = wallet.Withdraw(5);
        Assert.False(result.Success);
        Assert.Equal(0, wallet.Balance());
    }
}
