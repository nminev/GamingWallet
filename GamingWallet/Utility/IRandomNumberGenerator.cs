namespace GamingWallet.Utility;

public interface IRandomNumberGenerator
{
    double NextDouble();
    int Next(int minValue, int maxValue);
}
