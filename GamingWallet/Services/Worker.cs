using GamingWallet.Models;
using Microsoft.Extensions.Hosting;

namespace GamingWallet.Services;
public class Worker : BackgroundService
{
    private readonly IWallet _wallet;
    private readonly IGame _game;

    public Worker(IWallet wallet, IGame game)
    {
        _wallet = wallet;
        _game = game;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            Console.WriteLine("Choose an action: [d]eposit, [w]ithdraw, [p]lay, [q]uit");
            var action = Console.ReadLine();

            switch (action)
            {
                case "d":
                    Console.WriteLine("Enter amount to deposit: ");
                    var depositAmount = decimal.Parse(Console.ReadLine());
                    _wallet.Deposit(depositAmount);
                    break;
                case "w":
                    Console.WriteLine("Enter amount to withdraw: ");
                    var withdrawAmount = decimal.Parse(Console.ReadLine());
                    _wallet.Withdraw(withdrawAmount);
                    break;
                case "p":
                    Console.WriteLine("Enter bet amount: ");
                    var betAmount = decimal.Parse(Console.ReadLine());
                    if (betAmount < 1 || betAmount > 10)
                    {
                        Console.WriteLine("Invalid bet amount. Must be between 1 and 10");
                        break;
                    }
                    var result = _game.PlayRound(betAmount);
                    _wallet.Withdraw(betAmount);
                    if (result > 0)
                    {
                        _wallet.Deposit(result);
                    }
                    break;
                case "q":
                    return;
                default:
                    Console.WriteLine("Invalid action");
                    break;
            }

            await Task.Delay(1000, stoppingToken);
        }
    }
}
