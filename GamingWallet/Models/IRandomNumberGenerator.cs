namespace GamingWallet.Models;

public interface IRandomNumberGenerator
{
    double NextDouble();
    int Next(int minValue, int maxValue);
}
