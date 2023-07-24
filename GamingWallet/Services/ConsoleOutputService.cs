﻿using GamingWallet.Models;
using GamingWallet.Services.ServiceInterfaces;

namespace GamingWallet.Services;
public class UserOutputService : IUserOutputService
{

    public void PrintCurrentBalance(decimal currentBalance)
    {
        Console.WriteLine($"Current balance is: {currentBalance}");
    }

    public void PrintDepositSuccessfull(decimal amount, decimal balance)
    {
        Console.WriteLine($"Deposited {amount}. Current balance: {balance}");
    }

    public void PrintErrorMessage(string meessage)
    {
        Console.WriteLine(meessage);
    }
    public void PrintLostBet()
    {
        Console.WriteLine("You lost the bet");
    }

    public void PrintWithdrawSuccessfull(decimal amount, decimal balance)
    {
        Console.WriteLine($"Withdrew {amount}. Current balance: {balance}");
    }

    public void PrintWonBet(decimal winnings)
    {
        Console.WriteLine($"You won the bet: {winnings}");
    }
}
