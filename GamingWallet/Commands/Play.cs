using GamingWallet.Services;
using GamingWallet.Services.ServiceInterfaces;

namespace GamingWallet.Commands;

public class PlayCommand : ICommand
{
    public decimal BetAmount { get; }

    public PlayCommand(decimal betAmount)
    {
        BetAmount = betAmount;
    }
}

public class PlayCommandHandler : ICommandHandler<PlayCommand>
{
    private readonly IWalletService _walletService;
    private readonly IRoundService _roundService;
    private readonly IUserOutputService _userOutputService;

    public PlayCommandHandler(IWalletService walletService, IRoundService roundService, IUserOutputService userOutputService)
    {
        _walletService = walletService;
        _roundService = roundService;
        _userOutputService = userOutputService;
    }

    public void Handle(PlayCommand command)
    {
        decimal betAmount = command.BetAmount;

        var withdrawResult = _walletService.HouseWithdraw(betAmount);
        if (!withdrawResult.Success)
        {
            _userOutputService.PrintErrorMessage(string.Join(Environment.NewLine, withdrawResult.ErrorMessage));
            return;
        }

        var result = _roundService.PlayRound(betAmount);
        if (result > 0)
        {
            _userOutputService.PrintWonBet(result);
            var depositResult = _walletService.Deposit(result);

            if (depositResult.Success)
            {
                _userOutputService.PrintCurrentBalance(depositResult.NewBalance);
            }
            else
            {
                _userOutputService.PrintErrorMessage(string.Join(Environment.NewLine, depositResult.ErrorMessage));
            }
        }
        else
        {
            _userOutputService.PrintLostBet();
        }
    }
}

