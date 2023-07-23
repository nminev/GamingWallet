namespace GamingWallet.Models
{
    public interface IWalletService
    {
        void Deposit(decimal amount);
        bool Withdraw(decimal amount);
        decimal Balance();
    }
}