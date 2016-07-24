using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandHistories.SimpleObjects.Entities;

namespace MoneyMaker.BLL.Tools
{
    public static class StatGamesExtentions
    {
        public static double VPIP_ForPlayer(this IEnumerable<Game> playerGames, string player)
        {
            var VPIP_count = 0;
            var pgs = playerGames.ToArray();
            foreach (Game g in pgs)
            {
                var playerHistory = g.PlayerHistories.First(ph => ph.PlayerName == player);
                if (playerHistory.HandActions.PreflopHandActions().NotBlinds().Any(ha =>
                    ha.IsCallAction() || ha.IsRaiseAction()))
                    VPIP_count++;
            }
            return !pgs.Any() ? 0 : (double)VPIP_count / pgs.Count() * 100;
        }

        public static double VPIP_EP_ForPlayer(this IEnumerable<Game> games, string player)
        {
            var VPIP_count = 0;
            var pgs = games.ToArray();
            foreach (Game g in pgs)
            {
                var playerHistory = g.PlayerHistories.First(ph => ph.PlayerName == player);
                if (!playerHistory.IsEerlyPosition())
                    continue;
                if (playerHistory.HandActions.PreflopHandActions().NotBlinds().Any(ha =>
                    ha.IsCallAction() || ha.IsRaiseAction()))
                    VPIP_count++;
            }
            return !pgs.Any() ? 0 : (double)VPIP_count / pgs.Count() * 100;
        }

        public static double VPIP_MP_ForPlayer(this IEnumerable<Game> games, string player)
        {
            var VPIP_count = 0;
            var pgs = games.ToArray();
            foreach (Game g in pgs)
            {
                var playerHistory = g.PlayerHistories.First(ph => ph.PlayerName == player);
                if (!playerHistory.IsMiddlePosition())
                    continue;
                if (playerHistory.HandActions.PreflopHandActions().NotBlinds().Any(ha =>
                    ha.IsCallAction() || ha.IsRaiseAction()))
                    VPIP_count++;
            }
            return !pgs.Any() ? 0 : (double)VPIP_count / pgs.Count() * 100;
        }

        public static double VPIP_LP_ForPlayer(this IEnumerable<Game> games, string player)
        {
            var VPIP_count = 0;
            var pgs = games.ToArray();
            foreach (Game g in pgs)
            {
                var playerHistory = g.PlayerHistories.First(ph => ph.PlayerName == player);
                if (!playerHistory.IsLatePosition())
                    continue;
                if (playerHistory.HandActions.PreflopHandActions().NotBlinds().Any(ha =>
                    ha.IsCallAction() || ha.IsRaiseAction()))
                    VPIP_count++;
            }
            return !pgs.Any() ? 0 : (double)VPIP_count / pgs.Count() * 100;
        }

        public static double PFR_ForPlayer(this IEnumerable<Game> games, string player)
        {
            var pfrCount = 0;
            var pgs = games.ToArray();
            foreach (Game g in pgs)
            {
                var playerHistory = g.PlayerHistories.First(ph => ph.PlayerName == player);
                if (playerHistory.HandActions.PreflopHandActions().RealActions().Any(ha => ha.IsRaiseAction()))
                    pfrCount++;
            }
            return !pgs.Any() ? 0 : (double)pfrCount / pgs.Count() * 100;
        }

        public static double PFR_EP_ForPlayer(this IEnumerable<Game> games, string player)
        {
            var pfrCount = 0;
            var pgs = games.ToArray();
            foreach (Game g in pgs)
            {
                var playerHistory = g.PlayerHistories.First(ph => ph.PlayerName == player);
                if (!playerHistory.IsEerlyPosition())
                    continue;
                if (playerHistory.HandActions.PreflopHandActions().RealActions().Any(ha => ha.IsRaiseAction()))
                    pfrCount++;
            }
            return !pgs.Any() ? 0 : (double)pfrCount / pgs.Count() * 100;
        }

        public static double PFR_MP_ForPlayer(this IEnumerable<Game> games, string player)
        {
            var pfrCount = 0;
            var pgs = games.ToArray();
            foreach (Game g in pgs)
            {
                var playerHistory = g.PlayerHistories.First(ph => ph.PlayerName == player);
                if (!playerHistory.IsMiddlePosition())
                    continue;
                if (playerHistory.HandActions.PreflopHandActions().RealActions().Any(ha => ha.IsRaiseAction()))
                    pfrCount++;
            }
            return !pgs.Any() ? 0 : (double)pfrCount / pgs.Count() * 100;
        }

        public static double PFR_LP_ForPlayer(this IEnumerable<Game> games, string player)
        {
            var pfrCount = 0;
            var pgs = games.ToArray();
            foreach (Game g in pgs)
            {
                var playerHistory = g.PlayerHistories.First(ph => ph.PlayerName == player);
                if (!playerHistory.IsLatePosition())
                    continue;
                if (playerHistory.HandActions.PreflopHandActions().RealActions().Any(ha => ha.IsRaiseAction()))
                    pfrCount++;
            }
            return !pgs.Any() ? 0 : (double)pfrCount / pgs.Count() * 100;
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
            return Math.Round(callCount == 0 ? betOrRaiseCount : (double)betOrRaiseCount / callCount, 2);
        }

        public static double AF_Flop_ForPlayer(this IEnumerable<Game> games, string player)
        {
            var betOrRaiseCount = 0;
            var callCount = 0;
            foreach (var game in games)
            {
                var playerHistory = game.PlayerHistories.Find(ph => ph.PlayerName == player);
                foreach (var action in playerHistory.HandActions.Where(ha => ha.Street == Street.Flop))
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
                if (plr.Position != PositionType.SB)
                    continue;
                if (!g.WasStealToPlayer(plr))
                    continue;
                stealed++;
                if (plr.FirstRealActionOnPreflop().HandActionType == HandActionType.FOLD)
                    foldedAfterStealing++;
            }
            return stealed == 0 ? 0 : (double)foldedAfterStealing / stealed * 100;
        }

        /// <summary>
        /// Cold call preflop (Call Open) is when a player calls a preflop raise when he doesn’t have any money in the pot yet, including blinds. 
        /// </summary>
        public static double CallOpen_ForPlayer(this IEnumerable<Game> games, string player)
        {
            var raisedCount = 0;
            var callsCount = 0;
            foreach (var g in games)
            {
                var plr = g.PlayerHistories.Find(p => p.PlayerName == player);
                if (!plr.HandActions.PreflopHandActions().RealActions().Any())
                    continue;
                var firstActionIndex = plr.FirstRealActionOnPreflop().Index;
                var otherActionsBefore = g.AllPreflopHandActions().Where(ha => ha.PlayerName != player && ha.Index < firstActionIndex);
                if (otherActionsBefore.Any(ha => ha.IsRaiseAction()))
                    raisedCount++;
                else continue;
                if (plr.FirstRealActionOnPreflop().IsCallAction())
                    callsCount++;
            }
            return raisedCount == 0 ? 0 : (double)callsCount / raisedCount * 100;
        }

        public static double Flop_CB(this IEnumerable<Game> games, string player)
        {
            var raisedOnPreflopCount = 0;
            var cbCount = 0;
            foreach (var g in games)
            {
                var plr = g.PlayerHistories.Find(p => p.PlayerName == player);
                if (!plr.HandActions.FlopHandActions().RealActions().Any())
                    continue;
                if (plr.LastRealActionOnPreflop().IsRaiseAction())
                    raisedOnPreflopCount++;
                else continue;
                if (plr.FirstRealActionOnFlop().IsBetAction())
                    cbCount++;
            }
            return raisedOnPreflopCount == 0 ? 0 : (double)cbCount / raisedOnPreflopCount * 100;
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
                if (plr.FirstRealActionOnPreflop().HandActionType == HandActionType.FOLD)
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
                if (!g.IsStealSituationForPlayer(plr))
                    continue;
                atsSituationCount++;
                if (plr.FirstRealActionOnPreflop().IsRaiseAction())
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
                if (plr.Position != PositionType.CO)
                    continue;
                if (!g.IsStealSituationForPlayer(plr))
                    continue;
                atsSituationCount++;
                if (plr.FirstRealActionOnPreflop().IsRaiseAction())
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
                if (plr.FirstRealActionOnPreflop().IsRaiseAction())
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
                if (plr.FirstRealActionOnPreflop().IsRaiseAction())
                    atsRaiseCount++;
            }
            return atsSituationCount == 0 ? 0 : (double)atsRaiseCount / atsSituationCount * 100;
        }

        public static double ThreeBet_ForPlayer(this IEnumerable<Game> games, string player)
        {
            var threeBetCount = 0;
            var pgs = games.ToArray();
            foreach (Game g in pgs)
            {
                var playerHistory = g.PlayerHistories.Find(ph => ph.PlayerName == player);
                if (!playerHistory.HandActions.PreflopHandActions().RaiseActions().Any()) continue;
                var rrIndex = playerHistory.HandActions.PreflopHandActions().RaiseActions().First().Index;
                if (g.PlayerHistories.Any(ph => ph.PlayerName != player && ph.HandActions.PreflopHandActions().Any(a =>
                   a.IsRaiseAction() && a.Index < rrIndex)))
                    threeBetCount++;
            }
            return !pgs.Any() ? 0 : (double)threeBetCount / pgs.Count() * 100;
        }

        public static double BB_ForPlayer(this Game game, string player)
        {
            var ph = game.PlayerHistories.First(p => p.PlayerName == player);
            return ph.StartingStack / game.BigBlind;
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
                if (g.SawFlopForPlayer(player))
                    sawFlopCount++;
                else continue;
                if (g.IsWinGameForPlayer(player))
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
            var showdownCount = enumGames.Select(g => g.PlayerHistories.First(p => p.PlayerName == player)).
                Count(ph => ph.HandActions.Any(ha => ha.HandActionType == HandActionType.SHOW));
            return showdownCount == 0 ? 0 : (double)showdownCount / enumGames.Count() * 100;
        }
    }
}
