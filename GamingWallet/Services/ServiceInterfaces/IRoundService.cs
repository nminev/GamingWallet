namespace GamingWallet.Services.ServiceInterfaces;

/// <summary>
/// Provides a service for playing rounds of the game with a specified bet amount.
/// </summary>
public interface IRoundService
{
    /// <summary>
    /// Plays a round of the game with the specified bet amount and returns the result.
    /// </summary>
    /// <param name="bet">The bet amount for the round.</param>
    decimal PlayRound(decimal bet);
}