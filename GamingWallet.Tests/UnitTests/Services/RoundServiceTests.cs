using GamingWallet.Services;
using GamingWallet.Utility;
using Moq;

namespace GamingWallet.Tests.UnitTests.Services;
public class RoundServiceTests
{
    [Fact]
    public void TestLose()
    {
        // Arrange
        var rng = new Mock<IRandomNumberGenerator>();
        rng.Setup(r => r.NextDouble()).Returns(0.1);  // Simulate losing
        var round = new RoundService(rng.Object);

        // Act
        var result = round.PlayRound(5);

        // Assert
        Assert.Equal(-5, result);
    }

    [Fact]
    public void TestWinNormal()
    {
        // Arrange
        var rng = new Mock<IRandomNumberGenerator>();
        rng.Setup(r => r.NextDouble()).Returns(0.6);  // Simulate normal win
        var round = new RoundService(rng.Object);

        // Act
        var result = round.PlayRound(5);

        // Assert
        Assert.Equal(10, result);
    }

    [Fact]
    public void TestWinBig()
    {
        // Arrange
        var rng = new Mock<IRandomNumberGenerator>();
        rng.SetupSequence(r => r.NextDouble()).Returns(0.95).Returns(0.5);  // Simulate big win
        rng.Setup(r => r.Next(2, 11)).Returns(5);  // Simulate winning 5 times the bet
        var round = new RoundService(rng.Object);

        // Act
        var result = round.PlayRound(5);

        // Assert
        Assert.Equal(30, result);
    }
}
