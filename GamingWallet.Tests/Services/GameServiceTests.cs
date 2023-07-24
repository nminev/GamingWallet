using GamingWallet.Commands;
using GamingWallet.Services;
using GamingWallet.Services.ServiceInterfaces;
using Moq;
namespace GamingWallet.Tests.Services;

public class GameServiceTests
{
    private readonly Mock<IUserInputService> _userInputServiceMock;
    private readonly Mock<IUserOutputService> _userOutputServiceMock;
    private readonly Mock<ICommandHandlerResolver> _commandHandlerResolverMock;
    private readonly GameService _gameService;

    public GameServiceTests()
    {
        _userInputServiceMock = new Mock<IUserInputService>();
        _userOutputServiceMock = new Mock<IUserOutputService>();
        _commandHandlerResolverMock = new Mock<ICommandHandlerResolver>();
        _gameService = new GameService(_userInputServiceMock.Object, _userOutputServiceMock.Object, _commandHandlerResolverMock.Object);
    }

    [Fact]
    public void RunGame_ShouldCallDepositHandler_WhenDepositActionIsInput()
    {
        // Arrange
        decimal depositAmount = 10;
        var handlerMock = new Mock<ICommandHandler<DepositCommand>>();
        _commandHandlerResolverMock.Setup(r => r.Resolve<DepositCommand>()).Returns(handlerMock.Object);

        // Mock the user input sequence: deposit action, deposit amount, then quit action
        var userInputSequence = new MockSequence();
        _userInputServiceMock.InSequence(userInputSequence)
            .Setup(s => s.GetStringInput(It.IsAny<string>()))
            .Returns(("d", depositAmount));
        _userInputServiceMock.InSequence(userInputSequence)
            .Setup(s => s.GetStringInput(It.IsAny<string>()))
            .Returns(("q", null));

        // Act
        _gameService.RunGame();

        // Assert
        handlerMock.Verify(h => h.Handle(It.Is<DepositCommand>(c => c.Amount == depositAmount)), Times.Once);
    }

    [Fact]
    public void RunGame_ShouldPromptForAmountAndCallHandler_WhenAmountIsNotProvided()
    {
        // Arrange
        decimal depositAmount = 10;

        var userInputSequence = new MockSequence();
        _userInputServiceMock.InSequence(userInputSequence)
            .Setup(s => s.GetStringInput(It.IsAny<string>()))
            .Returns(("d", null));
        _userInputServiceMock.InSequence(userInputSequence)
            .Setup(s => s.GetStringInput(It.IsAny<string>()))
            .Returns(("q", null));

        _userInputServiceMock.Setup(s => s.GetDecimalInput(It.IsAny<string>())).Returns(depositAmount);
        var handlerMock = new Mock<ICommandHandler<DepositCommand>>();
        _commandHandlerResolverMock.Setup(r => r.Resolve<DepositCommand>()).Returns(handlerMock.Object);

        // Act
        _gameService.RunGame();

        // Assert
        _userInputServiceMock.Verify(s => s.GetDecimalInput(It.IsAny<string>()), Times.Once);
        handlerMock.Verify(h => h.Handle(It.Is<DepositCommand>(c => c.Amount == depositAmount)), Times.Once);
    }

    [Fact]
    public void RunGame_ShouldCallWithdrawHandler_WhenWithdrawActionIsInput()
    {
        // Arrange
        decimal withdrawAmount = 10;
        var handlerMock = new Mock<ICommandHandler<WithdrawCommand>>();
        _commandHandlerResolverMock.Setup(r => r.Resolve<WithdrawCommand>()).Returns(handlerMock.Object);

        // Mock the user input sequence: withdraw action, withdraw amount, then quit action
        var userInputSequence = new MockSequence();
        _userInputServiceMock.InSequence(userInputSequence)
            .Setup(s => s.GetStringInput(It.IsAny<string>()))
            .Returns(("w", withdrawAmount));
        _userInputServiceMock.InSequence(userInputSequence)
            .Setup(s => s.GetStringInput(It.IsAny<string>()))
            .Returns(("q", null));

        // Act
        _gameService.RunGame();

        // Assert
        handlerMock.Verify(h => h.Handle(It.Is<WithdrawCommand>(c => c.Amount == withdrawAmount)), Times.Once);
    }

    [Fact]
    public void RunGame_ShouldCallPlayHandler_WhenPlayActionIsInput()
    {
        // Arrange
        decimal betAmount = 5;
        var handlerMock = new Mock<ICommandHandler<PlayCommand>>();
        _commandHandlerResolverMock.Setup(r => r.Resolve<PlayCommand>()).Returns(handlerMock.Object);

        // Mock the user input sequence: play action, bet amount, then quit action
        var userInputSequence = new MockSequence();
        _userInputServiceMock.InSequence(userInputSequence)
            .Setup(s => s.GetStringInput(It.IsAny<string>()))
            .Returns(("p", betAmount));
        _userInputServiceMock.InSequence(userInputSequence)
            .Setup(s => s.GetStringInput(It.IsAny<string>()))
            .Returns(("q", null));

        // Act
        _gameService.RunGame();

        // Assert
        handlerMock.Verify(h => h.Handle(It.Is<PlayCommand>(c => c.BetAmount == betAmount)), Times.Once);
    }


}