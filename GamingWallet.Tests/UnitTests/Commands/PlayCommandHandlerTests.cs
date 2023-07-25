using GamingWallet.Commands;
using GamingWallet.Models;
using GamingWallet.Services.ServiceInterfaces;
using Moq;

namespace GamingWallet.Tests.UnitTests.Commands;
public class PlayCommandHandlerTests
{
    private readonly Mock<IWalletService> _walletServiceMock;
    private readonly Mock<IRoundService> _roundServiceMock;
    private readonly Mock<IUserOutputService> _userOutputServiceMock;
    private readonly PlayCommandHandler _playCommandHandler;

    public PlayCommandHandlerTests()
    {
        _walletServiceMock = new Mock<IWalletService>();
        _roundServiceMock = new Mock<IRoundService>();
        _userOutputServiceMock = new Mock<IUserOutputService>();
        _playCommandHandler = new PlayCommandHandler(_walletServiceMock.Object, _roundServiceMock.Object, _userOutputServiceMock.Object);
    }

    [Fact]
    public void Handle_ShouldWithdrawBetAndDepositWinnings_WhenPlayerWins()
    {
        // Arrange
        decimal betAmount = 5;
        decimal winAmount = 10;
        var command = new PlayCommand(betAmount);

        _walletServiceMock.Setup(w => w.WithdrawForTheHouse(betAmount)).Returns(new TransactionResult { Success = true });
        _roundServiceMock.Setup(r => r.PlayRound(betAmount)).Returns(winAmount);
        _walletServiceMock.Setup(w => w.Deposit(winAmount)).Returns(new TransactionResult { Success = true, NewBalance = winAmount });

        // Act
        _playCommandHandler.Handle(command);

        // Assert
        _walletServiceMock.Verify(w => w.WithdrawForTheHouse(betAmount), Times.Once);
        _walletServiceMock.Verify(w => w.Deposit(winAmount), Times.Once);
        _userOutputServiceMock.Verify(u => u.PrintWonBet(winAmount), Times.Once);
        _userOutputServiceMock.Verify(u => u.PrintCurrentBalance(winAmount), Times.Once);
    }

    [Fact]
    public void Handle_ShouldWithdrawBetAndNotDeposit_WhenPlayerLoses()
    {
        // Arrange
        decimal betAmount = 5;
        var command = new PlayCommand(betAmount);

        _walletServiceMock.Setup(w => w.WithdrawForTheHouse(betAmount)).Returns(new TransactionResult { Success = true });
        _roundServiceMock.Setup(r => r.PlayRound(betAmount)).Returns(0m);

        // Act
        _playCommandHandler.Handle(command);

        // Assert
        _walletServiceMock.Verify(w => w.WithdrawForTheHouse(betAmount), Times.Once);
        _walletServiceMock.Verify(w => w.Deposit(It.IsAny<decimal>()), Times.Never);
        _userOutputServiceMock.Verify(u => u.PrintLostBet(), Times.Once);
    }

    [Fact]
    public void Handle_ShouldPrintErrorAndAbort_WhenWithdrawFails()
    {
        // Arrange
        decimal betAmount = 5;
        var command = new PlayCommand(betAmount);
        var errorMessage = new List<string> { "Insufficient funds" };

        _walletServiceMock.Setup(w => w.WithdrawForTheHouse(betAmount)).Returns(new TransactionResult { Success = false, ErrorMessage = errorMessage });

        // Act
        _playCommandHandler.Handle(command);

        // Assert
        _walletServiceMock.Verify(w => w.WithdrawForTheHouse(betAmount), Times.Once);
        _userOutputServiceMock.Verify(u => u.PrintErrorMessage(string.Join(Environment.NewLine, errorMessage)), Times.Once);
        _roundServiceMock.Verify(r => r.PlayRound(It.IsAny<decimal>()), Times.Never); // Round should not be played
    }

    [Fact]
    public void Handle_ShouldPrintError_WhenDepositFails()
    {
        // Arrange
        decimal betAmount = 5;
        decimal winAmount = 10;
        var command = new PlayCommand(betAmount);
        var errorMessage = new List<string> { "Deposit failed" };

        _walletServiceMock.Setup(w => w.WithdrawForTheHouse(betAmount)).Returns(new TransactionResult { Success = true });
        _roundServiceMock.Setup(r => r.PlayRound(betAmount)).Returns(winAmount);
        _walletServiceMock.Setup(w => w.Deposit(winAmount)).Returns(new TransactionResult { Success = false, ErrorMessage = errorMessage });

        // Act
        _playCommandHandler.Handle(command);

        // Assert
        _walletServiceMock.Verify(w => w.Deposit(winAmount), Times.Once);
        _userOutputServiceMock.Verify(u => u.PrintErrorMessage(string.Join(Environment.NewLine, errorMessage)), Times.Once);
    }

    [Fact]
    public void Handle_ShouldPrintErrorMessage_WhenBetAmountIsLessThanMinBetAmount()
    {
        // Arrange
        decimal betAmount = 0.5m;  // Less than min bet amount
        var command = new PlayCommand(betAmount);

        // Act
        _playCommandHandler.Handle(command);

        // Assert
        _userOutputServiceMock.Verify(u => u.PrintErrorMessage(It.Is<string>(s => s.Contains("Invalid bet amount. Must be between 1 and 10"))), Times.Once);
    }

    [Fact]
    public void Handle_ShouldPrintErrorMessage_WhenBetAmountIsGreaterThanMaxBetAmount()
    {
        // Arrange
        decimal betAmount = 10.5m;  // Greater than max bet amount
        var command = new PlayCommand(betAmount);

        // Act
        _playCommandHandler.Handle(command);

        // Assert
        _userOutputServiceMock.Verify(u => u.PrintErrorMessage(It.Is<string>(s => s.Contains("Invalid bet amount. Must be between 1 and 10"))), Times.Once);
    }

    [Fact]
    public void Handle_ShouldPrintErrorMessage_WhenBetAmountIsLessThanZero()
    {
        // Arrange
        decimal betAmount = -10;
        var command = new PlayCommand(betAmount);

        // Act
        _playCommandHandler.Handle(command);

        // Assert
        _userOutputServiceMock.Verify(u => u.PrintErrorMessage(It.Is<string>(s => s.Contains("Invalid bet amount."))), Times.Once);
    }

}
