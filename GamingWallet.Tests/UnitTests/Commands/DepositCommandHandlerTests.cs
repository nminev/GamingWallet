using GamingWallet.Commands;
using GamingWallet.Models;
using GamingWallet.Services.ServiceInterfaces;
using Moq;

namespace GamingWallet.Tests.UnitTests.Commands;
public class DepositCommandHandlerTests
{
    private readonly Mock<IWalletService> _walletServiceMock;
    private readonly Mock<IUserOutputService> _userOutputServiceMock;
    private readonly DepositCommandHandler _depositCommandHandler;

    public DepositCommandHandlerTests()
    {
        _walletServiceMock = new Mock<IWalletService>();
        _userOutputServiceMock = new Mock<IUserOutputService>();
        _depositCommandHandler = new DepositCommandHandler(_walletServiceMock.Object, _userOutputServiceMock.Object);
    }

    [Fact]
    public void Handle_ShouldCallWalletServiceDeposit_WhenDepositCommandIsHandled()
    {
        // Arrange
        decimal depositAmount = 10;
        var command = new DepositCommand(depositAmount);

        _walletServiceMock.Setup(w => w.Deposit(depositAmount)).Returns(new TransactionResult { Success = true, NewBalance = depositAmount });

        // Act
        _depositCommandHandler.Handle(command);

        // Assert
        _walletServiceMock.Verify(w => w.Deposit(depositAmount), Times.Once);
    }

    [Fact]
    public void Handle_ShouldCallUserOutputServicePrintDeposit_WhenDepositIsSuccessful()
    {
        // Arrange
        decimal depositAmount = 10;
        var command = new DepositCommand(depositAmount);

        _walletServiceMock.Setup(w => w.Deposit(depositAmount)).Returns(new TransactionResult { Success = true, NewBalance = depositAmount });

        // Act
        _depositCommandHandler.Handle(command);

        // Assert
        _userOutputServiceMock.Verify(u => u.PrintDepositSuccessful(depositAmount, depositAmount), Times.Once);
    }

    [Fact]
    public void Handle_ShouldCallUserOutputServicePrintMessage_WhenDepositFails()
    {
        // Arrange
        decimal depositAmount = 10;
        var command = new DepositCommand(depositAmount);
        var errorMessage = "Deposit failed for some reason.";

        _walletServiceMock.Setup(w => w.Deposit(depositAmount)).Returns(new TransactionResult { Success = false, ErrorMessage = new List<string> { errorMessage } });

        // Act
        _depositCommandHandler.Handle(command);

        // Assert
        _userOutputServiceMock.Verify(u => u.PrintErrorMessage(errorMessage), Times.Once);
    }

    [Fact]
    public void Handle_ShouldHandleZeroDepositAmount()
    {
        // Arrange
        decimal depositAmount = 0;
        var command = new DepositCommand(depositAmount);

        _walletServiceMock.Setup(w => w.Deposit(depositAmount)).Returns(new TransactionResult { Success = true, NewBalance = depositAmount });

        // Act
        _depositCommandHandler.Handle(command);

        // Assert
        _walletServiceMock.Verify(w => w.Deposit(depositAmount), Times.Once);
    }

    [Fact]
    public void Handle_ShouldNotCallWalletServiceDeposit_WhenDepositAmountIsNegative()
    {
        // Arrange
        decimal depositAmount = -10;
        var command = new DepositCommand(depositAmount);

        // Act
        _depositCommandHandler.Handle(command);

        // Assert
        _walletServiceMock.Verify(w => w.Deposit(depositAmount), Times.Never);
    }

    [Fact]
    public void Handle_ShouldPrintMultipleErrorMessages_WhenMultipleErrorsAreReturned()
    {
        // Arrange
        decimal depositAmount = 1;
        var command = new DepositCommand(depositAmount);
        var errorMessages = new List<string> { "Error 1.", "Error 2." };

        _walletServiceMock.Setup(w => w.Deposit(depositAmount)).Returns(new TransactionResult { Success = false, ErrorMessage = errorMessages });

        // Act
        _depositCommandHandler.Handle(command);

        // Assert
        foreach (var errorMessage in errorMessages)
        {
            _userOutputServiceMock.Verify(u => u.PrintErrorMessage(errorMessage), Times.Once);
        }
    }

    [Fact]
    public void Handle_ShouldNotPrintDepositMessage_WhenDepositFails()
    {
        // Arrange
        decimal depositAmount = 5000;
        var command = new DepositCommand(depositAmount);
        var errorMessage = "Deposit failed for some reason.";

        _walletServiceMock.Setup(w => w.Deposit(depositAmount)).Returns(new TransactionResult { Success = false, ErrorMessage = new List<string> { errorMessage } });

        // Act
        _depositCommandHandler.Handle(command);

        // Assert
        _userOutputServiceMock.Verify(u => u.PrintDepositSuccessful(depositAmount, It.IsAny<decimal>()), Times.Never);
    }

    [Fact]
    public void Handle_ShouldPrintCorrectNewBalance_WhenDepositIsSuccessful()
    {
        // Arrange
        decimal depositAmount = 10;
        decimal newBalance = 100;
        var command = new DepositCommand(depositAmount);

        _walletServiceMock.Setup(w => w.Deposit(depositAmount)).Returns(new TransactionResult { Success = true, NewBalance = newBalance });

        // Act
        _depositCommandHandler.Handle(command);

        // Assert
        _userOutputServiceMock.Verify(u => u.PrintDepositSuccessful(depositAmount, newBalance), Times.Once);
    }

}
