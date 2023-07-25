using GamingWallet.Services.ServiceInterfaces;
using GamingWallet.Utility;

namespace GamingWallet.Services;

/// <inheritdoc/>
public class RoundService : IRoundService
{
    private readonly IRandomNumberGenerator _rng;
    
    public RoundService(IRandomNumberGenerator rng)
    {
        _rng = rng;
    }

    /// <summary>
    /// Plays a round of the game with the specified bet amount and returns the result.
    /// The result is determined randomly: there's a 50% chance of losing the bet, a 40% chance of doubling the bet, and a 10% chance of winning 2 to 10 times the bet.
    /// </summary>
    /// <param name="bet">The bet amount for the round.</param>
    /// <returns>The result of the round. This is a positive number if the player won, or a negative number if the player lost.</returns>
    public decimal PlayRound(decimal bet)
    {
        var roll = _rng.NextDouble();

        if (roll < 0.5)
        {
            return -bet;
        }
        else if (roll < 0.9)
        {
            return bet * 2;
        }
        else
        {
            var winnings = bet * _rng.Next(2, 11);
            return winnings + bet;
        }
    }
}