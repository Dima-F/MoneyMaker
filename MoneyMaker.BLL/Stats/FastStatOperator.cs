using System;
using System.Collections.Generic;
using System.Linq;
using HandHistories.SimpleObjects.Entities;
using MoneyMaker.BLL.Tools;

namespace MoneyMaker.BLL.Stats
{
    /// <summary>
    /// Ф:Попытка переделать BaseStatOperator с целью повышения производительности. Двома плюсами обозначены стати, которые удалось скомпоновать
    /// в одну итерацию перечисления всех игор. 
    /// На практике подобная стратегия существенного виишрыша не принесла, поэтому дальнейшее усовершенствование класса приостановлено.
    /// </summary>
    public class FastStatOperator : StatOperator
    {
        public FastStatOperator(bool useOnlyLive, int lastGames) : base(useOnlyLive, lastGames) { }

        protected override PlayerStats GetPlayerStats(string playerName, IEnumerable<Game> games)
        {
            var statCollection = new PlayerStats(playerName);
            var playerGames = games.GetGamesForPlayer(playerName).ToList();
            //counters...
            var pgCount = playerGames.Count;
            var winCount = 0;

            var vpipCount = 0;
            var vpipEpCount = 0;
            var vpipMpCount = 0;
            var vpipLpCount = 0;

            var pfrCount = 0;
            var pfrEpCount = 0;
            var pfrMpCount = 0;
            var pfrLpCount = 0;

            var totalProfit = 0.0d;
            var sawFlopCount = 0;
            var winWherSawFlop = 0;
            var showdownCount = 0;

            var afArray = new List<Af>()
            {
                new Af(),//general 0
                new Af(),//flop 1
                new Af(),//turn 2
                new Af()//river 3
            };

            var threeBetCount = 0;



            foreach (var g in playerGames)
            {
                var plH = g.PlayerHistories.First(ph => ph.PlayerName == playerName);
                winCount += plH.WinForPlayer();

                vpipCount += plH.Vpip();
                vpipEpCount += plH.VpipEp();
                vpipMpCount += plH.VpipMp();
                vpipLpCount += plH.VpipLp();

                pfrCount += plH.Pfr();
                pfrEpCount += plH.PfrEp();
                pfrMpCount += plH.PfrMp();
                pfrLpCount += plH.PfrLp();

                plH.AF_(afArray[0]);
                plH.AF_Flop(afArray[1]);
                plH.AF_Turn(afArray[2]);
                plH.AF_River(afArray[3]);

                threeBetCount += plH.ThreeBet(g);
                totalProfit += plH.HandProfit();

                if (g.SawFlopForPlayer(playerName))
                {
                    sawFlopCount++;
                    if (g.IsWinGameForPlayer(playerName))
                        winWherSawFlop++;
                }

                showdownCount += plH.ReachShowdown();

            }
            if (true)//win % stat ++
            {
                statCollection.Add(new Stat() { Name = "Win %", Value = Math.Round((double)winCount/pgCount * 100, 2) });
            }
            if (true)//hands ++
                statCollection.Add(new Stat() { Name = "Hands", Value = playerGames.Count });

            if (true)//VPIP ++
            {
                statCollection.Add(new Stat() { Name = "VPIP", Value = Math.Round((double)vpipCount/playerGames.Count * 100, 2) });
            }
            if (true)//EP VPIP ++
            {
                statCollection.Add(new Stat() { Name = "EP VPIP", Value = Math.Round((double)vpipEpCount / playerGames.Count * 100, 2) });
            }
            if (true)//MP VPIP ++
            {
                statCollection.Add(new Stat() { Name = "MP VPIP", Value = Math.Round((double)vpipMpCount / playerGames.Count * 100, 2) });
            }
            if (true)//LP VPIP ++
            {
                statCollection.Add(new Stat() { Name = "LP VPIP", Value = Math.Round((double)vpipLpCount / playerGames.Count * 100, 2) });
            }
            if (true)//Profit ++
            {
                statCollection.Add(new Stat() { Name = "Profit", Value = Math.Round(totalProfit, 2) });
            }
            if (true)//PFR ++
            {
                statCollection.Add(new Stat() { Name = "PFR", Value = Math.Round((double)pfrCount / playerGames.Count * 100, 2) });
            }
            if (true)//EP PFR ++
            {
                statCollection.Add(new Stat() { Name = "EP PFR", Value = Math.Round((double)pfrEpCount / playerGames.Count * 100, 2) });
            }
            if (true)//MP PFR ++
            {
                statCollection.Add(new Stat() { Name = "MP PFR", Value = Math.Round((double)pfrMpCount / playerGames.Count * 100, 2) });
            }
            if (true)//LP PFR ++
            {
                statCollection.Add(new Stat() { Name = "LP PFR", Value = Math.Round((double)pfrLpCount / playerGames.Count * 100, 2) });
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

            if (true) //AF ++
            {
                statCollection.Add(new Stat() { Name = "AF", Value = Math.Round(afArray[0].GetFactor(), 2) });
            }
            if (true) //AF Flop ++
            {
                statCollection.Add(new Stat() { Name = "AF Flop", Value = Math.Round(afArray[1].GetFactor(), 2) });
            }
            if (true) //AF Turn ++
            {
                statCollection.Add(new Stat() { Name = "AF Turn", Value = Math.Round(afArray[2].GetFactor(), 2) });
            }
            if (true) //AF River ++
            {
                statCollection.Add(new Stat() { Name = "AF River", Value = Math.Round(afArray[3].GetFactor(), 2) });
            }
            if (true)//3B ++
            {
                statCollection.Add(new Stat() { Name = "3B", Value = Math.Round((double)threeBetCount / playerGames.Count * 100, 2) });
            }
            if (true)//Fold sb to Steal
            {
                var fSb = playerGames.Fold_SB_To_Steal_ForPlayer(playerName);
                statCollection.Add(new Stat() { Name = "Fold SB to Stl", Value = Math.Round(fSb, 2) });
            }
            if (true)//Fold Bb to Steal
            {
                var fBb = playerGames.Fold_BB_To_Steal_ForPlayer(playerName);
                statCollection.Add(new Stat() { Name = "Fold BB to Stl", Value = Math.Round(fBb, 2) });
            }
            if (true) // w$wsf ++
            {
                statCollection.Add(new Stat() { Name = "W$WSF", Value = Math.Round(sawFlopCount == 0 ? 0 : (double)winWherSawFlop / sawFlopCount * 100, 2) });
            }
            if (true) // wtsd ++
            {
                statCollection.Add(new Stat() { Name = "WTSD", Value = Math.Round((double)showdownCount / playerGames.Count * 100, 2) });
            }
            if (true)//Call Open
            {
                var co = playerGames.CallOpen_ForPlayer(playerName);
                statCollection.Add(new Stat() { Name = "Call Open", Value = Math.Round(co, 2) });
            }
            if (true) //flop cb
            { 
                var cb = playerGames.Flop_CB(playerName);
                statCollection.Add(new Stat() { Name = "Flop CB", Value = Math.Round(cb, 2) });
            }
            return statCollection;
        }
    }
}
