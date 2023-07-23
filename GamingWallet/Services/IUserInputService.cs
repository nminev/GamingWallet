namespace GamingWallet.Services
{
    public interface IUserInputService
    {
        decimal GetDecimalInput(string prompt);
        string GetStringInput(string prompt);
    }
}