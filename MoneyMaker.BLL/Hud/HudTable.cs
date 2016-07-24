using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HandHistories.SimpleObjects.Entities;
using HandHistories.SimpleObjects.Tools;
using HandHistories.SimpleParser;
using MoneyMaker.BLL.Files;
using MoneyMaker.BLL.Stats;
using MoneyMaker.BLL.Tools;

namespace MoneyMaker.BLL.Hud
{

    /// <summary>
    /// Ф:Клас-парсер в режиме реального времени  с одновременным возвратом необходимых статистических данных 
    /// без сохранения в базе данных. Т.е. он использует только данные одного стола текущей сессии.
    /// </summary>
    public  class HudTable
    { 
        private readonly List<Game> _games;

        private readonly StatOperator _statOperator;

        private readonly PokerParser _parser;

        private readonly string _shortPath;

        public HudTable(StatOperator statOperator, string path)
        {
            _shortPath = System.IO.Path.GetFileNameWithoutExtension(path);
            var text = PokerFileReader.ReadFileWithWaiting(path);
            _parser = ParserFactory.CreateParser(_shortPath);
            _games = _parser.ParseGames(text);
            _statOperator = statOperator;
        }

        public virtual List<PlayerStats> GetPlayerStatsList()
        {
            return _statOperator.GetPlayerStatsList(_games);
        }

        public virtual string GetHudInfo()
        {
            var builder = new StringBuilder();
            foreach (var row  in _parser.GetInfoFromPath(_shortPath))
            {
                builder.AppendLine(row.Key + " : " + row.Value);
            }
            //inner information
            builder.AppendLine("Played hands: " + _games.Count);
            builder.AppendLine("Sitting for: " + GetTimeSession() + " minutes");
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
            return lastGame.GetMuck();
        }

        public IEnumerable<double> GetHeroProfits()
        {
            return _games.Select(game => game.CalculateHeroProfit());
        }
        
        private int GetTimeSession()
        {
            TimeSpan start = _games.First().DateOfHand.TimeOfDay;
            TimeSpan end = _games.Last().DateOfHand.TimeOfDay;
            if (end >= start)
                return Convert.ToInt32((end - start).TotalMinutes);
            var timeBefore = (new TimeSpan(23, 59, 59)) - start;
            return Convert.ToInt32((timeBefore + end).TotalMinutes);
        }

    }
}
