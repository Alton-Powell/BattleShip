
namespace Common.Infrastructure
{
    public interface IRandomNumberGenerator
    {
        int Next(int minValue, int maxValue);
        int Next(int maxValue);
    }
}
