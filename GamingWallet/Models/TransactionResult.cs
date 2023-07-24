
namespace GamingWallet.Models;
public class TransactionResult : ITransactionResult
{
    public bool Success { get; set; }
    public decimal NewBalance { get; set; }
    public IList<string> ErrorMessage { get; set; } = new List<string>();
}
    