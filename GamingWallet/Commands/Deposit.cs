using GamingWallet.Services.ServiceInterfaces;

namespace GamingWallet.Commands;

/// <summary>
/// Command to trigger deposit action.
/// </summary>
public class DepositCommand : ICommand
{
    /// <summary>
    /// Amount to deposit.
    /// </summary>
    public decimal Amount { get; }

    public DepositCommand(decimal amount)
    {
        Amount = amount;
    }
}

/// <summary>
/// Handles deposit commands by depositing the specified amount into the wallet.
/// </summary>
public class DepositCommandHandler : ICommandHandler<DepositCommand>
{
    private readonly IWalletService _walletService;
    private readonly IUserOutputService _userOutputService;
    
    public DepositCommandHandler(IWalletService walletService, IUserOutputService userOutputService)
    {
        _walletService = walletService;
        _userOutputService = userOutputService;
    }

    /// <summary>
    /// Handles the specified deposit command by depositing the specified amount into the wallet.
    /// If the deposit is successful, a success message is printed to the user.
    /// If the deposit fails, the error messages are printed to the user.
    /// </summary>
    /// <param name="command">The deposit command to handle. This must be of type <see cref="DepositCommand"/>.</param>
    public void Handle(DepositCommand command)
    {
        var depositAmount = command.Amount;

        if (depositAmount < 0)
        {
            _userOutputService.PrintErrorMessage("Deposit amount cannot be negative.");
            return;
        }

        var result = _walletService.Deposit(depositAmount);
        if (result.Success)
        {
            _userOutputService.PrintDepositSuccessful(depositAmount, result.NewBalance);
        }
        else
        {
            foreach (var errorMessage in result.ErrorMessage)
            {
                _userOutputService.PrintErrorMessage(errorMessage);
            }
        }
    }

}