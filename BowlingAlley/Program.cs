using BowlingAlley.Facades;
using BowlingAlley.Repository;
using BowlingAlley.Services;
using Microsoft.EntityFrameworkCore;

namespace BowlingAlley
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BowlingAlleyDataContext>();
            optionsBuilder.UseSqlServer(BowlingAlleyDataContext.GetConnString());

            using BowlingAlleyDataContext dbContext = new(optionsBuilder.Options);
            PlayerRepo playerRepo = new(dbContext);
            PlayerService playerService = new(playerRepo);
            GameFacade gameFacade = new(playerService);

            gameFacade.ShowGameMenu();
        }
    }
}
