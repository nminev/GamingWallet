namespace GamingWallet.Services.ServiceInterfaces;

/// <summary>
/// The main entrypoint for the app. 
/// This is were the main game loop is managed and the user input is handled.
/// </summary>
public interface IGameService
{
    /// <summary>
    /// Runs the game.
    /// </summary>
    void RunGame();
}