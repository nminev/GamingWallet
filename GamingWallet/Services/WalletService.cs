using GamingWallet.Models;
using GamingWallet.Services.ServiceInterfaces;

namespace GamingWallet.Services;

/// <inheritdoc/>
public class WalletService : IWalletService
{
    private readonly IWallet _wallet;
    public WalletService(IWallet wallet)
    {
        _wallet = wallet;
    }
    
    /// <inheritdoc/>
    public ITransactionResult Deposit(decimal amount)
    {
        _wallet.Balance += amount;
        return new TransactionResult
        {
            Success = true,
            NewBalance = _wallet.Balance
        };
    }

    /// <inheritdoc/>
    public ITransactionResult Withdraw(decimal amount)
    {
        return DeductAmount(amount);
    }

    /// <inheritdoc/>
    public ITransactionResult WithdrawForTheHouse(decimal amount)
    {
        return DeductAmount(amount);
    }
    
    /// <inheritdoc/>
    public decimal Balance()
    {
        return _wallet.Balance;
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
}

