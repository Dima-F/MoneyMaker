using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandHistories.SimpleObjects.Entities;

namespace MoneyMaker.BLL.Tools
{
    
    public static class FastGameExtentions
    {
        public static int WinForPlayer(this PlayerHistory ph)
        {
            return ph.HandActions.Any(ha => ha.IsWinAction()) ? 1 : 0;
        }

        public static int Vpip(this PlayerHistory ph)
        {
            return ph.HandActions.PreflopHandActions().NotBlinds().Any(ha =>
                ha.IsCallAction() || ha.IsRaiseAction()) ? 1 : 0;
        }
        public static int VpipEp(this PlayerHistory ph)
        {
            if (!ph.IsEerlyPosition())
                return 0;
            return ph.HandActions.PreflopHandActions().NotBlinds().Any(ha =>
                ha.IsCallAction() || ha.IsRaiseAction()) ? 1 : 0;
        }
        public static int VpipMp(this PlayerHistory ph)
        {
            if (!ph.IsMiddlePosition())
                return 0;
            return ph.HandActions.PreflopHandActions().NotBlinds().Any(ha =>
                ha.IsCallAction() || ha.IsRaiseAction()) ? 1 : 0;
        }
        public static int VpipLp(this PlayerHistory ph)
        {
            if (!ph.IsLatePosition())
                return 0;
            return ph.HandActions.PreflopHandActions().NotBlinds().Any(ha =>
                ha.IsCallAction() || ha.IsRaiseAction()) ? 1 : 0;
        }

        public static int Pfr(this PlayerHistory ph)
        {
            return ph.HandActions.PreflopHandActions().RealActions().Any(ha => ha.IsRaiseAction()) ? 1 : 0;
        }
        public static int PfrEp(this PlayerHistory ph)
        {
            if (!ph.IsEerlyPosition())
                return 0;
            return ph.HandActions.PreflopHandActions().RealActions().Any(ha => ha.IsRaiseAction()) ? 1 : 0;
        }
        public static int PfrMp(this PlayerHistory ph)
        {
            if (!ph.IsMiddlePosition())
                return 0;
            return ph.HandActions.PreflopHandActions().RealActions().Any(ha => ha.IsRaiseAction()) ? 1 : 0;
        }
        public static int PfrLp(this PlayerHistory ph)
        {
            if (!ph.IsLatePosition())
                return 0;
            return ph.HandActions.PreflopHandActions().RealActions().Any(ha => ha.IsRaiseAction()) ? 1 : 0;
        }

        public static void AF_(this PlayerHistory ph, Af afObject)
        {
            foreach (var action in ph.HandActions)
            {
                switch (action.HandActionType)
                {
                    case HandActionType.CALL:
                    case HandActionType.ALL_IN_CALL:
                        afObject.CallCount++;
                        break;
                    case HandActionType.ALL_IN_RAISE:
                    case HandActionType.BET:
                    case HandActionType.ALL_IN_BET:
                    case HandActionType.RAISE:
                        afObject.BetRaiseCount++;
                        break;
                }
            }
        }
        public static void AF_Flop(this PlayerHistory ph, Af afObject)
        {
            foreach (var action in ph.HandActions)
            {
                if (action.Street != Street.Flop)
                    continue;
                switch (action.HandActionType)
                {
                    case HandActionType.CALL:
                    case HandActionType.ALL_IN_CALL:
                        afObject.CallCount++;
                        break;
                    case HandActionType.ALL_IN_RAISE:
                    case HandActionType.BET:
                    case HandActionType.ALL_IN_BET:
                    case HandActionType.RAISE:
                        afObject.BetRaiseCount++;
                        break;
                }
            }
        }
        public static void AF_Turn(this PlayerHistory ph, Af afObject)
        {
            foreach (var action in ph.HandActions)
            {
                if (action.Street != Street.Turn)
                    continue;
                switch (action.HandActionType)
                {
                    case HandActionType.CALL:
                    case HandActionType.ALL_IN_CALL:
                        afObject.CallCount++;
                        break;
                    case HandActionType.ALL_IN_RAISE:
                    case HandActionType.BET:
                    case HandActionType.ALL_IN_BET:
                    case HandActionType.RAISE:
                        afObject.BetRaiseCount++;
                        break;
                }
            }
        }
        public static void AF_River(this PlayerHistory ph, Af afObject)
        {
            foreach (var action in ph.HandActions)
            {
                if(action.Street!=Street.River)
                    continue;
                switch (action.HandActionType)
                {
                    case HandActionType.CALL:
                    case HandActionType.ALL_IN_CALL:
                        afObject.CallCount++;
                        break;
                    case HandActionType.ALL_IN_RAISE:
                    case HandActionType.BET:
                    case HandActionType.ALL_IN_BET:
                    case HandActionType.RAISE:
                        afObject.BetRaiseCount++;
                        break;
                }
            }
        }

        public static int ThreeBet(this PlayerHistory ph, Game g)
        {
            if (!ph.HandActions.PreflopHandActions().RaiseActions().Any()) return 0;
            var rrIndex = ph.HandActions.PreflopHandActions().RaiseActions().First().Index;
            return g.PlayerHistories.Any(
                eph => eph.PlayerName != ph.PlayerName && eph.HandActions.PreflopHandActions().Any(a =>
                    a.IsRaiseAction() && a.Index < rrIndex)) ? 1 : 0;
        }

        public static double HandProfit(this PlayerHistory ph)
        {
            return ph.HandActions.Sum(ha => ha.Amount);
        }

        public static int ReachShowdown(this PlayerHistory ph)
        {
            return ph.HandActions.Any(ha => ha.HandActionType == HandActionType.SHOW) ? 1 : 0;
        }
    }

    public class Af
    {
        public string Name { get; set; }
        public int BetRaiseCount { get; set; }
        public int CallCount { get; set; }

        public double GetFactor()
        {
            return CallCount == 0 ? BetRaiseCount : (double)BetRaiseCount / CallCount ;
        }
    }
}
