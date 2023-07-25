namespace GamingWallet.Utility;

/// <inheritdoc/>
public class RandomNumberGenerator : IRandomNumberGenerator
{
    private readonly Random _random;

    public RandomNumberGenerator()
    {
        _random = new Random();
    }

    /// <inheritdoc/>
    public double NextDouble()
    {
        return _random.NextDouble();
    }
    /// <inheritdoc/>
    public int Next(int minValue, int maxValue)
    {
        return _random.Next(minValue, maxValue);
    }
}