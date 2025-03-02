namespace BowlingAlley.Entities
{
    // Simpel klass som representerar en spelare.
    public class Player
    {        
        public int PlayerID { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }

        public Player(string name)
        {
            Name = name;
            Score = 0;
        }
    }
}
