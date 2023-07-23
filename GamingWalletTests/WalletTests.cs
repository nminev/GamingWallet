using GamingWallet.Models;

namespace GamingWalletTests;
    
public class WalletTests
{
    [Fact]
    public void TestDeposit()
    {
        var wallet = new Wallet();
        wallet.Deposit(10);
        Assert.Equal(10, wallet.Balance);
    }

    [Fact]
    public void TestWithdraw()
    {
        var wallet = new Wallet();
        wallet.Deposit(10);
        var isSuccessful = wallet.Withdraw(5);
        Assert.True(isSuccessful);
        Assert.Equal(5, wallet.Balance);
    }

    [Fact]
    public void TestWithdrawInsufficientFunds()
    {
        var wallet = new Wallet();
        var isSuccessful = wallet.Withdraw(5);
        Assert.False(isSuccessful);
        Assert.Equal(0, wallet.Balance);
    }
}
