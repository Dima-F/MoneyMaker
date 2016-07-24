using System;
using System.Collections.Generic;
using System.Linq;
using HandHistories.SimpleObjects.Entities;
using MoneyMaker.BLL.Tools;

namespace MoneyMaker.BLL.Stats
{
    /// <summary>
    /// Ф:Формирует полную коллекцию статов для всех существующих игроков
    /// </summary>
    public class BaseStatOperator : StatOperator
    {
        public BaseStatOperator(bool useOnlyLive, int lastGames) : base(useOnlyLive, lastGames) { }
        protected override PlayerStats GetPlayerStats(string playerName, IEnumerable<Game> games)
        {
            var statCollection = new PlayerStats(playerName);
            var playerGames = games.GetGamesForPlayer(playerName).ToList();
            if (true)//win % stat
            {
                var wp = playerGames.GetHandsWonPercentForPlayerGames(playerName);
                statCollection.Add(new Stat() { Name = "Win %", Value = Math.Round(wp, 2) });
            }
            if (true)//hands
                statCollection.Add(new Stat() { Name = "Hands", Value = playerGames.Count() });

            if (true)//VPIP
            {
                var vpip = playerGames.VPIP_ForPlayer(playerName);
                statCollection.Add(new Stat() { Name = "VPIP", Value = Math.Round(vpip, 2) });
            }
            if (true)//EP VPIP
            {
                var vpip = playerGames.VPIP_EP_ForPlayer(playerName);
                statCollection.Add(new Stat() { Name = "EP VPIP", Value = Math.Round(vpip, 2) });
            }
            if (true)//MP VPIP
            {
                var vpip = playerGames.VPIP_MP_ForPlayer(playerName);
                statCollection.Add(new Stat() { Name = "MP VPIP", Value = Math.Round(vpip, 2) });
            }
            if (true)//LP VPIP
            {
                var vpip = playerGames.VPIP_LP_ForPlayer(playerName);
                statCollection.Add(new Stat() { Name = "LP VPIP", Value = Math.Round(vpip, 2) });
            }
            if (true)//Profit
            {
                var profit = playerGames.CalculateTotalProfit(playerName);
                statCollection.Add(new Stat() { Name = "Profit", Value = Math.Round(profit, 2) });
            }
            if (true)//PFR
            {
                var prc = playerGames.PFR_ForPlayer(playerName);
                statCollection.Add(new Stat() { Name = "PFR", Value = Math.Round(prc, 2) });
            }
            if (true)//EP PFR
            {
                var prc = playerGames.PFR_EP_ForPlayer(playerName);
                statCollection.Add(new Stat() { Name = "EP PFR", Value = Math.Round(prc, 2) });
            }
            if (true)//MP PFR
            {
                var prc = playerGames.PFR_MP_ForPlayer(playerName);
                statCollection.Add(new Stat() { Name = "MP PFR", Value = Math.Round(prc, 2) });
            }
            if (true)//LP PFR
            {
                var prc = playerGames.PFR_LP_ForPlayer(playerName);
                statCollection.Add(new Stat() { Name = "LP PFR", Value = Math.Round(prc, 2) });
            }
            if (true)//ATS
            {
                var atsPercent = playerGames.ATS_PercentForPlayer(playerName);
                statCollection.Add(new Stat() { Name = "ATS", Value = Math.Round(atsPercent, 2) });
            }
            if (true)//CO ATS
            {
                var atsPercent = playerGames.ATS_CO_PercentForPlayer(playerName);
                statCollection.Add(new Stat() { Name = "CO ATS", Value = Math.Round(atsPercent, 2) });
            }
            if (true)//B ATS
            {
                var atsPercent = playerGames.ATS_B_PercentForPlayer(playerName);
                statCollection.Add(new Stat() { Name = "B ATS", Value = Math.Round(atsPercent, 2) });
            }
            if (true)//SB ATS
            {
                var atsPercent = playerGames.ATS_SB_PercentForPlayer(playerName);
                statCollection.Add(new Stat() { Name = "SB ATS", Value = Math.Round(atsPercent, 2) });
            }
            if (true)
                statCollection.Add(new Stat() { Name = "BB", Value = Math.Round(playerGames.Last().BB_ForPlayer(playerName)) });

            if (true) //AF
            {
                var afPercent = playerGames.AF_ForPlayer(playerName);
                statCollection.Add(new Stat() { Name = "AF", Value = Math.Round(afPercent, 2) });
            }
            if (true) //AF Flop
            {
                var afPercent = playerGames.AF_Flop_ForPlayer(playerName);
                statCollection.Add(new Stat() { Name = "AF Flop", Value = Math.Round(afPercent, 2) });
            }
            if (true) //AF Turn
            {
                var afPercent = playerGames.AF_Turn_ForPlayer(playerName);
                statCollection.Add(new Stat() { Name = "AF Turn", Value = Math.Round(afPercent, 2) });
            }
            if (true) //AF River
            {
                var afPercent = playerGames.AF_River_ForPlayer(playerName);
                statCollection.Add(new Stat() { Name = "AF River", Value = Math.Round(afPercent, 2) });
            }
            if (true)//3B
            {
                var thbc = playerGames.ThreeBet_ForPlayer(playerName);
                statCollection.Add(new Stat() { Name = "3B", Value = Math.Round(thbc, 2) });
            }
            if (true)//Stat Fb to Steal
            {
                var fSb = playerGames.Fold_SB_To_Steal_ForPlayer(playerName);
                statCollection.Add(new Stat() { Name = "Fold SB to Stl", Value = Math.Round(fSb, 2) });
            }
            if (true)//Stat Bb to Steal
            {
                var fBb = playerGames.Fold_BB_To_Steal_ForPlayer(playerName);
                statCollection.Add(new Stat() { Name = "Fold BB to Stl", Value = Math.Round(fBb, 2) });
            }
            if (true)
            {
                var wmwsf = playerGames.WMWSF_ForPlayer(playerName);
                statCollection.Add(new Stat() { Name = "W$WSF", Value = Math.Round(wmwsf, 2) });
            }
            if (true)
            {
                var wtsd = playerGames.WTSD_ForPlayer(playerName);
                statCollection.Add(new Stat() { Name = "WTSD", Value = Math.Round(wtsd, 2) });
            }
            if (true)
            {
                var co = playerGames.CallOpen_ForPlayer(playerName);
                statCollection.Add(new Stat() { Name = "Call Open", Value = Math.Round(co, 2) });
            }
            if (true)
            {
                var cb = playerGames.Flop_CB(playerName);
                statCollection.Add(new Stat() { Name = "Flop CB", Value = Math.Round(cb, 2) });
            }
            return statCollection;
        }
    }
}
