using BowlingAlley.Entities;

namespace BowlingAlley.Repository
{
    // En repository klass som hanterar CRUD operationer för Player
    // med entity framework.
    // Innerhåller en referens(_context) till en BowlingAlleyDataContext klass som
    // i sin tur används för att ge åtkomst till databasen.

    public class PlayerRepo
    {
        private readonly BowlingAlleyDataContext _dataContext;
        public PlayerRepo(BowlingAlleyDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void AddPlayer(Player playerName)
        {
            _dataContext.Players.Add(playerName);
            _dataContext.SaveChanges();
        }

        public List<Player> GetPlayers()
        {
            return _dataContext.Players.ToList();
        }

        public void UpdatePlayerScore(Player player)
        {
            _dataContext.Players.Update(player);
            _dataContext.SaveChanges();
        }
       
        public void DeletePlayers()
        {
            _dataContext.Players.RemoveRange(_dataContext.Players);
            _dataContext.SaveChanges();
        }
    }
}
