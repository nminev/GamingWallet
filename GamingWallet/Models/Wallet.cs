namespace GamingWallet.Models;
public class Wallet : IWallet
{
    public decimal Balance { get; private set; }

    public void Deposit(decimal amount)
    {
        Balance += amount;
        Console.WriteLine($"Deposited {amount}. Current balance: {Balance}");
    }

    public bool Withdraw(decimal amount)
    {
        if (Balance < amount)
        {
            Console.WriteLine("Insufficient funds");
            return false;
        }

        Balance -= amount;
        Console.WriteLine($"Withdrew {amount}. Current balance: {Balance}");
        return true;
    }
}

