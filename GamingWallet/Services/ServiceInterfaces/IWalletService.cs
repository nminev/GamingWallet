using GamingWallet.Models;

namespace GamingWallet.Services.ServiceInterfaces;

/// <summary>
/// Service that can perform operations on a wallet, such as depositing, withdrawing, and checking the balance.
/// </summary>
public interface IWalletService
{
    /// <summary>
    /// Deposits the specified amount into the wallet and returns the result of the transaction.
    /// </summary>
    /// <param name="amount">The amount to deposit.</param>
    /// <returns>The result of the transaction.</returns>
    ITransactionResult Deposit(decimal amount);

    /// <summary>
    /// Withdraws the specified amount from the wallet and returns the result of the transaction.
    /// </summary>
    /// <param name="amount">The amount to withdraw.</param>
    /// <returns>The result of the transaction.</returns>
    ITransactionResult Withdraw(decimal amount);

    /// <summary>
    /// Withdraws the specified amount and transfers it to the house.
    /// Essentially this is called when player loses money.
    /// </summary>
    /// <param name="amount">The amount to withdraw.</param>
    /// <returns>The result of the transaction.</returns>
    ITransactionResult WithdrawForTheHouse(decimal amount);

    /// <summary>
    /// Returns the current balance of the wallet.
    /// </summary>
    /// <returns>The current balance of the wallet.</returns>
    decimal Balance();
}