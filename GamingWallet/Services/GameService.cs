using GamingWallet.Services.ServiceInterfaces;

namespace GamingWallet.Services;
public class GameService : IGameService
{
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
                    decimal depositAmount = _userInputService.GetDecimalInput("Enter amount to deposit: ");
                    _walletService.Deposit(depositAmount);
                    _userOutputService.PrintDeposit(depositAmount, _walletService.Balance());
                    break;
                case "w":
                    decimal withdrawAmount = _userInputService.GetDecimalInput("Enter amount to withdraw: ");
                    _walletService.Withdraw(withdrawAmount);
                    _userOutputService.PrintWithdraw(withdrawAmount, _walletService.Balance());
                    break;
                case "p":
                    decimal betAmount = _userInputService.GetDecimalInput("Enter bet amount: ");
                    if (betAmount < 1 || betAmount > 10)
                    {
                        _userOutputService.PrintInvalidBetAmount();
                        break;
                    }
                    if (_walletService.Balance() < betAmount)
                    {
                        _userOutputService.PrintInsufficientFunds();
                        break;
                    }

                    var result = _roundService.PlayRound(betAmount);
                    if (result > 0)
                    {
                        _userOutputService.PrintWonBet(result);
                    }
                    else
                    {
                        _userOutputService.PrintLostBet();
                    }

                    _walletService.Deposit(result);
                    _userOutputService.PrintCurrentBalance(_walletService.Balance());
                    break;
                case "q":
                    return;
                default:
                    _userOutputService.PrintInvalidAction();
                    break;
            }
        }
    }
}
