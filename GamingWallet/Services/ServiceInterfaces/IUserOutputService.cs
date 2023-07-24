namespace GamingWallet.Services.ServiceInterfaces
{
    public interface IUserOutputService
    {
        void PrintCurrentBalance(decimal currentBalance);
        void PrintDeposit(decimal amount, decimal balance);
        void PrintMessage(string errorMessage);
        void PrintLostBet();
        void PrintWithdraw(decimal amount, decimal balance);
        void PrintWonBet(decimal winnings);
    }
}