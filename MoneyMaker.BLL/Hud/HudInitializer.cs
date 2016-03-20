﻿using System;
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
    /// Ф:Клас-парсер в режиме реального времени с одновременным возвратом необходимых статистических данных 
    /// без сохранения в базе данных. Т.е. он использует только данные текущей сессии. 
    /// </summary>
    public  class HudInitializer
    {
        private readonly List<Game> _games;

        private readonly IStatOperator _statOperator;

        private readonly List<HandAction> _allHandActions;

        public HudInitializer(IStatOperator statOperator, string path)
        {
            Path = path;
            var text = PokerFileReader.ReadFileWithWaiting(Path);
            var parser = ParserFactory.CreateParser(System.IO.Path.GetFileNameWithoutExtension(path));
            _games = parser.ParseGames(text);
            _allHandActions = new List<HandAction>();
            _statOperator = statOperator;
        }

        public string Path { get; }

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

        public virtual List<PlayerStats> GetPlayerStatsList()
        {
            return _statOperator.GetPlayerStatsList(_games);
        }

        public virtual string GetHudInfo()
        {
            var fileName = System.IO.Path.GetFileNameWithoutExtension(Path);
            var builder = new StringBuilder();
            builder.AppendLine("Date: " + fileName.GetDateFromFileName().ToShortDateString());
            builder.AppendLine("Table name: " + fileName.GetTableFromFileName());
            builder.AppendLine("Limit: " + fileName.GetLimitFromFileName());
            builder.AppendLine("Blinds: " + fileName.GetBlindsFromFileName());
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
            return lastGame.WasMucking() ? lastGame.GetMuck() : null;
        }

        public IEnumerable<decimal> GetHeroProfits()
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
