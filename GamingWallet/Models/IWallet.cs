namespace GamingWallet.Models;

/// <summary>
/// Defines the contract for a wallet, which includes information about the current balance.
/// </summary>
public interface IWallet
{
    /// <summary>
    /// Gets or sets the current balance of the wallet.
    /// </summary>
    decimal Balance { get; set; }
}