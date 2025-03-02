using BowlingAlley.Interfaces;

namespace BowlingAlley.Entities
{
    // En context klass som håller en referens till en IScore strategi klass.
    // I det här programmet finns det dock bara en strategi klass så det kanske
    // egentligen inte är nödvändigt att använda en context klass.
    // Med det sagt så finns det iallafall möjlighet att lägga till fler strategi
    // klasser i framtiden och kombinera strategy pattern med ett factory pattern 
    // som då skulle kunna skapa instanser av strategi klasser.
    public class ScoreContext : IScore
    {
        private IScore? _playerScore;

        public void SetScoreStrategy(IScore playerScore)
        {
            _playerScore = playerScore;   
        }

        public int CalculateScore()
        {
            return _playerScore?.CalculateScore() ?? 0;
        }
    }
}
