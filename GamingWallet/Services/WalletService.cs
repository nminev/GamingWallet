using GamingWallet.Services.ServiceInterfaces;

namespace GamingWallet.Models;

public class WalletService : IWalletService
{
    private IWallet _wallet;
    public WalletService(IWallet wallet)
    {
        _wallet = wallet;
    }

    public ITransactionResult Deposit(decimal amount)
    {
        _wallet.Balance += amount;
        return new TransactionResult
        {
            Success = true,
            NewBalance = _wallet.Balance
        };
    }

    public ITransactionResult Withdraw(decimal amount)
    {
        return DeductAmount(amount);
    }

    public ITransactionResult HouseWithdraw(decimal amount)
    {
        return DeductAmount(amount);
    }

    private ITransactionResult DeductAmount(decimal amount)
    {
        if (_wallet.Balance < amount)
        {
            return new TransactionResult
            {
                Success = false,
                NewBalance = _wallet.Balance,
                ErrorMessage = { "Insufficient funds" }
            };
        }

        _wallet.Balance -= amount;
        return new TransactionResult
        {
            Success = true,
            NewBalance = _wallet.Balance
        };
    }

    public decimal Balance()
    {
        return _wallet.Balance;
    }
}

