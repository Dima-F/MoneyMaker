﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using HandHistories.SimpleObjects.Entities;
using HandHistories.SimpleObjects.Tools;
using HandHistories.SimpleParser;
using MoneyMaker.BLL.Tools;
using MoneyMaker.BLL.ViewEntities;

namespace MoneyMaker.BLL
{
    
    /// <summary>
    /// Ф:Клас-парсер в режиме реального времени с одновременным возвратом необходимых статистических данных 
    /// без сохранения в базе данных. Т.е. он использует только данные текущей сессии. 
    /// </summary>
    public class HudInitializer:IHudInitializer
    {
        private readonly List<Game> _games;

        private List<HandAction> _allHandActions; 

        public HudInitializer(IHandHistoryParser parser, string path)
        {
            Path = path;
            _games = ParseGames(parser);
            _allHandActions=new List<HandAction>();
        }

        public string Path { get; private set; }

        //Lazy load pattern
        public List<HandAction> AllHandActions
        {
            get
            {
                if(_allHandActions.Count!=0)
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
            var livePlayerNames = _games.GetLivePlayerNames();
            return livePlayerNames.Select(GetHudStatisticsByName);
        }

        public string GetHudInfo()
        {
            var fileName = System.IO.Path.GetFileNameWithoutExtension(Path);
            var builder = new StringBuilder();
            builder.AppendLine("Date: " + fileName.GetDateFromFileName().ToShortDateString());
            builder.AppendLine("Table name: "+ fileName.GetTableFromFileName());
            builder.AppendLine("Limit: " + fileName.GetLimitFromFileName());
            builder.AppendLine("Blinds: " + fileName.GetBlindsFromFileName());
            builder.AppendLine("Played hands: " + _games.Count);
            builder.AppendLine("Sitting for: " + ParseTimeSession()+" minutes");
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
            string allText = ReadFile(Path);
            return parser.ParseGames(allText);
        }

        private int ParseTimeSession()
        {
            TimeSpan start = _games.First().DateOfHand.TimeOfDay;
            var end = _games.Last().DateOfHand.TimeOfDay;
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

        private HudStatistics GetHudStatisticsByName(string name)
        {
            var playerGames = _games.GetGamesForPlayer(name).ToList();
            var handsWon = _allHandActions.GetHandsWonCountForPlayer(name);
            var gamesCount = playerGames.Count();
            var valPutCount = playerGames.VPIPCountForPlayer(name);
            var preflopRaiseCount = playerGames.PFRCountForPlayer(name);
            var atsPercent = playerGames.GetATSPercentForPlayer(name);
            var afpfPercent = playerGames.GetAFPercentForPlayer(name);
            var profit = playerGames.CalculateTotalProfit(name);
            var hudStatistics = new HudStatistics
            {
                Name = name,
                Hands = gamesCount,
                WinPercent = decimal.Round((decimal)handsWon / (decimal)gamesCount * 100, 2),
                Profit = decimal.Round((decimal)profit,2),
                VPIP = decimal.Round((decimal)valPutCount / (decimal)gamesCount * 100, 2),
                PFR = decimal.Round((decimal)preflopRaiseCount / (decimal)gamesCount * 100, 2),
                ATS = decimal.Round((decimal)atsPercent, 2),
                AF = decimal.Round((decimal)afpfPercent, 2)
            };
            return hudStatistics;
        }
    }
}