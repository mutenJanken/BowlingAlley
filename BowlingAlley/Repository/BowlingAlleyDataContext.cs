using BowlingAlley.Entities;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace BowlingAlley.Repository
{
    // En DataContext klass som implementerar DbContext och innehåller en referens till
    // en DbSet<Player> som sedan används för att hämta och spara spelare till
    // databasen.
    // Har också skapat en metod som returnerar connection strängen från en app.config fil.
    public class BowlingAlleyDataContext : DbContext
    {
        public BowlingAlleyDataContext(DbContextOptions<BowlingAlleyDataContext> options) : base(options) { }

        public DbSet<Player> Players { get; set; }

        public static string GetConnString()
        {
            return ConfigurationManager.ConnectionStrings["BowlingAlleyDb"].ConnectionString;
        }
    }
}
