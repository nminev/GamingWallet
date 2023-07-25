namespace GamingWallet.Services.ServiceInterfaces
{
    public interface IUserOutputService
    {
        /// <summary>
        /// Prints the current balance to the console.
        /// </summary>
        /// <param name="currentBalance">The current balance to print.</param>
        void PrintCurrentBalance(decimal currentBalance);

        /// <summary>
        /// Prints a message to the console indicating that a deposit was successful.
        /// </summary>
        /// <param name="amount">The amount that was deposited.</param>
        /// <param name="balance">The new balance after the deposit.</param>
        void PrintDepositSuccessful(decimal amount, decimal balance);

        /// <summary>
        /// Prints an error message to the console.
        /// </summary>
        /// <param name="message">The error message to print.</param>
        void PrintErrorMessage(string message);

        /// <summary>
        /// Prints a message to the console indicating that the user lost a bet.
        /// </summary>
        void PrintLostBet();

        /// <summary>
        /// Prints a message to the console indicating that a withdrawal was successful.
        /// </summary>
        /// <param name="amount">The amount that was withdrawn.</param>
        /// <param name="balance">The new balance after the withdrawal.</param>
        void PrintWithdrawSuccessful(decimal amount, decimal balance);

        /// <summary>
        /// Prints a message to the console indicating that the user won a bet.
        /// </summary>
        /// <param name="winnings">The amount that the user won.</param>
        void PrintWonBet(decimal winnings);
    }
}