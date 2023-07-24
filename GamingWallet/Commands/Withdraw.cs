using GamingWallet.Services.ServiceInterfaces;

namespace GamingWallet.Commands;

public class WithdrawCommand : ICommand
{
    public decimal Amount { get; }

    public WithdrawCommand(decimal amount)
    {
        Amount = amount;
    }
}


public class WithdrawCommandHandler : ICommandHandler<WithdrawCommand>
{
    private readonly IWalletService _walletService;
    private readonly IUserOutputService _userOutputService;

    public WithdrawCommandHandler(IWalletService walletService, IUserOutputService userOutputService)
    {
        _walletService = walletService;
        _userOutputService = userOutputService;
    }

    public void Handle(WithdrawCommand command)
    {
        decimal withdrawAmount = command.Amount;
        if (withdrawAmount < 0)
        {
            _userOutputService.PrintErrorMessage("Invalid withdraw ammount.");
            return;
        }

        var result = _walletService.Withdraw(withdrawAmount);
        if (result.Success)
        {
            _userOutputService.PrintWithdrawSuccessfull(withdrawAmount, result.NewBalance);
        }
        else
        {
            _userOutputService.PrintErrorMessage(string.Join(Environment.NewLine, result.ErrorMessage));
        }
    }
}
