using BowlingAlley.Facades;
using BowlingAlley.Repository;
using BowlingAlley.Services;

namespace BowlingAlley
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameFacade gameFacade = new(BowlingAlleyDataContext.GetConnString());
            gameFacade.ShowGameMenu();
        }
    }
}
