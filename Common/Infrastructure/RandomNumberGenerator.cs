using System;


namespace Common.Infrastructure
{
    public class RandomNumberGenerator : IRandomNumberGenerator
    {
        public int Next(int minValue, int maxValue)
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            return random.Next(minValue, maxValue);
        }

        public int Next(int maxValue)
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            return random.Next(maxValue);
        }
    }
}
