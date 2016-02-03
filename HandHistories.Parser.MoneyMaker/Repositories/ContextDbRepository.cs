using System;
using System.Collections.Generic;
using System.Linq;
using HandHistories.Parser.MoneyMaker.EntityFramework;
using HandHistories.SimpleObjects.Entities;
using MoneyMaker.BLL.Tools;
using MoneyMaker.BLL.ViewEntities;

namespace HandHistories.Parser.MoneyMaker.Repositories
{

    /// <summary>
    /// Ф:Хранилище на основании контекста БД EF
    /// </summary>
    public  class ContextDbRepository:IRepository
    {
        private HandHistoryContext _context;

        public ContextDbRepository(HandHistoryContext context)
        {
            _context = context;
            _context.Database.Initialize(false);
        }

        public IEnumerable<string> OponentNames
        {
            get { return _context.PlayerHistories.GetDistinctPlayerNames(); }
        }

        //todo:need to refactor and has grouping bag
        public IEnumerable<PlayerSummary> GetPlayerSummariesByName(string name)
        {
            if(!OponentNames.Contains(name))
                throw new Exception(string.Format("Player with name {0} is not found!", name));
            var playerGames = _context.Games.GetGamesForPlayer(name).ToList();//* 2 qs
            var limits = playerGames.GetDistinctLimits();//* 1qs
            foreach (var limit in limits)
            {
                SeatType l = limit;
                var limitPlayerGames = playerGames.GetGamesForLimit(l);
                var handsWon = _context.HandActions.GetWonActionsCountForPlayer(name);

                var sessionGroups = from lg in limitPlayerGames
                    group lg by new {lg.TableName, lg.DateOfHand.Date};

                var playerSummary = new PlayerSummary
                {
                    Name = name,
                    Limit = limit,
                    Hands = limitPlayerGames.Count(),
                    HandsWon = handsWon,
                    Sessions = sessionGroups.Count()
                };
                playerSummary.HandsWonPercent =
                    decimal.Round((decimal) playerSummary.HandsWon/(decimal) playerSummary.Hands*100, 2);
                yield return playerSummary;
            }
        }

        //todo:need to refactor
        public IEnumerable<PlayerStatistics> GetPlayerStatisticsByName(string name)
        {
            if (!OponentNames.Contains(name))
                throw new Exception(string.Format("Player with name {0} is not found!", name));
            var playerGames = _context.Games.GetGamesForPlayer(name).ToList();
            var limits = playerGames.GetDistinctLimits();
            foreach (var l in limits)
            {
                SeatType limit = l;
                var limitGames = playerGames.GetGamesForLimit(limit).ToList();
                var limitGameCount = limitGames.Count();
                var valPutCount = limitGames.VPIPCountForPlayer(name);
                var preflopRaiseCount = limitGames.PFRCountForPlayer(name);
                var atsPercent = limitGames.GetATSPercentForPlayer(name);
                var afpfPercent = limitGames.GetAFPercentForPlayer(name);
                var playerStatistics = new PlayerStatistics
                {
                    Name = name,
                    Limit = l,
                    VPIP = decimal.Round((decimal)valPutCount/(decimal)limitGameCount*100,2),
                    PFR = decimal.Round((decimal)preflopRaiseCount / (decimal)limitGameCount * 100, 2),
                    ATS = decimal.Round((decimal)atsPercent, 2),
                    AF = decimal.Round((decimal)afpfPercent, 2)
                };
                yield return playerStatistics;
            }
        }
    }
}
