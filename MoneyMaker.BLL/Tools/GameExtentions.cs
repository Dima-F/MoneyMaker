using System;
using System.Collections.Generic;
using System.Linq;
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

        public static int VPIP_CountForPlayer(this IEnumerable<Game> playerGames, string player)
        {
            var count = 0;
            foreach (Game g in playerGames)
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

        public static int VPIP_EP_CountForPlayer(this IEnumerable<Game> games, string player)
        {
            var count = 0;
            foreach (Game g in games)
            {
                var playerHistory = g.PlayerHistories.First(ph => ph.PlayerName == player);
                if (!playerHistory.IsEerlyPosition())
                    continue;
                if (playerHistory.HandActions.Any(
                    ha => ha.Street == Street.Preflop
                    && ha.HandActionType != HandActionType.BIG_BLIND
                    && ha.HandActionType != HandActionType.SMALL_BLIND
                    && (ha.HandActionType == HandActionType.CALL || ha.HandActionType == HandActionType.RAISE)))
                    count++;
            }
            return count;
        }

        public static int VPIP_MP_CountForPlayer(this IEnumerable<Game> games, string player)
        {
            var count = 0;
            foreach (Game g in games)
            {
                var playerHistory = g.PlayerHistories.First(ph => ph.PlayerName == player);
                if (!playerHistory.IsMiddlePosition())
                    continue;
                if (playerHistory.HandActions.Any(
                    ha => ha.Street == Street.Preflop
                    && ha.HandActionType != HandActionType.BIG_BLIND
                    && ha.HandActionType != HandActionType.SMALL_BLIND
                    && (ha.HandActionType == HandActionType.CALL || ha.HandActionType == HandActionType.RAISE)))
                    count++;
            }
            return count;
        }

        public static int VPIP_LP_CountForPlayer(this IEnumerable<Game> games, string player)
        {
            var count = 0;
            foreach (Game g in games)
            {
                var playerHistory = g.PlayerHistories.First(ph => ph.PlayerName == player);
                if (!playerHistory.IsLatePosition())
                    continue;
                if (playerHistory.HandActions.Any(
                    ha => ha.Street == Street.Preflop
                    && ha.HandActionType != HandActionType.BIG_BLIND
                    && ha.HandActionType != HandActionType.SMALL_BLIND
                    && (ha.HandActionType == HandActionType.CALL || ha.HandActionType == HandActionType.RAISE)))
                    count++;
            }
            return count;
        }

        public static int PFR_CountForPlayer(this IEnumerable<Game> games, string player)
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

        public static int PFR_EP_CountForPlayer(this IEnumerable<Game> games, string player)
        {
            var count = 0;
            foreach (Game g in games)
            {
                var playerHistory = g.PlayerHistories.First(ph => ph.PlayerName == player);
                if(!playerHistory.IsEerlyPosition())
                    continue;
                if (playerHistory.HandActions.Any(ha => ha.Street == Street.Preflop && ha.HandActionType == HandActionType.RAISE))
                    count++;
            }
            return count;
        }

        public static int PFR_MP_CountForPlayer(this IEnumerable<Game> games, string player)
        {
            var count = 0;
            foreach (Game g in games)
            {
                var playerHistory = g.PlayerHistories.First(ph => ph.PlayerName == player);
                if (!playerHistory.IsMiddlePosition())
                    continue;
                if (playerHistory.HandActions.Any(ha => ha.Street == Street.Preflop && ha.HandActionType == HandActionType.RAISE))
                    count++;
            }
            return count;
        }

        public static int PFR_LP_CountForPlayer(this IEnumerable<Game> games, string player)
        {
            var count = 0;
            foreach (Game g in games)
            {
                var playerHistory = g.PlayerHistories.First(ph => ph.PlayerName == player);
                if (!playerHistory.IsLatePosition())
                    continue;
                if (playerHistory.HandActions.Any(ha => ha.Street == Street.Preflop && ha.HandActionType == HandActionType.RAISE))
                    count++;
            }
            return count;
        }

        public static double AF_ForPlayer(this IEnumerable<Game> games, string player)
        {
            var betOrRaiseCount = 0;
            var callCount = 0;
            foreach (var game in games)
            {
                var playerHistory = game.PlayerHistories.Find(ph => ph.PlayerName == player);
                foreach (var action in playerHistory.HandActions)
                {
                    switch (action.HandActionType)
                    {
                        case HandActionType.CALL:
                        case HandActionType.ALL_IN_CALL:
                            callCount++;
                            break;
                         case HandActionType.ALL_IN_RAISE:
                         case HandActionType.BET:
                         case HandActionType.ALL_IN_BET:
                         case HandActionType.RAISE:
                            betOrRaiseCount++;
                            break;
                    }
                }
            }
            return Math.Round(callCount == 0 ? betOrRaiseCount : (double)betOrRaiseCount / callCount,2);
        }

        public static double AF_Flop_ForPlayer(this IEnumerable<Game> games, string player)
        {
            var betOrRaiseCount = 0;
            var callCount = 0;
            foreach (var game in games)
            {
                var playerHistory = game.PlayerHistories.Find(ph => ph.PlayerName == player);
                foreach (var action in playerHistory.HandActions.Where(ha=>ha.Street==Street.Flop))
                {
                    switch (action.HandActionType)
                    {
                        case HandActionType.CALL:
                        case HandActionType.ALL_IN_CALL:
                            callCount++;
                            break;
                        case HandActionType.ALL_IN_RAISE:
                        case HandActionType.BET:
                        case HandActionType.ALL_IN_BET:
                        case HandActionType.RAISE:
                            betOrRaiseCount++;
                            break;
                    }
                }
            }
            return Math.Round(callCount == 0 ? betOrRaiseCount : (double)betOrRaiseCount / callCount, 2);
        }

        public static double AF_Turn_ForPlayer(this IEnumerable<Game> games, string player)
        {
            var betOrRaiseCount = 0;
            var callCount = 0;
            foreach (var game in games)
            {
                var playerHistory = game.PlayerHistories.Find(ph => ph.PlayerName == player);
                foreach (var action in playerHistory.HandActions.Where(ha => ha.Street == Street.Turn))
                {
                    switch (action.HandActionType)
                    {
                        case HandActionType.CALL:
                        case HandActionType.ALL_IN_CALL:
                            callCount++;
                            break;
                        case HandActionType.ALL_IN_RAISE:
                        case HandActionType.BET:
                        case HandActionType.ALL_IN_BET:
                        case HandActionType.RAISE:
                            betOrRaiseCount++;
                            break;
                    }
                }
            }
            return Math.Round(callCount == 0 ? betOrRaiseCount : (double)betOrRaiseCount / callCount, 2);
        }

        public static double AF_River_ForPlayer(this IEnumerable<Game> games, string player)
        {
            var betOrRaiseCount = 0;
            var callCount = 0;
            foreach (var game in games)
            {
                var playerHistory = game.PlayerHistories.Find(ph => ph.PlayerName == player);
                foreach (var action in playerHistory.HandActions.Where(ha => ha.Street == Street.River))
                {
                    switch (action.HandActionType)
                    {
                        case HandActionType.CALL:
                        case HandActionType.ALL_IN_CALL:
                            callCount++;
                            break;
                        case HandActionType.ALL_IN_RAISE:
                        case HandActionType.BET:
                        case HandActionType.ALL_IN_BET:
                        case HandActionType.RAISE:
                            betOrRaiseCount++;
                            break;
                    }
                }
            }
            return Math.Round(callCount == 0 ? betOrRaiseCount : (double)betOrRaiseCount / callCount, 2);
        }

        public static double ATS_PercentForPlayer(this IEnumerable<Game> games, string player)
        {
            var atsSituationCount = 0;
            var atsRaiseCount = 0;
            foreach (var g in games)
            {
                var stealPlayer = g.PlayerHistories.Find(p => p.PlayerName == player);
                if(!(stealPlayer.Position==PositionType.B || stealPlayer.Position == PositionType.CO || stealPlayer.Position == PositionType.SB))
                    continue;
                //действия игрока на префлопе кроме малого блайнда
                var playerPreflopHandActions = stealPlayer.HandActions.Where(
                        ha => ha.Street == Street.Preflop && ha.HandActionType != HandActionType.SMALL_BLIND).ToArray();
                var minIndex = playerPreflopHandActions.Min(ha => ha.Index);
                var firstAction = playerPreflopHandActions.First(ha => ha.Index == minIndex);
                //действия остальных игроков кроме блайндов на префлопе
                var otherPlayersPreflopHandActions = new List<HandAction>();
                foreach (var ph in g.PlayerHistories.Where(ph => ph.PlayerName != player))
                {
                    otherPlayersPreflopHandActions.AddRange(ph.HandActions.Where(ha => ha.Street == Street.Preflop && ha.HandActionType != HandActionType.BIG_BLIND
                        && ha.HandActionType != HandActionType.SMALL_BLIND && ha.HandActionType!=HandActionType.DEALT_HERO_CARDS && ha.HandActionType!=HandActionType.UNCALLED_BET));
                }
                //проверяем стилинговую ситуацию. Все игроки до стилера кроме блайндов должны упасть
                if (!otherPlayersPreflopHandActions.Where(ha => ha.Index < minIndex)
                    .All(a => a.HandActionType == HandActionType.FOLD || a.HandActionType == HandActionType.CHECK))
                    continue;
                atsSituationCount++;
                if (firstAction.HandActionType == HandActionType.RAISE)
                    atsRaiseCount++;
            }
            return atsSituationCount == 0 ? 0 : (double)atsRaiseCount / atsSituationCount * 100;
        }
        
        public static int ThreeBetCountForPlayer(this IEnumerable<Game> games, string player)
        {
            var count = 0;
            foreach (Game g in games)
            {
                var playerHistory = g.PlayerHistories.First(ph => ph.PlayerName == player);
                if (!playerHistory.HandActions.Any(ha => ha.Street == Street.Preflop && ha.HandActionType == HandActionType.RAISE)) continue;
                var rrIndex = playerHistory.HandActions.Find(ha => ha.Street == Street.Preflop && ha.HandActionType == HandActionType.RAISE).Index;
                if (g.PlayerHistories.Any(ph => ph.PlayerName != player && ph.HandActions.Any( a =>
                    a.Street == Street.Preflop && a.HandActionType == HandActionType.RAISE && a.Index < rrIndex)))
                    count++;
            }
            return count;
        }

        public static double BB_ForPlayer(this Game game, string player)
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
        
    }
}
