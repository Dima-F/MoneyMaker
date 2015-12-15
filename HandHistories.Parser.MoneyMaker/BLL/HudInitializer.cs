using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using HandHistories.Parser.MoneyMaker.BLL.ViewEntities;
using HandHistories.Parser.MoneyMaker.Tools;
using HandHistories.SimpleObjects.Entities;
using HandHistories.SimpleParser;

namespace HandHistories.Parser.MoneyMaker.BLL
{
    /// <summary>
    /// Ф:Уникальный класс, используется в качестве парсера в режиме реального времени с одновременным возвратом необходимых статистических данных 
    /// без сохранения в базе данных. Т.е. он использует только данные текущей сессии. Корректно отслеживает изменение только в одном файле(можно 
    /// играть только за одним столом).
    /// </summary>
    public class HudInitializer
    {
        private readonly IHandHistoryParser _innerParser;

        public HudInitializer(IHandHistoryParser innerParser)
        {
            _innerParser = innerParser;
        }

        public string Path { get; set; }

        public IEnumerable<HudStatistics> ParseHudStatistics()
        {
            var games = ParseGames();
            var livePlayerNames = games.GetLivePlayerNames();
            var handActions = ParseHandActions();
            return livePlayerNames.Select(playerName => GetHudStatisticsByName(playerName, games, handActions));
        }

        public string GetHudInfo()
        {
            var fileName = System.IO.Path.GetFileNameWithoutExtension(Path);
            var builder = new StringBuilder();
            builder.AppendLine("Date: " + fileName.GetDateFromFileName().ToShortDateString());
            builder.AppendLine("Table name: "+ fileName.GetTableFromFileName());
            builder.AppendLine("Limit: " + fileName.GetLimitFromFileName());
            builder.AppendLine("Blinds: " + fileName.GetBlindsFromFileName());
            builder.AppendLine("Played hands: " + ParseGames().Count);
            builder.AppendLine("Sitting for: " + ParseTimeSession()+" minutes");
            return builder.ToString();
        }

        public string GetHeroCards()
        {
            var heroCards = ParseGames().GetHeroCardsFromLastGame();
            return heroCards != null ? heroCards.ConvertByteCardsToString() : string.Empty;
        }

        public string GetMucking()
        {
            var lastGame = ParseGames().Last();
            return lastGame.WasMucking() ? lastGame.GetPlayerAndMuckedCardsAsString() : "Wait for mucking...";
        }





        private List<Game> ParseGames()
        {
            string allText = ReadFile(Path);
            return _innerParser.ParseGames(allText);
        }

        private List<HandAction> ParseHandActions()
        {
            string allText = ReadFile(Path);
            return _innerParser.ParseHandActions(allText);
        }

        private int ParseTimeSession()
        {
            var games = ParseGames();
            TimeSpan start = games.First().DateOfHand.TimeOfDay;
            var end = games.Last().DateOfHand.TimeOfDay;
            return Convert.ToInt32((end - start).TotalMinutes);
        }

        /// <summary>
        /// Ф:Читает один конкретный текстовый файл.
        /// </summary>
        private string ReadFile(string fileName)
        {
            var file = new FileInfo(fileName);
            var builder = new StringBuilder();
            var fs = file.OpenRead();
            using (var reader = new StreamReader(fs, Encoding.Default))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    builder.AppendLine(line);
                }
            }
            builder.Append("\n\n");
            fs.Close();
            return builder.ToString();
        }

        private HudStatistics GetHudStatisticsByName(string name, IEnumerable<Game> games, IEnumerable<HandAction> handActions)
        {
            var playerGames = games.GetGamesForPlayer(name).ToList();
            var handsWon = handActions.GetHandsWonCountForPlayer(name);
            var gamesCount = playerGames.Count();
            var valPutCount = playerGames.VPIPCountForPlayer(name);
            var preflopRaiseCount = playerGames.PFRCountForPlayer(name);
            var atsPercent = playerGames.GetATSPercentForPlayer(name);
            var afpfPercent = playerGames.GetAFPercentForPlayer(name);
            var hudStatistics = new HudStatistics
            {
                Name = name,
                Hands = gamesCount,
                HandsWonPercent = decimal.Round((decimal)handsWon / (decimal)gamesCount * 100, 2),
                VPIP = decimal.Round((decimal)valPutCount / (decimal)gamesCount * 100, 2),
                PFR = decimal.Round((decimal)preflopRaiseCount / (decimal)gamesCount * 100, 2),
                ATS = decimal.Round((decimal)atsPercent, 2),
                AF = decimal.Round((decimal)afpfPercent, 2)
            };
            return hudStatistics;
        }
    }
}
