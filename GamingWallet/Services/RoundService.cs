using GamingWallet.Services.ServiceInterfaces;
using GamingWallet.Utility;

namespace GamingWallet.Services;
public class RoundService : IRoundService
{
    private readonly IRandomNumberGenerator _rng;

    public RoundService(IRandomNumberGenerator rng)
    {
        _rng = rng;
    }

    public decimal PlayRound(decimal bet)
    {
        var roll = _rng.NextDouble();

        if (roll < 0.5)
        {
            return -bet;
        }
        else if (roll < 0.9)
        {
            return bet*2;
        }
        else
        {
            var winnings = bet * _rng.Next(2, 11);
            return winnings + bet;
        }
    }
}

