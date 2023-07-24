namespace GamingWallet.Models
{
    public interface ITransactionResult
    {
        bool Success {   get; set; }
        decimal NewBalance { get; set; }
        IList<string> ErrorMessage { get; set; }
    }
}