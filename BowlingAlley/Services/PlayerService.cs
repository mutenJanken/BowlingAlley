﻿using BowlingAlley.Entities;
using BowlingAlley.Repository;
using BowlingAlley.Singleton;
using BowlingAlley.Utilities;

namespace BowlingAlley.Services
{
    // PlayerService agerar som en mellanhand mellan PlayerRepo och GameFacade
    // men också som en entrypoint till databas hanteringen via PlayerRepo(_playerRepo).
    // PlayerService innehåller ett gäng metoder med logic för att lägga till, hämta,
    // uppdatera och ta bort spelare via PlayerRepo.
    // Loggar också till log.txt när en spelare läggs till eller tas bortfrån databasen.
    public class PlayerService
    {

        private readonly PlayerRepo _playerRepo;

        public PlayerService(PlayerRepo playerRepo)
        {
            _playerRepo = playerRepo;
        }
                
        public void AddPlayer()
        {
            bool isRunning = true;
            // när fullGrpCounter blir 2 stängs loopen
            int fullGroupCounter = 0;
            // Kontrollerar att det inte redan finns 2 spelare
            if (!VerifyPlayersCount())
            {
                ConsoleAlerts.Error("Players already added.");
                isRunning = false;
            }

            while (isRunning)
            {
                Console.WriteLine("Enter player name: ");
                string playerName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(playerName))
                {
                    ConsoleAlerts.Error("Must contain atleast 1 character");
                }
                else
                {
                    // skapar en spelare och skickar spelare till PlayerRepo
                    var player = new Player(playerName);
                    _playerRepo.AddPlayer(player);

                    ConsoleAlerts.Success($"Player ({playerName}) has been added.");
                    Logger.Instance.Log($"ADDED Player: ({playerName}) to DB: T-SQL(BowlingAlleyDb)");
                    fullGroupCounter++;
                    if (fullGroupCounter >= 2)
                    {
                        ConsoleAlerts.Success("Two players have been added, ready to play.");
                        isRunning = false;
                    }
                }
            }
        }

        public List<Player> GetAvaliablePlayers()
        {
            return _playerRepo.GetPlayers();
        }

        public void UpdatePlayerScore(Player player)
        {
            _playerRepo.UpdatePlayerScore(player);
        }

        public void DeletePlayers()
        {

            var players = _playerRepo.GetPlayers();
            foreach(Player player in players)
            {
                Logger.Instance.Log($"DELETED Player: ({player.Name}) from DB: T-SQL(BowlingAlleyDb)");
            }
            _playerRepo.DeletePlayers();
        }

        public bool VerifyPlayersCount()
        {
            var players = _playerRepo.GetPlayers();
            if (players.Count < 2)
            {
                return true;
            }
            return false;
        }
    }
}
