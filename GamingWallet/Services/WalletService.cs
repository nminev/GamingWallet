using GamingWallet.Services.ServiceInterfaces;

namespace GamingWallet.Models;

public class WalletService : IWalletService
{
    private IWallet _wallet;
    public WalletService(IWallet wallet)
    {
        _wallet = wallet;
    }

    public void Deposit(decimal amount)
    {
        _wallet.Balance += amount;
    }

    public bool Withdraw(decimal amount)
    {
        if (_wallet.Balance < amount)
        {
            return false;
        }

        _wallet.Balance -= amount;
        return true;
    }

    public decimal Balance()
    {
        return _wallet.Balance;
    }
}

