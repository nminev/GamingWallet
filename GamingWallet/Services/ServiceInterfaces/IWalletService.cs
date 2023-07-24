using GamingWallet.Models;

namespace GamingWallet.Services.ServiceInterfaces
{
    public interface IWalletService
    {
        ITransactionResult Deposit(decimal amount);
        ITransactionResult Withdraw(decimal amount);
        ITransactionResult HouseWithdraw(decimal amount);
        decimal Balance();
    }
}