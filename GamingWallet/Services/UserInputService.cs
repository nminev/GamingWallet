using GamingWallet.Services.ServiceInterfaces;

namespace GamingWallet.Services;
public class UserInputService : IUserInputService
{
    public decimal GetDecimalInput(string prompt)
    {
        Console.WriteLine(prompt);
        string input = Console.ReadLine();

        if (decimal.TryParse(input, out decimal result))
        {
            return result;
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a number.");
            return GetDecimalInput(prompt);
        }
    }

    public (string, decimal?) GetStringInput(string prompt)
    {
        Console.WriteLine(prompt);
        string input = Console.ReadLine();

        // Try to split the input into a command and a number
        string[] parts = input.Split(' ');
        if (parts.Length == 2 && decimal.TryParse(parts[1], out decimal number))
        {
            return (parts[0], number);
        }

        // If the input couldn't be split into a command and a number, return just the command
        return (input, null);
    }

}
