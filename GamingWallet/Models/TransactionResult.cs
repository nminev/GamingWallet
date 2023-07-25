
namespace GamingWallet.Models;

/// <inheritdoc/>
public class TransactionResult : ITransactionResult
{
    /// <inheritdoc/>
    public bool Success { get; set; }
    
    /// <inheritdoc/>
    public decimal NewBalance { get; set; }
    
    /// <inheritdoc/>
    public IList<string> ErrorMessage { get; set; } = new List<string>();
}
    