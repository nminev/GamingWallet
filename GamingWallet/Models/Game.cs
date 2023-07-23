namespace GamingWallet.Models;
public class Game : IGame
{
    private readonly IRandomNumberGenerator _rng;

    public Game(IRandomNumberGenerator rng)
    {
        _rng = rng;
    }

    public decimal PlayRound(decimal bet)
    {
        var roll = _rng.NextDouble();

        if (roll < 0.5)
        {
            Console.WriteLine("You lost the bet");
            return -bet;
        }
        else if (roll < 0.9)
        {
            Console.WriteLine("You won the bet");
            return bet;
        }
        else
        {
            var winnings = bet * _rng.Next(2, 11);
            Console.WriteLine($"You won big: {winnings}");
            return winnings;
        }
    }
}

