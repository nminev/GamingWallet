namespace GamingWallet.Models;

/// <summary>
/// Defines the contract for a transaction result, which includes information about whether the transaction was successful, the new balance after the transaction, and any error messages.
/// </summary>
public interface ITransactionResult
{
    /// <summary>
    /// Gets a value indicating whether the transaction was successful.
    /// </summary>
    bool Success { get; }

    /// <summary>
    /// Gets the new balance after the transaction.
    /// </summary>
    decimal NewBalance { get; }

    /// <summary>
    /// Gets a list of error messages that occurred during the transaction. This is populated only if <see cref="Success"/> is <c>false</c>.
    /// </summary>
    IList<string> ErrorMessage { get; }
}