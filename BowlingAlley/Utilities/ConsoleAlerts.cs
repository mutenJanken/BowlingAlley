using BowlingAlley.Singleton;

namespace BowlingAlley.Utilities
{
    // Utility klass som hanterar utskrift av "viktigare" konsol meddelanden
    // till användaren så som felmeddelanden, lyckade operationer och spelresultat.
    // Rensar också konsolen och skriver ut meddelanden i lite... spännande färger -.-.

    public static class ConsoleAlerts
    {
        public static void Error(string message)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Thread.Sleep(2000);
            Console.ResetColor();
            Console.Clear();
        }

        public static void Success(string message)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Thread.Sleep(1000);
            Console.ResetColor();
            Console.Clear();
        }

        public static void GameResult(string winner, string loser)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(winner);
            Logger.Instance.Log(winner);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(loser);
            Logger.Instance.Log(loser);
            Console.ReadKey();
            Console.ResetColor();
            Console.Clear();
        }

        public static void Draw(string message)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ReadKey();
            Console.ResetColor();
            Console.Clear();
        }
    }
}
