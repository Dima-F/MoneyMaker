using System;
using System.Collections.Generic;
using System.Linq;
using HandHistories.SimpleObjects.Entities;
using MoneyMaker.BLL.Tools;
using MoneyMaker.BLL.ViewEntities;

namespace HandHistories.Parser.MoneyMaker.Repositories
{
    public class LiveRepository:IRepository
    {
        private List<Game> _games;
        private List<HandAction> _actions;
        private List<PlayerHistory> _playerHistories; 

        public LiveRepository(List<Game> games)
        {
            Games = games;
        }

        public IEnumerable<string> OponentNames
        {
            get { throw new NotImplementedException(); }
        }

        public List<PlayerHistory> PlayerHistories
        {
            get { return _playerHistories; }
            set { _playerHistories = value; }
        }

        public List<Game> Games
        {
            get { return _games; }
            set { _games = value; }
        }

        public List<HandAction> HandActions
        {
            get { return _actions; }
            set { _actions = value; }
        }

        public IEnumerable<PlayerSummary> GetPlayerSummariesByName(string name)
        {
            if (!OponentNames.Contains(name))
                throw new Exception(string.Format("Player with name {0} is not found!", name));
            var playerGames = Games.GetGamesForPlayer(name).ToList();//* 2 qs
            var limits = playerGames.GetDistinctLimits();//* 1qs
            foreach (var limit in limits)
            {
                SeatType l = limit;
                var limitPlayerGames = playerGames.GetGamesForLimit(l);
                var handsWon = HandActions.GetWonActionsCountForPlayer(name);

                var sessionGroups = from lg in limitPlayerGames
                                    group lg by new { lg.TableName, lg.DateOfHand.Date };

                var playerSummary = new PlayerSummary
                {
                    Name = name,
                    Limit = limit,
                    Hands = limitPlayerGames.Count(),
                    HandsWon = handsWon,
                    Sessions = sessionGroups.Count()
                };
                playerSummary.HandsWonPercent =
                    decimal.Round((decimal)playerSummary.HandsWon / (decimal)playerSummary.Hands * 100, 2);
                yield return playerSummary;
            }
        }

        public IEnumerable<PlayerStatistics> GetPlayerStatisticsByName(string name)
        {
            if (!OponentNames.Contains(name))
                throw new Exception(string.Format("Player with name {0} is not found!", name));
            var playerGames = _games.GetGamesForPlayer(name).ToList();
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
                    VPIP = decimal.Round((decimal)valPutCount / (decimal)limitGameCount * 100, 2),
                    PFR = decimal.Round((decimal)preflopRaiseCount / (decimal)limitGameCount * 100, 2),
                    ATS = decimal.Round((decimal)atsPercent, 2),
                    AF = decimal.Round((decimal)afpfPercent, 2)
                };
                yield return playerStatistics;
            }
        }
    }
}
