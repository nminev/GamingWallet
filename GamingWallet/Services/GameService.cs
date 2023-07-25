using GamingWallet.Commands;
using GamingWallet.Services.ServiceInterfaces;
using GamingWallet.Utility;

namespace GamingWallet.Services;

/// <inheritdoc/>
public class GameService : IGameService
{
    private readonly ICommandHandlerResolver _commandHandlerResolver;
    private readonly IUserInputService _userInputService;
    private readonly IUserOutputService _userOutputService;

    public GameService(IUserInputService userInputService, IUserOutputService userOutputService, ICommandHandlerResolver commandHandlerResolver)
    {
        _userInputService = userInputService;
        _userOutputService = userOutputService;
        _commandHandlerResolver = commandHandlerResolver;
    }

    /// <inheritdoc/>
    public void RunGame()
    {
        while (true)
        {
            try
            {
                (string? action, decimal? amount) = _userInputService.GetStringInput("Choose an action: [d]eposit, [w]ithdraw, [p]lay, [q]uit");

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
            catch (Exception ex)
            {
                _userOutputService.PrintErrorMessage($"An error occurred: {ex.Message}");
                break;
            }
        }
    }


    private void HandleDepositCommand(decimal? amount)
    {
        decimal depositAmount = amount ?? _userInputService.GetDecimalInput("Enter amount to deposit: ");
        var command = new DepositCommand(depositAmount);
        var handler = _commandHandlerResolver.Resolve<DepositCommand>();
        handler.Handle(command);
    }

    private void HandleWithdrawCommand(decimal? amount)
    {
        decimal withdrawAmount = amount ?? _userInputService.GetDecimalInput("Enter amount to withdraw: ");
        var command = new WithdrawCommand(withdrawAmount);
        var handler = _commandHandlerResolver.Resolve<WithdrawCommand>();
        handler.Handle(command);
    }

    private void HandlePlayCommand(decimal? amount)
    {
        decimal betAmount = amount ?? GetValidBetAmount();
        var command = new PlayCommand(betAmount);
        var handler = _commandHandlerResolver.Resolve<PlayCommand>();
        handler.Handle(command);
    }

    private decimal GetValidBetAmount()
    {
        while (true)
        {
            decimal betAmount = _userInputService.GetDecimalInput("Enter bet amount: ");
            return betAmount;
        }
    }
}
