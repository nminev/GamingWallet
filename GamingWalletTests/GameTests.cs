using GamingWallet.Models;
using Moq;

namespace GamingWalletTests;
public class GameTests
{
    [Fact]
    public void TestLose()
    {
        var rng = new Mock<IRandomNumberGenerator>();
        rng.Setup(r => r.NextDouble()).Returns(0.1);  // Simulate losing
        var game = new Game(rng.Object);
        var result = game.PlayRound(5);
        Assert.Equal(-5, result);
    }

    [Fact]
    public void TestWinNormal()
    {
        var rng = new Mock<IRandomNumberGenerator>();
        rng.Setup(r => r.NextDouble()).Returns(0.6);  // Simulate normal win
        var game = new Game(rng.Object);
        var result = game.PlayRound(5);
        Assert.Equal(5, result);
    }

    [Fact]
    public void TestWinBig()
    {
        var rng = new Mock<IRandomNumberGenerator>();
        rng.SetupSequence(r => r.NextDouble()).Returns(0.95).Returns(0.5);  // Simulate big win
        rng.Setup(r => r.Next(2, 11)).Returns(5);  // Simulate winning 5 times the bet
        var game = new Game(rng.Object);
        var result = game.PlayRound(5);
        Assert.Equal(25, result);
    }
}
