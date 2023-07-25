using GamingWallet.Services.ServiceInterfaces;

namespace GamingWallet.Commands;

/// <summary>
/// Command to trigger withdraw action.
/// </summary>
public class WithdrawCommand : ICommand
{
    /// <summary>
    /// Amount to withdraw.
    /// </summary>
    public decimal Amount { get; }

    public WithdrawCommand(decimal amount)
    {
        Amount = amount;
    }
}

/// <summary>
/// Handles withdraw commands by withdrawing the specified amount from the wallet.
/// </summary>
public class WithdrawCommandHandler : ICommandHandler<WithdrawCommand>
{
    private readonly IWalletService _walletService;
    private readonly IUserOutputService _userOutputService;

    public WithdrawCommandHandler(IWalletService walletService, IUserOutputService userOutputService)
    {
        _walletService = walletService;
        _userOutputService = userOutputService;
    }

    /// <summary>
    /// Handles the specified withdraw command by withdrawing the specified amount from the wallet.
    /// If the withdraw is successful, a success message is printed to the user.
    /// If the withdraw fails, the error messages are printed to the user.
    /// </summary>
    /// <param name="command">The withdraw command to handle. This must be of type <see cref="WithdrawCommand"/>.</param>
    public void Handle(WithdrawCommand command)
    {
        decimal withdrawAmount = command.Amount;
        if (withdrawAmount < 0)
        {
            _userOutputService.PrintErrorMessage("Invalid withdraw amount.");
            return;
        }

        var result = _walletService.Withdraw(withdrawAmount);
        if (result.Success)
        {
            _userOutputService.PrintWithdrawSuccessful(withdrawAmount, result.NewBalance);
        }
        else
        {
            _userOutputService.PrintErrorMessage(string.Join(Environment.NewLine, result.ErrorMessage));
        }
    }
}
