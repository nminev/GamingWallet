using GamingWallet.Services.ServiceInterfaces;

namespace GamingWallet.Commands;

/// <summary>
/// Command to trigger play action.
/// </summary>
public class PlayCommand : ICommand
{
    /// <summary>
    /// Amount to bet.
    /// </summary>
    public decimal BetAmount { get; }

    public PlayCommand(decimal betAmount)
    {
        BetAmount = betAmount;
    }
}

/// <summary>
/// Handles play commands by playing a round of the game with the specified bet amount.
/// </summary>
public class PlayCommandHandler : ICommandHandler<PlayCommand>
{
    private readonly IWalletService _walletService;
    private readonly IRoundService _roundService;
    private readonly IUserOutputService _userOutputService;
    private const decimal MinBetAmount = 1;
    private const decimal MaxBetAmount = 10;

    public PlayCommandHandler(IWalletService walletService, IRoundService roundService,
        IUserOutputService userOutputService)
    {
        _walletService = walletService;
        _roundService = roundService;
        _userOutputService = userOutputService;
    }

    /// <summary>
    /// Handles the specified play command by playing a round of the game with the specified bet amount.
    /// If the play is successful, a success message is printed to the user.
    /// If the play fails, the error messages are printed to the user.
    /// </summary>
    /// <param name="command">The play command to handle. This must be of type <see cref="PlayCommand"/>.</param>
    public void Handle(PlayCommand command)
    {
        decimal betAmount = command.BetAmount;

        if (betAmount is < MinBetAmount or > MaxBetAmount)
        {
            _userOutputService.PrintErrorMessage("Invalid bet amount. Must be between 1 and 10");
            return;
        }

        var withdrawResult = _walletService.WithdrawForTheHouse(betAmount);
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