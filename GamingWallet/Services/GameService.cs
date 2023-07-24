using GamingWallet.Commands;
using GamingWallet.Services.ServiceInterfaces;

namespace GamingWallet.Services;
public class GameService : IGameService
{
    private const decimal MinBetAmount = 1;
    private const decimal MaxBetAmount = 10;

    private readonly ICommandHandlerResolver _commandHandlerResolver;
    private readonly IUserInputService _userInputService;
    private readonly IUserOutputService _userOutputService;

    public GameService(IUserInputService userInputService, IUserOutputService userOutputService, ICommandHandlerResolver commandHandlerResolver)
    {
        _userInputService = userInputService;
        _userOutputService = userOutputService;
        _commandHandlerResolver = commandHandlerResolver;
    }

    public void RunGame()
    {
        while (true)
        {
            (string action, decimal? amount) = _userInputService.GetStringInput("Choose an action: [d]eposit, [w]ithdraw, [p]lay, [q]uit");

            switch (action)
            {
                case "d":
                case "deposit":
                    HandleDepositCommand(amount);
                    break;
                case "w":
                case "withdraw":
                    HandleWithdrawCommand(amount);
                    break;
                case "p":
                case "play":
                    HandlePlayCommand(amount);
                    break;
                case "q":
                case "quit":
                    return;
                default:
                    _userOutputService.PrintErrorMessage("Invalid Action");
                    continue;
            }
        }
    }

    private void HandleDepositCommand(decimal? amount)
    {
        decimal depositAmount = amount != default ? amount!.Value : _userInputService.GetDecimalInput("Enter amount to deposit: ");
        var command = new DepositCommand(depositAmount);
        var handler = _commandHandlerResolver.Resolve<DepositCommand>();
        handler.Handle(command);
    }

    private void HandleWithdrawCommand(decimal? amount)
    {
        decimal withdrawAmount = amount != default ? amount!.Value : _userInputService.GetDecimalInput("Enter amount to withdraw: ");
        var command = new WithdrawCommand(withdrawAmount);
        var handler = _commandHandlerResolver.Resolve<WithdrawCommand>();
        handler.Handle(command);
    }

    private void HandlePlayCommand(decimal? amount)
    {
        decimal betAmount = amount != default ? amount!.Value: GetValidBetAmount();
        var command = new PlayCommand(betAmount);
        var handler = _commandHandlerResolver.Resolve<PlayCommand>();
        handler.Handle(command);
    }

    private decimal GetValidBetAmount()
    {
        while (true)
        {
            decimal betAmount = _userInputService.GetDecimalInput("Enter bet amount: ");
            if (betAmount >= MinBetAmount && betAmount <= MaxBetAmount)
            {
                return betAmount;
            }

            _userOutputService.PrintErrorMessage("Invalid bet ammount");
        }
    }
}
