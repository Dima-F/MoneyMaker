using System.Collections.Generic;
using System.Linq;
using HandHistories.SimpleObjects.Entities;

namespace MoneyMaker.BLL.Tools
{
    public static class HandActionExtentions
    {
        public static IEnumerable<HandAction> NotBlinds(this IEnumerable<HandAction> actions)
        {
            return actions.Where( a => a.HandActionType != HandActionType.SMALL_BLIND 
            && a.HandActionType != HandActionType.BIG_BLIND
            && a.HandActionType!= HandActionType.DEAD_BLIND 
            && a.HandActionType!= HandActionType.All_IN_SB 
            && a.HandActionType != HandActionType.All_IN_BB);
        }

        public static IEnumerable<HandAction> Blinds(this IEnumerable<HandAction> actions)
        {
            return actions.Where(a => a.HandActionType == HandActionType.SMALL_BLIND 
                || a.HandActionType == HandActionType.BIG_BLIND
                || a.HandActionType == HandActionType.DEAD_BLIND);
        }

        public static IEnumerable<HandAction> RealActionsAndBlinds(this IEnumerable<HandAction> actions)
        {
            return actions.Where( a => a.HandActionType == HandActionType.SMALL_BLIND 
            || a.HandActionType == HandActionType.BIG_BLIND
            || a.HandActionType == HandActionType.BET
            || a.HandActionType == HandActionType.CALL
            || a.HandActionType == HandActionType.CHECK
            || a.HandActionType == HandActionType.FOLD
            || a.HandActionType == HandActionType.RAISE
            || a.HandActionType == HandActionType.ALL_IN_BET
            || a.HandActionType == HandActionType.ALL_IN_CALL
            || a.HandActionType == HandActionType.ALL_IN_RAISE
            || a.HandActionType == HandActionType.All_IN_BB
            || a.HandActionType == HandActionType.All_IN_SB);
        }

        public static IEnumerable<HandAction> PreflopHandActions(this IEnumerable<HandAction> actions)
        {
            return actions.Where(a => a.Street == Street.Preflop);
        }

        public static IEnumerable<HandAction> FlopHandActions(this IEnumerable<HandAction> actions)
        {
            return actions.Where(a => a.Street == Street.Flop);
        }

        public static IEnumerable<HandAction> TurnHandActions(this IEnumerable<HandAction> actions)
        {
            return actions.Where(a => a.Street == Street.Turn);
        }

        public static IEnumerable<HandAction> RiverHandActions(this IEnumerable<HandAction> actions)
        {
            return actions.Where(a => a.Street == Street.River);
        }

        public static IEnumerable<HandAction> RealActions(this IEnumerable<HandAction> actions)
        {
            return actions.Where(
           a=>a.HandActionType == HandActionType.BET
           || a.HandActionType == HandActionType.CALL
           || a.HandActionType == HandActionType.CHECK
           || a.HandActionType == HandActionType.FOLD
           || a.HandActionType == HandActionType.RAISE
           || a.HandActionType == HandActionType.ALL_IN_BET
           || a.HandActionType == HandActionType.ALL_IN_CALL
           || a.HandActionType == HandActionType.ALL_IN_RAISE);
        }

        public static IEnumerable<HandAction> RaiseActions(this IEnumerable<HandAction> actions)
        {
            return actions.Where(a => a.HandActionType == HandActionType.RAISE
                                      || a.HandActionType == HandActionType.ALL_IN_RAISE);
        }

        public static bool IsRaiseAction(this HandAction action)
        {
            return action.HandActionType == HandActionType.RAISE ||
                   action.HandActionType == HandActionType.ALL_IN_RAISE;
        }

        public static bool IsCallAction(this HandAction action)
        {
            return action.HandActionType == HandActionType.CALL ||
                   action.HandActionType == HandActionType.ALL_IN_CALL;
        }

        public static bool IsBetAction(this HandAction action)
        {
            return action.HandActionType == HandActionType.BET ||
                   action.HandActionType == HandActionType.ALL_IN_BET;
        }

        public static bool IsWinAction(this HandAction action)
        {
            return action.HandActionType == HandActionType.WINS ||
                   action.HandActionType == HandActionType.WINS_MAIN_POT ||
                   action.HandActionType == HandActionType.WINS_SIDE_POT;
        }

        /// <summary>
        /// Ф:Перед использованием произвести фильтрацию по улице!
        /// </summary>
        public static bool AllFoldedBeforeAction(this IEnumerable<HandAction> actions, HandAction actionBefore)
        {
            var beforeActions = actions.Where(a => a.Index < actionBefore.Index).NotBlinds();
            return beforeActions.RealActionsAndBlinds().All(a => a.HandActionType == HandActionType.FOLD 
                || a.HandActionType == HandActionType.CHECK);//check when dead blind
        }
    }
}
