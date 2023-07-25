namespace GamingWallet.Services.ServiceInterfaces;

/// <summary>
/// Defines the contract for a service that can get user input.
/// </summary>
public interface IUserInputService
{
    /// <summary>
    /// Gets a decimal input from the user.
    /// </summary>
    /// <param name="prompt">The prompt to display to the user.</param>
    /// <returns>The decimal input entered by the user.</returns>
    decimal GetDecimalInput(string prompt);

    /// <summary>
    /// Gets a string input and an optional decimal input from the user.
    /// </summary>
    /// <param name="prompt">The prompt to display to the user.</param>
    /// <returns>A tuple containing the string input and the optional decimal input entered by the user.</returns>
    (string?, decimal?) GetStringInput(string prompt);
}