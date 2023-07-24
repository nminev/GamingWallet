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

    public string GetStringInput(string prompt)
    {
        Console.WriteLine(prompt);
        return Console.ReadLine();
    }
}
