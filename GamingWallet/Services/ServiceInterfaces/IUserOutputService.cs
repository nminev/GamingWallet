namespace GamingWallet.Services.ServiceInterfaces
{
    public interface IUserOutputService
    {
        void PrintCurrentBalance(decimal currentBalance);
        void PrintDepositSuccessfull(decimal amount, decimal balance);
        void PrintErrorMessage(string errorMessage);
        void PrintLostBet();
        void PrintWithdrawSuccessfull(decimal amount, decimal balance);
        void PrintWonBet(decimal winnings);
    }
}