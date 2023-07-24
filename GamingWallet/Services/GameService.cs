using GamingWallet.Services.ServiceInterfaces;

namespace GamingWallet.Services;
public class GameService : IGameService
{
    private const decimal MinBetAmount = 1;
    private const decimal MaxBetAmount = 10;

    private readonly IWalletService _walletService;
    private readonly IRoundService _roundService;
    private readonly IUserInputService _userInputService;
    private readonly IUserOutputService _userOutputService;

    public GameService(IWalletService wallet, IRoundService roundService, IUserInputService userInputService, IUserOutputService userOutputService)
    {
        _walletService = wallet;
        _roundService = roundService;
        _userInputService = userInputService;
        _userOutputService = userOutputService;
    }

    public void RunGame()
    {
        while (true)
        {
            string action = _userInputService.GetStringInput("Choose an action: [d]eposit, [w]ithdraw, [p]lay, [q]uit");

            switch (action)
            {
                case "d":
                    HandleDeposit();
                    break;
                case "w":
                    HandleWithdraw();
                    break;
                case "p":
                    HandlePlay();
                    break;
                case "q":
                    return;
                default:
                    _userOutputService.PrintMessage("Invalid Action");
                    break;
            }
        }
    }

    private void HandleDeposit()
    {
        decimal depositAmount = _userInputService.GetDecimalInput("Enter amount to deposit: ");
        var result = _walletService.Deposit(depositAmount);
        if (result.Success)
        {
            _userOutputService.PrintDeposit(depositAmount, result.NewBalance);
        }
        else
        {
            foreach (var errorMessage in result.ErrorMessage)
            {
                _userOutputService.PrintMessage(errorMessage);
            }
        }
    }

    private void HandleWithdraw()
    {
        decimal withdrawAmount = _userInputService.GetDecimalInput("Enter amount to withdraw: ");
        var result = _walletService.Withdraw(withdrawAmount);
        if (result.Success)
        {
            _userOutputService.PrintWithdraw(withdrawAmount, result.NewBalance);
        }
        else
        {
            _userOutputService.PrintMessage(string.Join(Environment.NewLine, result.ErrorMessage));
        }
    }

    private void HandlePlay()
    {
        decimal betAmount = GetValidBetAmount();
        var withdrawResult = _walletService.HouseWithdraw(betAmount);
        if (!withdrawResult.Success)
        {
            _userOutputService.PrintMessage(string.Join(Environment.NewLine, withdrawResult.ErrorMessage));
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
                _userOutputService.PrintMessage(string.Join(Environment.NewLine, depositResult.ErrorMessage));
            }
        }
        else
        {
            _userOutputService.PrintLostBet();
        }


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

            _userOutputService.PrintMessage("Invalid bet ammount");
        }
    }
}
