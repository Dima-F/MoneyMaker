﻿using System;
using System.Collections.Generic;
using System.Linq;
using HandHistories.SimpleObjects.Entities;
using MoneyMaker.BLL.Stats;
using MoneyMaker.BLL.Tools;

namespace MoneyMaker.UI.Light.BLL
{
    /// <summary>
    /// Ф:Формирует коллекцию статов в зависимости от файла конфигурации для игроков, которые участвовали по крайней мере в 
    /// трьох последних играх
    /// </summary>
    public class ConditionalStatOperator:IStatOperator
    {
        public List<PlayerStats> GetPlayerStatsList(IEnumerable<Game> games)
        {
            var gs = games as Game[] ?? games.ToArray();
            var last3GamesPlayerNames = gs.GetLivePlayerNames(Properties.Settings.Default.LiveGamesCount);
            return last3GamesPlayerNames.Select(playerName => GetPlayerStats(playerName, gs)).ToList();
        }

        private PlayerStats GetPlayerStats(string playerName, IEnumerable<Game> games)
        {
            var statCollection = new PlayerStats(playerName);
            var playerGames = games.GetGamesForPlayer(playerName).ToList();
            var gamesCount = playerGames.Count();
            if (Properties.Settings.Default.Stat_Win)//win % stat
            {
                var handsWon = playerGames.GetHandsWonCountForPlayerGames(playerName);
                statCollection.Add(new Stat() { Name = "Win %", Value = Math.Round((double)handsWon / gamesCount * 100, 2) });
            }
            if (Properties.Settings.Default.Stat_Hands)//hands
                statCollection.Add(new Stat() { Name = "Hands", Value = gamesCount });

            if (Properties.Settings.Default.Stat_VPIP)//VPIP
            {
                var valPutCount = playerGames.VPIP_CountForPlayer(playerName);
                statCollection.Add(new Stat() { Name = "VPIP", Value = Math.Round((double)valPutCount / gamesCount * 100, 2) });
            }
            if (Properties.Settings.Default.Stat_EP_VPIP)//EP VPIP
            {
                var valPutCount = playerGames.VPIP_EP_CountForPlayer(playerName);
                statCollection.Add(new Stat() { Name = "EP VPIP", Value = Math.Round((double)valPutCount / gamesCount * 100, 2) });
            }
            if (Properties.Settings.Default.Stat_MP_VPIP)//MP VPIP
            {
                var valPutCount = playerGames.VPIP_MP_CountForPlayer(playerName);
                statCollection.Add(new Stat() { Name = "MP VPIP", Value = Math.Round((double)valPutCount / gamesCount * 100, 2) });
            }
            if (Properties.Settings.Default.Stat_LP_VPIP)//LP VPIP
            {
                var valPutCount = playerGames.VPIP_LP_CountForPlayer(playerName);
                statCollection.Add(new Stat() { Name = "LP VPIP", Value = Math.Round((double)valPutCount / gamesCount * 100, 2) });
            }
            if (Properties.Settings.Default.Stat_Profit)//Profit
            {
                var profit = playerGames.CalculateTotalProfit(playerName);
                statCollection.Add(new Stat() { Name = "Profit", Value = Math.Round(profit, 2) });
            }
            if (Properties.Settings.Default.Stat_PFR)//PFR
            {
                var preflopRaiseCount = playerGames.PFR_CountForPlayer(playerName);
                statCollection.Add(new Stat() { Name = "PFR", Value = Math.Round((double)preflopRaiseCount / gamesCount * 100, 2) });
            }
            if (Properties.Settings.Default.Stat_EP_PFR)//EP PFR
            {
                var preflopRaiseCount = playerGames.PFR_EP_CountForPlayer(playerName);
                statCollection.Add(new Stat() { Name = "EP PFR", Value = Math.Round((double)preflopRaiseCount / gamesCount * 100, 2) });
            }
            if (Properties.Settings.Default.Stat_MP_PFR)//MP PFR
            {
                var preflopRaiseCount = playerGames.PFR_MP_CountForPlayer(playerName);
                statCollection.Add(new Stat() { Name = "MP PFR", Value = Math.Round((double)preflopRaiseCount / gamesCount * 100, 2) });
            }
            if (Properties.Settings.Default.Stat_LP_PFR)//LP PFR
            {
                var preflopRaiseCount = playerGames.PFR_LP_CountForPlayer(playerName);
                statCollection.Add(new Stat() { Name = "LP PFR", Value = Math.Round((double)preflopRaiseCount / gamesCount * 100, 2) });
            }
            if (Properties.Settings.Default.Stat_ATS)//ATS
            {
                var atsPercent = playerGames.ATS_PercentForPlayer(playerName);
                statCollection.Add(new Stat() { Name = "ATS", Value = Math.Round(atsPercent, 2) });
            }
            if (Properties.Settings.Default.Stat_ATS_CO)//CO ATS
            {
                var atsPercent = playerGames.ATS_CO_PercentForPlayer(playerName);
                statCollection.Add(new Stat() { Name = "CO ATS", Value = Math.Round(atsPercent, 2) });
            }
            if (Properties.Settings.Default.Stat_ATS_B)//B ATS
            {
                var atsPercent = playerGames.ATS_B_PercentForPlayer(playerName);
                statCollection.Add(new Stat() { Name = "B ATS", Value = Math.Round(atsPercent, 2) });
            }
            if (Properties.Settings.Default.Stat_ATS_SB)//SB ATS
            {
                var atsPercent = playerGames.ATS_SB_PercentForPlayer(playerName);
                statCollection.Add(new Stat() { Name = "SB ATS", Value = Math.Round(atsPercent, 2) });
            }
            if (Properties.Settings.Default.Stat_BB)
                statCollection.Add(new Stat() { Name = "BB", Value = Math.Round(playerGames.Last().BB_ForPlayer(playerName)) });

            if (Properties.Settings.Default.Stat_AF) //AF
            {
                var afPercent = playerGames.AF_ForPlayer(playerName);
                statCollection.Add(new Stat() { Name = "AF", Value = Math.Round(afPercent, 2) });
            }
            if (Properties.Settings.Default.Stat_AF_Flop) //AF Flop
            {
                var afPercent = playerGames.AF_Flop_ForPlayer(playerName);
                statCollection.Add(new Stat() { Name = "AF Flop", Value = Math.Round(afPercent, 2) });
            }
            if (Properties.Settings.Default.Stat_AF_Turn) //AF Turn
            {
                var afPercent = playerGames.AF_Turn_ForPlayer(playerName);
                statCollection.Add(new Stat() { Name = "AF Turn", Value = Math.Round(afPercent, 2) });
            }
            if (Properties.Settings.Default.Stat_AF_River) //AF River
            {
                var afPercent = playerGames.AF_River_ForPlayer(playerName);
                statCollection.Add(new Stat() { Name = "AF River", Value = Math.Round(afPercent, 2) });
            }
            if (Properties.Settings.Default.Stat_3B)//3B
            {
                var preflop3BCount = playerGames.ThreeBetCountForPlayer(playerName);
                statCollection.Add(new Stat() { Name = "3B", Value = Math.Round((double)preflop3BCount / gamesCount * 100, 2) });
            }
            if (Properties.Settings.Default.Stat_Fold_SB_ToSteal)//Stat Fb to Steal
            {
                var fSb = playerGames.Fold_SB_To_Steal_ForPlayer(playerName);
                statCollection.Add(new Stat() { Name = "Fold SB to Stl", Value = Math.Round(fSb, 2) });
            }
            if (Properties.Settings.Default.Stat_Fold_BB_ToSteal)//Stat Bb to Steal
            {
                var fBb = playerGames.Fold_BB_To_Steal_ForPlayer(playerName);
                statCollection.Add(new Stat() { Name = "Fold BB to Stl", Value = Math.Round(fBb, 2) });
            }
            if (Properties.Settings.Default.Stat_WMWSF)
            {
                var wmwsf = playerGames.WMWSF_ForPlayer(playerName);
                statCollection.Add(new Stat() { Name = "W$WSF", Value = Math.Round(wmwsf, 2) });
            }
            if (Properties.Settings.Default.Stat_WTSD)
            {
                var wtsd = playerGames.WTSD_ForPlayer(playerName);
                statCollection.Add(new Stat() { Name = "WTSD", Value = Math.Round(wtsd, 2) });
            }
            return statCollection;
        }
    }
}
