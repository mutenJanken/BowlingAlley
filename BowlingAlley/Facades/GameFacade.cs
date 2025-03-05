using BowlingAlley.Entities;
using BowlingAlley.Services;
using BowlingAlley.Utilities;
using System.Text;

namespace BowlingAlley.Facades
{

    // Facade pattern klass som hanterar all logik för att skapa och spela ett spel.
    // Innehåller en Menu som visar om spelare finns tillgängliga. Tillgängliga
    // spelar namn hämtas ifrån Databasen(T-SQL) via metoden DisplayPlayers().
    // Facade klassen använder sig av PlayerService för att hämta spelare och
    // ScoreContext för att beräkna spelar poäng. Beräkna kanske är att ta i då
    // poängen slumpas fram, men men.
    // DetermineWinnerAndLoser metoden kontrollerar vilken spelare som vunnit och 
    // vilken som förlorat och skriver ut resultatet via utility klassen ConsoleAlerts.

    // Har också valt att ta bort båda spelarna från databasen vid program exit.

    public class GameFacade
    {
        private readonly PlayerService _playerService;
        public GameFacade(PlayerService playerService)
        {
            _playerService = playerService;
        }

        public void ShowGameMenu()
        {
            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine(" - BowlingAlley Autoplay -\n");
                Console.WriteLine($"Avaliable players: \n{DisplayPlayers()}");
                Console.WriteLine("1.] Play");
                Console.WriteLine("2.] Add 2 Players");
                Console.WriteLine("3.] Exit");
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            Console.Clear();
                            Play();
                            break;
                        case 2:
                            Console.Clear();
                            _playerService.AddPlayer();
                            break;
                        case 3:
                            _playerService.DeletePlayers();
                            isRunning = false;
                            break;
                    }
                }
                else
                {
                    ConsoleAlerts.Error("Invalid input, try 1, 2 or 3.");
                }
            }
        }

        public void Play()
        {
            var players = _playerService.GetAvaliablePlayers();
            // Är players mindre än 2
            if (_playerService.VerifyPlayersCount())
            {
                ConsoleAlerts.Error("Not Enough players, try adding player.");
            }
            else
            {
                // Sätter vilken strategi som ska användas.
                ScoreContext scoreContext = new();
                scoreContext.SetScoreStrategy(new RandomScoreStrategy());

                // Loopar igenom players och sätter ett score för varje spelare
                foreach (var player in players)
                {
                    player.Score = scoreContext.CalculateScore();
                    // Uppdaterar spelar poäng i DB
                    _playerService.UpdatePlayerScore(player);
                }
                // Kollar vem som vunnit och förlorat.
                EvaluateGameOutcome(players);
            }
        }

        public string DisplayPlayers()
        {
            var players = _playerService.GetAvaliablePlayers();
            if (players.Count == 0)
            {
                return "No players avaliable\n";
            }

            var stringPlayers = new StringBuilder();
            foreach (var player in players)
            {
                stringPlayers.AppendLine(player.Name);
            }
            return stringPlayers.ToString();
        }

        // (Match resultat loggas till log.txt fast inom ConsoleAlerts klassen.)
        public void EvaluateGameOutcome(List<Player> players)
        {
            if (players[0].Score > players[1].Score)
            {
                string winner = $"Winner: {players[0].Name}! Score: {players[0].Score}";
                string loser = $"Loser: {players[1].Name}! Score: {players[1].Score}";
                ConsoleAlerts.GameResult(winner, loser);
            }
            else if (players[0].Score < players[1].Score)
            {
                string winner = $"Winner: {players[1].Name}! Score: {players[1].Score}";
                string loser = $"Loser: {players[0].Name}! Score: {players[0].Score}";
                ConsoleAlerts.GameResult(winner, loser);
            }
            else
            {
                ConsoleAlerts.Draw("It's a tie!");
            }
        }
    }
}
