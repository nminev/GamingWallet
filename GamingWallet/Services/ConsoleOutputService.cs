using GamingWallet.Services.ServiceInterfaces;

namespace GamingWallet.Services;
public class UserOutputService : IUserOutputService
{

    /// <inheritdoc/>
    public void PrintCurrentBalance(decimal currentBalance)
    {
        Console.WriteLine($"Current balance is: {currentBalance}");
    }

    /// <inheritdoc/>
    public void PrintDepositSuccessful(decimal amount, decimal balance)
    {
        Console.WriteLine($"Deposited {amount}. Current balance: {balance}");
    }

    /// <inheritdoc/>
    public void PrintErrorMessage(string message)
    {
        Console.WriteLine(message);
    }
    
    /// <inheritdoc/>
    public void PrintLostBet()
    {
        Console.WriteLine("You lost the bet");
    }

    /// <inheritdoc/>
    public void PrintWithdrawSuccessful(decimal amount, decimal balance)
    {
        Console.WriteLine($"Withdrew {amount}. Current balance: {balance}");
    }

    /// <inheritdoc/>
    public void PrintWonBet(decimal winnings)
    {
        Console.WriteLine($"You won the bet: {winnings}");
    }
}
