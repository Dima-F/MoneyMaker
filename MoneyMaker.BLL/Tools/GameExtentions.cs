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

        public static IEnumerable<Game> GetLive(this IEnumerable<Game> games, int liveGamesCount)
        {
            var enumerable = games as Game[] ?? games.ToArray();
            var gamesCount = enumerable.Length;
            return enumerable.Skip(gamesCount - liveGamesCount);
        }

        public static IEnumerable<string> GetLivePlayerNames(this IEnumerable<Game> games, int lastGamesCount)
        {
            var enumerable = games as Game[] ?? games.ToArray();
            var gamesCount = enumerable.Length;
            var last3Games = enumerable.Skip(gamesCount - lastGamesCount);
            var playerHistories = new List<PlayerHistory>();
            foreach (var g in last3Games)
            {
                playerHistories.AddRange(g.PlayerHistories);
            }
            return playerHistories.Select(p => p.PlayerName).Distinct();
        }

        public static IEnumerable<string> GetAllPlayerNames(this IEnumerable<Game> games)
        {
            var playerHistories = new List<PlayerHistory>();
            foreach (var g in games)
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
                if (playerHistory.HandActions.Any(ha =>ha.IsWinAction()))
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
                if (playerHistory.HandActions.PreflopHandActions().NotBlinds().Any(ha=>
                    ha.IsCallAction() || ha.IsRaiseAction()))
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
                if (playerHistory.HandActions.PreflopHandActions().NotBlinds().Any(ha =>
                    ha.IsCallAction() || ha.IsRaiseAction()))
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
                if (playerHistory.HandActions.PreflopHandActions().NotBlinds().Any(ha =>
                    ha.IsCallAction() || ha.IsRaiseAction()))
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
                if (playerHistory.HandActions.PreflopHandActions().NotBlinds().Any(ha =>
                    ha.IsCallAction() || ha.IsRaiseAction()))
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
                if (playerHistory.HandActions.PreflopHandActions().RealActions().Any(ha=>ha.IsRaiseAction()))
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
                if (playerHistory.HandActions.PreflopHandActions().RealActions().Any(ha => ha.IsRaiseAction()))
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
                if (playerHistory.HandActions.PreflopHandActions().RealActions().Any(ha => ha.IsRaiseAction()))
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
                if (playerHistory.HandActions.PreflopHandActions().RealActions().Any(ha => ha.IsRaiseAction()))
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

        public static double Fold_SB_To_Steal_ForPlayer(this IEnumerable<Game> games, string player)
        {
            var stealed = 0;
            var foldedAfterStealing = 0;
            foreach (var g in games)
            {
                var plr = g.PlayerHistories.Find(p => p.PlayerName == player);
                if(plr.Position!=PositionType.SB)
                    continue;
                if (!g.WasStealToPlayer(plr))
                    continue;
                stealed++;
                if (plr.FirstRealAction().HandActionType == HandActionType.FOLD)
                    foldedAfterStealing++;
            }
            return stealed == 0 ? 0 : (double)foldedAfterStealing / stealed * 100;
        }

        public static double Fold_BB_To_Steal_ForPlayer(this IEnumerable<Game> games, string player)
        {
            var stealed = 0;
            var foldedAfterStealing = 0;
            foreach (var g in games)
            {
                var plr = g.PlayerHistories.Find(p => p.PlayerName == player);
                if (plr.Position != PositionType.BB)
                    continue;
                if (!g.WasStealToPlayer(plr))
                    continue;
                stealed++;
                if (plr.FirstRealAction().HandActionType == HandActionType.FOLD)
                    foldedAfterStealing++;
            }
            return stealed == 0 ? 0 : (double)foldedAfterStealing / stealed * 100;
        }

        public static double ATS_PercentForPlayer(this IEnumerable<Game> games, string player)
        {
            var atsSituationCount = 0;
            var atsRaiseCount = 0;
            foreach (var g in games)
            {
                var plr = g.PlayerHistories.Find(p => p.PlayerName == player);
                if(!g.IsStealSituationForPlayer(plr))
                    continue;
                atsSituationCount++;
                if (plr.FirstRealAction().IsRaiseAction())
                    atsRaiseCount++;
            }
            return atsSituationCount == 0 ? 0 : (double)atsRaiseCount / atsSituationCount * 100;
        }

        public static double ATS_CO_PercentForPlayer(this IEnumerable<Game> games, string player)
        {
            var atsSituationCount = 0;
            var atsRaiseCount = 0;
            foreach (var g in games)
            {
                var plr = g.PlayerHistories.Find(p => p.PlayerName == player);
                if(plr.Position!=PositionType.CO)
                    continue;
                if (!g.IsStealSituationForPlayer(plr))
                    continue;
                atsSituationCount++;
                if (plr.FirstRealAction().IsRaiseAction())
                    atsRaiseCount++;
            }
            return atsSituationCount == 0 ? 0 : (double)atsRaiseCount / atsSituationCount * 100;
        }

        public static double ATS_B_PercentForPlayer(this IEnumerable<Game> games, string player)
        {
            var atsSituationCount = 0;
            var atsRaiseCount = 0;
            foreach (var g in games)
            {
                var plr = g.PlayerHistories.Find(p => p.PlayerName == player);
                if (plr.Position != PositionType.B)
                    continue;
                if (!g.IsStealSituationForPlayer(plr))
                    continue;
                atsSituationCount++;
                if (plr.FirstRealAction().IsRaiseAction())
                    atsRaiseCount++;
            }
            return atsSituationCount == 0 ? 0 : (double)atsRaiseCount / atsSituationCount * 100;
        }

        public static double ATS_SB_PercentForPlayer(this IEnumerable<Game> games, string player)
        {
            var atsSituationCount = 0;
            var atsRaiseCount = 0;
            foreach (var g in games)
            {
                var plr = g.PlayerHistories.Find(p => p.PlayerName == player);
                if (plr.Position != PositionType.SB)
                    continue;
                if (!g.IsStealSituationForPlayer(plr))
                    continue;
                atsSituationCount++;
                if (plr.FirstRealAction().IsRaiseAction())
                    atsRaiseCount++;
            }
            return atsSituationCount == 0 ? 0 : (double)atsRaiseCount / atsSituationCount * 100;
        }

        public static int ThreeBetCountForPlayer(this IEnumerable<Game> games, string player)
        {
            var count = 0;
            foreach (Game g in games)
            {
                var playerHistory = g.PlayerHistories.Find(ph => ph.PlayerName == player);
                if (!playerHistory.HandActions.PreflopHandActions().RaiseActions().Any()) continue;
                var rrIndex = playerHistory.HandActions.PreflopHandActions().RaiseActions().First().Index;
                if (g.PlayerHistories.Any(ph => ph.PlayerName != player && ph.HandActions.PreflopHandActions().Any( a =>
                    a.IsRaiseAction() && a.Index < rrIndex)))
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
        /// Win money when saw flop
        /// </summary>
        public static double WMWSF_ForPlayer(this IEnumerable<Game> games, string player)
        {
            var sawFlopCount = 0;
            var winWherSawFlop = 0;
            foreach (var g in games)
            {
                if(g.SawFlopForPlayer(player))
                    sawFlopCount++;
                else continue;
                if (g.WinGameForPlayer(player))
                    winWherSawFlop++;
            }
            return sawFlopCount == 0 ? 0 : (double)winWherSawFlop / sawFlopCount * 100;
        }

        /// <summary>
        /// Went to showdown
        /// </summary>
        public static double WTSD_ForPlayer(this IEnumerable<Game> games, string player)
        {
            var enumGames = games as Game[] ?? games.ToArray();
            var sawFlopCount = enumGames.Select(g => g.PlayerHistories.First(p => p.PlayerName == player)).
                Count(ph => ph.HandActions.Any(ha => ha.HandActionType == HandActionType.SHOW));
            return sawFlopCount == 0 ? 0 : (double)sawFlopCount / enumGames.Count() * 100;
        }

        public static List<HandAction> AllPreflopHandActions(this Game game)
        {
            var has=new List<HandAction>();
            foreach (var playerHistory in game.PlayerHistories)
            {
                has.AddRange(playerHistory.HandActions.Where(ha => ha.Street == Street.Preflop));
            }
            return has;
        }

        public static List<HandAction> AllFlopHandActions(this Game game)
        {
            var has = new List<HandAction>();
            foreach (var playerHistory in game.PlayerHistories)
            {
                has.AddRange(playerHistory.HandActions.Where(ha => ha.Street == Street.Flop));
            }
            return has;
        }

        public static List<HandAction> AllTurnHandActions(this Game game)
        {
            var has = new List<HandAction>();
            foreach (var playerHistory in game.PlayerHistories)
            {
                has.AddRange(playerHistory.HandActions.Where(ha => ha.Street == Street.Turn));
            }
            return has;
        }

        public static List<HandAction> AllRiverHandActions(this Game game)
        {
            var has = new List<HandAction>();
            foreach (var playerHistory in game.PlayerHistories)
            {
                has.AddRange(playerHistory.HandActions.Where(ha => ha.Street == Street.River));
            }
            return has;
        }

        public static bool SawFlopForPlayer(this Game game, string player)
        {
            var ph = game.PlayerHistories.First(p => p.PlayerName == player);
            return
                ph.HandActions.RealActions()
                    .Any(ha => ha.Street == Street.Flop || ha.HandActionType == HandActionType.SHOW);
        }

        public static bool WinGameForPlayer(this Game game, string player)
        {
            var ph = game.PlayerHistories.First(p => p.PlayerName == player);
            return ph.HandActions.Any(ha => ha.IsWinAction());
        }

        public static PlayerHistory GetPlayerHistoryForAction(this Game game, HandAction ha)
        {
            return game.PlayerHistories.FirstOrDefault(ph => ph.PlayerName== ha.PlayerName);
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

        /// <summary>
        /// Ф:Проверяет ситуацию, когда кто-то сделал рейз из позней позиции в неоткрытом банке
        /// </summary>
        private static bool WasStealToPlayer (this Game game, PlayerHistory ph)
        {
            var playersInStealSituations =
                game.PlayerHistories.Where(p => p.PlayerName != ph.PlayerName && game.IsStealSituationForPlayer(p));
            return playersInStealSituations.Any(p=>p.HandActions.RealActions().First().IsRaiseAction());
        }
        /// <summary>
        /// Ф:Проверяет стиллинговую ситуацию для игрока
        /// </summary>
        private static bool IsStealSituationForPlayer(this Game game, PlayerHistory playerHistory)
        {
            var playersCount = game.PlayerHistories.Count;
            if (playersCount < 3)
                return false;
            if (!playerHistory.HandActions.PreflopHandActions().RealActions().Any())
                return false;
            if (!playerHistory.IsInStealPosition())
                return false;
            var allPreflopHandActions = game.AllPreflopHandActions().RealActions().ToList();
            var positionAction = allPreflopHandActions.Where(ha => ha.PlayerName == playerHistory.PlayerName).OrderBy(ha => ha.Index).First();

            if (!allPreflopHandActions.AllFoldedBeforeAction(positionAction))
                return false;
            return true;
        }


        
    }
}
