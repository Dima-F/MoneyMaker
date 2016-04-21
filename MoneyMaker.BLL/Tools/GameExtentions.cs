using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using HandHistories.SimpleObjects.Entities;
using HandHistories.SimpleObjects.Tools;
using MoneyMaker.BLL.Hud;

namespace MoneyMaker.BLL.Tools
{
    public static class GameExtentions
    {
        public static IEnumerable<string> GetDistinctPlayerNames(this IEnumerable<PlayerHistory> playerHistories)
        {
            return playerHistories.Select(p => p.PlayerName).Distinct();
        }

        public static IEnumerable<string> GetLivePlayerNames(this IEnumerable<Game> games)
        {
            var lastGame = games.Last();
            return lastGame.PlayerHistories.Where(ph => ph.GameNumber == lastGame.GameNumber).Select(p => p.PlayerName).Distinct();
        }
        
        public static IEnumerable<string> GetLast3GamesPlayerNames(this IEnumerable<Game> games)
        {
            var enumerable = games as Game[] ?? games.ToArray();
            var gamesCount = enumerable.Length;
            var last3Games = enumerable.Skip(gamesCount - 3);
            var playerHistories = new List<PlayerHistory>();
            foreach (var g in last3Games)
            {
                playerHistories.AddRange(g.PlayerHistories);
            }
            return playerHistories.Select(p => p.PlayerName).Distinct();
        }

        public static IEnumerable<SeatType> GetDistinctLimits(this IEnumerable<Game> games)
        {
            return games.Where(g => g.SeatType != SeatType.Unknown).Select(g => g.SeatType).Distinct();
        }

        public static IEnumerable<Game> GetGamesForPlayer(this IEnumerable<Game> games, string player)
        {
            return games.Where(game => game.PlayerHistories.Any(ph =>ph.PlayerName == player));
        }
        

        public static int GetHandsWonCountForPlayerGames(this IEnumerable<Game> games, string player)
        {
            var count = 0;
            foreach (Game g in games)
            {
                var playerHistory = g.PlayerHistories.First(ph => ph.PlayerName == player);
                if (playerHistory.HandActions.Any(
                    ha =>
                        ha.HandActionType == HandActionType.WINS ||
                        ha.HandActionType == HandActionType.WINS_SIDE_POT ||
                        ha.HandActionType == HandActionType.WINS_MAIN_POT))
                    count++;
            }
            return count;
        }

        public static int VPIPCountForPlayer(this IEnumerable<Game> games, string player)
        {
            var count = 0;
            foreach (Game g in games)
            {
                var playerHistory = g.PlayerHistories.First(ph => ph.PlayerName == player);
                if (playerHistory.HandActions.Any(
                    ha=>ha.Street == Street.Preflop
                    && ha.HandActionType != HandActionType.BIG_BLIND
                    && ha.HandActionType != HandActionType.SMALL_BLIND
                    && (ha.HandActionType == HandActionType.CALL || ha.HandActionType == HandActionType.RAISE)))
                    count++;
            }
            return count;
        }

        public static int PFRCountForPlayer(this IEnumerable<Game> games, string player)
        {
            var count = 0;
            foreach (Game g in games)
            {
                var playerHistory = g.PlayerHistories.First(ph => ph.PlayerName == player);
                if (playerHistory.HandActions.Any(ha=>ha.Street == Street.Preflop  && ha.HandActionType == HandActionType.RAISE))
                    count++;
            }
            return count;
        }

        public static double GetAfPostflopPercentForPlayer(this IEnumerable<Game> games, string player)
        {
            var passiveActCount = 0;
            var agrassiveActCount = 0;
            foreach (var game in games)
            {
                var playerHistory = game.PlayerHistories.First(ph => ph.PlayerName == player);

                foreach (var action in playerHistory.HandActions.Where(ha=>ha.Street != Street.Preflop && ha.Street != Street.Showdown && ha.Street != Street.Null))
                {
                    switch (action.HandActionType)
                    {
                        case HandActionType.CALL:
                        case HandActionType.ALL_IN_CALL:
                        case HandActionType.CHECK:
                            passiveActCount++;
                            break;
                         case HandActionType.ALL_IN_RAISE:
                         case HandActionType.BET:
                         case HandActionType.ALL_IN_BET:
                         case HandActionType.RAISE:
                            agrassiveActCount++;
                            break;
                    }
                }
            }
            return Math.Round(passiveActCount == 0 ? agrassiveActCount : (double)agrassiveActCount /passiveActCount,2);
        }

        public static double GetATSPercentForPlayer(this IEnumerable<Game> games, string player)
        {
            //var atsSituationCount = 0;
            //var atsRaiseCount = 0;
            //foreach (var g in games)
            //{
            //    byte buttonPosition = g.ButtonPosition;
            //    byte cutofPosition = DefineCutoffPosition(g);
            //    byte smallBlindPosition = DefineSBPosition(g);
            //    //сначала проверяем, на какой позиции находится игрок - нас интересует CO и BTN
            //    byte playerSeatNumber = g.PlayerHistories.Find(p => p.PlayerName == player).SeatNumber;
            //    if (!(playerSeatNumber == buttonPosition || playerSeatNumber == cutofPosition || playerSeatNumber==smallBlindPosition))
            //        continue;
            //    //кэшируем все действия игроков на префлопе
            //    var allPlayersPreflopHandActions = g.HandActions.Where(ha => !string.IsNullOrEmpty(ha.Source) && ha.Street == Street.Preflop).ToList();
            //    //действия игрока
            //    var playerPreflopHandActions = allPlayersPreflopHandActions.Where(ha => ha.Source == player);
            //    //действия всех остальных игроков до флопа кроме блайндов
            //    var otherPlayersPreflopActions = allPlayersPreflopHandActions.Where(ha => ha.Source != player
            //        && ha.HandActionType != HandActionType.BIG_BLIND
            //        && ha.HandActionType != HandActionType.SMALL_BLIND);
            //    //если кто-то из них сказал кол или рейз, это не стиллингова ситуация
            //    if (otherPlayersPreflopActions.Any(ha => ha.HandActionType == HandActionType.CALL || ha.HandActionType == HandActionType.RAISE))
            //        continue;
            //    //здесь стилинговая ситуация
            //    atsSituationCount++;
            //    if (playerPreflopHandActions.Any(ha => ha.HandActionType == HandActionType.RAISE))
            //        atsRaiseCount++;//возможность стила и игрок сделал рейз
            //}
            //return atsSituationCount == 0 ? 0 : (decimal)atsRaiseCount / atsSituationCount * 100;
            return 1;
        }


        //todo:work with index
        public static int Get3BCountForPlayer(this IEnumerable<Game> games, string player)
        {
            var count = 0;
            foreach (Game g in games)
            {
                
                var playerHistory = g.PlayerHistories.First(ph => ph.PlayerName == player);
                if (playerHistory.HandActions.Any(ha => ha.Street == Street.Preflop && ha.HandActionType == HandActionType.RAISE))
                {
                    var rrAmount =
                        playerHistory.HandActions.Find(
                            ha => ha.Street == Street.Preflop && ha.HandActionType == HandActionType.RAISE).Amount;
                    if (g.PlayerHistories.Any(ph => ph.PlayerName != player && ph.HandActions.Any(
                        a =>
                            a.Street == Street.Preflop && a.HandActionType == HandActionType.RAISE &&
                            a.Amount < rrAmount)))//игрок сделал рейз, теперь проверим, есть ли рейз на прейлопе из меньшим значением (т.е. он будет до нас)
                        count++;
                }
            }
            return count;
        }

        public static double GetBBForPlayer(this Game game, string player)
        {
            var ph = game.PlayerHistories.First(p => p.PlayerName == player);
            return ph.StartingStack/game.BigBlind;
        }
        
        /// <summary>
        /// Ф:Следует проверять возвращенное значение на null
        /// </summary>
        public static byte[] GetHeroCardsFromLastGame(this IEnumerable<Game> games)
        {
            var lastGame = games.Last();
            var hero = lastGame.PlayerHistories.FirstOrDefault(ph => ph.PlayerName == lastGame.Hero);
            return hero != null ? hero.HoleCards : null;
        }
        
        public static Muck GetMuck(this Game game)
        {
            foreach (var playerHistory in game.PlayerHistories)
            {
                foreach (var handAction in playerHistory.HandActions)
                {
                    if (handAction.HandActionType == HandActionType.MUCKS)
                    {
                        var cards = playerHistory.HoleCards.ConvertByteCardsToString();
                        return new Muck {PlayerName = playerHistory.PlayerName, Cards = cards};
                    }
                }
            }
            return null;
        }
        

        public static double CalculateTotalProfit(this IEnumerable<Game> games, string player)
        {
            double sum = 0;
            foreach (Game g in games)
            {
                var playerHistory = g.PlayerHistories.First(ph => ph.PlayerName == player);
                sum += playerHistory.HandActions.Sum(ha => ha.Amount);
            }
            return sum;
        }

        public static double CalculateHeroProfit(this Game game)
        {
            double sum = 0;
            var playerHistory = game.PlayerHistories.Find(ph => ph.PlayerName == game.Hero);
            if (playerHistory == null) return 0;
            sum += playerHistory.HandActions.Sum(ha => ha.Amount);
            return sum;
        }




        #region Workers
        private static byte DefineCutoffPosition(Game g)
        {
            byte buttonPosition = g.ButtonPosition;
            //позиции, на которых сидят игроки
            var positions = g.PlayerHistories.Select(player => player.SeatNumber).OrderBy(p => p).ToList();
            if (positions.First() != buttonPosition)
            {
                var index = positions.IndexOf(buttonPosition) - 1;
                return positions[index];
            }
            else return positions.Max();
        }

        private static byte DefineSBPosition(Game g)
        {
            byte buttonPosition = g.ButtonPosition;
            //позиции, на которых сидят игроки
            var positions = g.PlayerHistories.Select(player => player.SeatNumber).OrderBy(p => p).ToList();
            if (positions.Last() != buttonPosition)
            {
                var index = positions.IndexOf(buttonPosition) + 1;
                return positions[index];
            }
            else return positions.Min();
        }
        #endregion
    }
}
