using BowlingAlley.Entities;

namespace BowlingAlley.Repository
{
    // En repository klass som hanterar CRUD operationer för Player
    // med entity framework.
    // Innerhåller en referens(_context) till en BowlingAlleyDataContext klass som
    // i sin tur används för att ge åtkomst till databasen.

    public class PlayerRepo
    {
        private readonly BowlingAlleyDataContext _dbContext;
        public PlayerRepo(BowlingAlleyDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddPlayer(Player playerName)
        {
            _dbContext.Players.Add(playerName);
            _dbContext.SaveChanges();
        }

        public List<Player> GetPlayers()
        {
            return _dbContext.Players.ToList();
        }

        public void UpdatePlayerScore(Player player)
        {
            _dbContext.Players.Update(player);
            _dbContext.SaveChanges();
        }
       
        public void DeletePlayers()
        {
            _dbContext.Players.RemoveRange(_dbContext.Players);
            _dbContext.SaveChanges();
        }
    }
}
