using GamingWallet.Services.ServiceInterfaces;

namespace GamingWallet.Services;

/// <inheritdoc/>
public class UserInputService : IUserInputService
{
    
    /// <inheritdoc/>
    public decimal GetDecimalInput(string prompt)
    {
        Console.WriteLine(prompt);
        string? input = Console.ReadLine();

        if (decimal.TryParse(input, out var result))
        {
            return result;
        }

        Console.WriteLine("Invalid input. Please enter a number.");
        return GetDecimalInput(prompt);
    }

    /// <inheritdoc/>
    public (string?, decimal?) GetStringInput(string prompt)
    {
        Console.WriteLine(prompt);
        string? input = Console.ReadLine();

        // Try to split the input into a command and a number
        string[]? parts = input?.Split(' ');
        if (parts is { Length: 2 } && decimal.TryParse(parts[1], out decimal number))
        {
            return (parts[0], number);
        }

        // If the input couldn't be split into a command and a number, return just the command
        return (input, null);
    }
}