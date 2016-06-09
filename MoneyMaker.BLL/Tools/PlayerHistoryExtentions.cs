using System.Linq;
using HandHistories.SimpleObjects.Entities;

namespace MoneyMaker.BLL.Tools
{
    public static class PlayerHistoryExtentions
    {
        /// <summary>
        /// Is Utg or Utg2 or Utg3
        /// </summary>
        public static bool IsEerlyPosition(this PlayerHistory playerHistory)
        {
            return playerHistory.Position == PositionType.UTG ||
                   playerHistory.Position == PositionType.UTG2 ||
                   playerHistory.Position == PositionType.UTG3;
        }
        /// <summary>
        /// Is CO or Button
        /// </summary>
        public static bool IsLatePosition(this PlayerHistory playerHistory)
        {
            return playerHistory.Position == PositionType.CO ||
                   playerHistory.Position == PositionType.B ;
        }
        /// <summary>
        /// Is MP or MP2 or MP3
        /// </summary>
        public static bool IsMiddlePosition(this PlayerHistory playerHistory)
        {
            return playerHistory.Position == PositionType.MP ||
                   playerHistory.Position == PositionType.MP2 ||
                   playerHistory.Position == PositionType.MP3;
        }

        /// <summary>
        /// Is CO, Button or SB 
        /// </summary>
        public static bool IsInStealPosition(this PlayerHistory playerHistory)
        {
            return playerHistory.Position == PositionType.CO ||
                   playerHistory.Position == PositionType.B ||
                   playerHistory.Position == PositionType.SB;
        }

        public static HandAction FirstRealAction(this PlayerHistory playerHistory)
        {
            return playerHistory.HandActions.RealActions().First();
        }
    }
}
