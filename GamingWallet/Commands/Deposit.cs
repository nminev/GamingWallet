using GamingWallet.Commands;
using GamingWallet.Services.ServiceInterfaces;


public class DepositCommand : ICommand
{
    public decimal Amount { get; }

    public DepositCommand(decimal amount)
    {
        Amount = amount;
    }
}

public class DepositCommandHandler : ICommandHandler<DepositCommand>
{
    private readonly IWalletService _walletService;
    private readonly IUserOutputService _userOutputService;

    public DepositCommandHandler(IWalletService walletService, IUserOutputService userOutputService)
    {
        _walletService = walletService;
        _userOutputService = userOutputService;
    }

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
            _userOutputService.PrintDepositSuccessfull(depositAmount, result.NewBalance);
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

