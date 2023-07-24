namespace GamingWallet.Services.ServiceInterfaces
{
    public interface IUserInputService
    {
        decimal GetDecimalInput(string prompt);
        (string, decimal?) GetStringInput(string prompt);
    }
}