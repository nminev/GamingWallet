namespace GamingWallet.Services.ServiceInterfaces
{
    public interface IUserInputService
    {
        decimal GetDecimalInput(string prompt);
        string GetStringInput(string prompt);
    }
}