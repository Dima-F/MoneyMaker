using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HandHistories.SimpleObjects.Entities;
using HandHistories.SimpleObjects.Tools;
using HandHistories.SimpleParser;
using MoneyMaker.BLL.Files;
using MoneyMaker.BLL.Tools;
using MoneyMaker.BLL.ViewEntities;

namespace MoneyMaker.BLL.Hud
{

    /// <summary>
    /// Ф:Клас-парсер в режиме реального времени с одновременным возвратом необходимых статистических данных 
    /// без сохранения в базе данных. Т.е. он использует только данные текущей сессии. 
    /// </summary>
    public class HudInitializer : IHudInitializer
    {
        private readonly List<Game> _games;

        private readonly List<HandAction> _allHandActions;

        public HudInitializer(IHandHistoryParser parser, string path)
        {
            Path = path;
            _games = ParseGames(parser);
            _allHandActions = new List<HandAction>();
        }

        public string Path { get; private set; }

        //Lazy load pattern
        public List<HandAction> AllHandActions
        {
            get
            {
                if (_allHandActions.Count != 0)
                    return _allHandActions;
                foreach (var game in _games)
                {
                    _allHandActions.AddRange(game.HandActions);
                }
                return _allHandActions;
            }
        }

        public IEnumerable<HudStatistics> ParseHudStatistics()
        {
            var last3GamesPlayerNames = _games.GetLast3GamesPlayerNames();
            return last3GamesPlayerNames.Select(GetHudStatisticsByName);
        }

        public string GetHudInfo()
        {
            var fileName = System.IO.Path.GetFileNameWithoutExtension(Path);
            var builder = new StringBuilder();
            builder.AppendLine("Date: " + fileName.GetDateFromFileName().ToShortDateString());
            builder.AppendLine("Table name: " + fileName.GetTableFromFileName());
            builder.AppendLine("Limit: " + fileName.GetLimitFromFileName());
            builder.AppendLine("Blinds: " + fileName.GetBlindsFromFileName());
            builder.AppendLine("Played hands: " + _games.Count);
            builder.AppendLine("Sitting for: " + ParseTimeSession() + " minutes");
            return builder.ToString();
        }

        public string GetHeroCards()
        {
            var heroCards = _games.GetHeroCardsFromLastGame();
            return heroCards != null ? heroCards.ConvertByteCardsToString() : string.Empty;
        }

        public Muck GetMucking()
        {
            var lastGame = _games.Last();
            return lastGame.WasMucking() ? lastGame.GetMuck() : null;
        }

        public IEnumerable<decimal> GetHeroProfits()
        {
            return _games.Select(game => game.CalculateHeroProfit());
        }

        private List<Game> ParseGames(IHandHistoryParser parser)
        {
            var allText = PokerFileReader.ReadFileWithWaiting(Path);
            return parser.ParseGames(allText);
            
        }

        private int ParseTimeSession()
        {
            TimeSpan start = _games.First().DateOfHand.TimeOfDay;
            var end = _games.Last().DateOfHand.TimeOfDay;
            return Convert.ToInt32((end - start).TotalMinutes);
        }

        private HudStatistics GetHudStatisticsByName(string name)
        {
            var playerGames = _games.GetGamesForPlayer(name).ToList();
            var handsWon = playerGames.GetHandsWonCountForPlayerGames(name);
            var gamesCount = playerGames.Count();
            var valPutCount = playerGames.VPIPCountForPlayer(name);
            var preflopRaiseCount = playerGames.PFRCountForPlayer(name);
            var preflop3BCount = playerGames.Get3BCountForPlayer(name);
            var atsPercent = playerGames.GetATSPercentForPlayer(name);
            var afpfPercent = playerGames.GetAFPercentForPlayer(name);
            var profit = playerGames.CalculateTotalProfit(name);
            var hudStatistics = new HudStatistics
            {
                Name = name,
                Hands = gamesCount,
                WinPercent = decimal.Round((decimal)handsWon / (decimal)gamesCount * 100, 2),
                Profit = decimal.Round((decimal)profit, 2),
                VPIP = decimal.Round((decimal)valPutCount / (decimal)gamesCount * 100, 2),
                PFR = decimal.Round((decimal)preflopRaiseCount / (decimal)gamesCount * 100, 2),
                ATS = decimal.Round((decimal)atsPercent, 2),
                AF = decimal.Round((decimal)afpfPercent, 2),
                ThB = decimal.Round((decimal)preflop3BCount / (decimal)gamesCount * 100, 2),
            };
            return hudStatistics;
        }

    }
}
