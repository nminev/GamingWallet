namespace GamingWallet.Services
{
    public interface IUserOutputService
    {
        void PrintCurrentBalance(decimal currentBalance);
        void PrintDeposit(decimal amount, decimal balance);
        void PrintInsufficientFunds();
        void PrintInvalidAction();
        void PrintInvalidBetAmount();
        void PrintLostBet();
        void PrintWithdraw(decimal amount, decimal balance);
        void PrintWonBet(decimal winnings);
    }
}