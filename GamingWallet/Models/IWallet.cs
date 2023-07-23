namespace GamingWallet.Models
{
    public interface IWallet
    {
        decimal Balance { get; }

        void Deposit(decimal amount);
        bool Withdraw(decimal amount);
    }
}