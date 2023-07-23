using GamingWallet.Models;

namespace GamingWallet.Services;
public class UserOutputService : IUserOutputService
{
    public void PrintDeposit(decimal amount, decimal balance)
    {
        Console.WriteLine($"Deposited {amount}. Current balance: {balance}");
    }

    public void PrintWithdraw(decimal amount, decimal balance)
    {
        Console.WriteLine($"Withdrew {amount}. Current balance: {balance}");
    }

    public void PrintInsufficientFunds()
    {
        Console.WriteLine("Insufficient funds");
    }

    public void PrintLostBet()
    {
        Console.WriteLine("You lost the bet");
    }

    public void PrintWonBet(decimal winnings)
    {
        Console.WriteLine($"You won the bet: {winnings}");
    }
    public void PrintInvalidBetAmount()
    {
        Console.WriteLine("Invalid bet amount. Must be between 1 and 10");
    }

    public void PrintInvalidAction()
    {
        Console.WriteLine("Invalid action");
    }

    public void PrintCurrentBalance(decimal currentBalance)
    {
        Console.WriteLine($"Current balance is: {currentBalance}");
    }
}
