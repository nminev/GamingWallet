using GamingWallet.Commands;
using GamingWallet.Models;
using GamingWallet.Services.ServiceInterfaces;
using Moq;

namespace GamingWallet.Tests.Commands;

public class WithdrawCommandHandlerTests
{
    private readonly Mock<IWalletService> _walletServiceMock;
    private readonly Mock<IUserOutputService> _userOutputServiceMock;
    private readonly WithdrawCommandHandler _withdrawCommandHandler;

    public WithdrawCommandHandlerTests()
    {
        _walletServiceMock = new Mock<IWalletService>();
        _userOutputServiceMock = new Mock<IUserOutputService>();
        _withdrawCommandHandler = new WithdrawCommandHandler(_walletServiceMock.Object, _userOutputServiceMock.Object);
    }
    [Fact]
    public void Handle_ShouldCallWalletServiceWithdraw_WhenWithdrawCommandIsProcessed()
    {
        // Arrange
        decimal withdrawAmount = 10;
        var command = new WithdrawCommand(withdrawAmount);

        _walletServiceMock.Setup(w => w.Withdraw(withdrawAmount)).Returns(new TransactionResult { Success = true, NewBalance = 0 });

        // Act
        _withdrawCommandHandler.Handle(command);

        // Assert
        _walletServiceMock.Verify(w => w.Withdraw(withdrawAmount), Times.Once);
    }

    [Fact]
    public void Handle_ShouldPrintSuccessfulWithdrawMessage_WhenWithdrawSucceeds()
    {
        // Arrange
        decimal withdrawAmount = 10;
        var command = new WithdrawCommand(withdrawAmount);

        _walletServiceMock.Setup(w => w.Withdraw(withdrawAmount)).Returns(new TransactionResult { Success = true, NewBalance = 0 });

        // Act
        _withdrawCommandHandler.Handle(command);

        // Assert
        _userOutputServiceMock.Verify(u => u.PrintWithdrawSuccessfull(withdrawAmount, It.IsAny<decimal>()), Times.Once);
    }

    [Fact]
    public void Handle_ShouldPrintErrorMessage_WhenWithdrawFails()
    {
        // Arrange
        decimal withdrawAmount = 10;
        var command = new WithdrawCommand(withdrawAmount);
        var errorMessage = new List<string> { "Insufficient funds" };

        _walletServiceMock.Setup(w => w.Withdraw(withdrawAmount)).Returns(new TransactionResult { Success = false, ErrorMessage = errorMessage });

        // Act
        _withdrawCommandHandler.Handle(command);

        // Assert
        _userOutputServiceMock.Verify(u => u.PrintErrorMessage(string.Join(Environment.NewLine, errorMessage)), Times.Once);
    }

    [Fact]
    public void Handle_ShouldHandleZeroWithdrawalAmount()
    {
        // Arrange
        decimal withdrawAmount = 0;
        var command = new WithdrawCommand(withdrawAmount);

        _walletServiceMock.Setup(w => w.Withdraw(withdrawAmount)).Returns(new TransactionResult { Success = true, NewBalance = 100 });

        // Act
        _withdrawCommandHandler.Handle(command);

        // Assert
        _walletServiceMock.Verify(w => w.Withdraw(withdrawAmount), Times.Once);
    }

    [Fact]
    public void Handle_ShouldHandleExcessiveWithdrawalAmount()
    {
        // Arrange
        decimal withdrawAmount = 200; // Assuming initial balance is less than 200
        var command = new WithdrawCommand(withdrawAmount);
        var errorMessage = new List<string> { "Insufficient funds" };

        _walletServiceMock.Setup(w => w.Withdraw(withdrawAmount)).Returns(new TransactionResult { Success = false, ErrorMessage = errorMessage });

        // Act
        _withdrawCommandHandler.Handle(command);

        // Assert
        _userOutputServiceMock.Verify(u => u.PrintErrorMessage(string.Join(Environment.NewLine, errorMessage)), Times.Once);
    }

    [Fact]
    public void Handle_ShouldProcessWithdrawal_AfterDeposit()
    {
        // Arrange
        decimal depositAmount = 100;
        decimal withdrawAmount = 50;
        var command = new WithdrawCommand(withdrawAmount);

        // Assume that deposit operation was successful and balance is now 100
        _walletServiceMock.Setup(w => w.Withdraw(withdrawAmount)).Returns(new TransactionResult { Success = true, NewBalance = depositAmount - withdrawAmount });

        // Act
        _withdrawCommandHandler.Handle(command);

        // Assert
        _userOutputServiceMock.Verify(u => u.PrintWithdrawSuccessfull(withdrawAmount, depositAmount - withdrawAmount), Times.Once);
    }

    [Fact]
    public void Handle_ShouldProcessMultipleWithdrawals()
    {
        // Arrange
        decimal initialBalance = 100;
        decimal firstWithdrawAmount = 30;
        decimal secondWithdrawAmount = 50;

        _walletServiceMock.SetupSequence(w => w.Withdraw(It.IsAny<decimal>()))
            .Returns(new TransactionResult { Success = true, NewBalance = initialBalance - firstWithdrawAmount })
            .Returns(new TransactionResult { Success = true, NewBalance = initialBalance - firstWithdrawAmount - secondWithdrawAmount });

        // Act
        _withdrawCommandHandler.Handle(new WithdrawCommand(firstWithdrawAmount));
        _withdrawCommandHandler.Handle(new WithdrawCommand(secondWithdrawAmount));

        // Assert
        _walletServiceMock.Verify(w => w.Withdraw(firstWithdrawAmount), Times.Once);
        _walletServiceMock.Verify(w => w.Withdraw(secondWithdrawAmount), Times.Once);
    }

}
