using BowlingAlley.Interfaces;

namespace BowlingAlley.Entities
{
    // En strategi klass som implementerar IScore interfacet och
    // returnerar ett slumpmässigt värde mellan 0 och 15.
    class RandomScoreStrategy : IScore
    {
        private Random _random = new();

        public int CalculateScore()
        {
            return _random.Next(0, 16);
        }
    }
}
