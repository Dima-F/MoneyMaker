﻿namespace HandHistories.Parser.MoneyMaker.BLL.ViewEntities
{
    /// <summary>
    /// View object for hud (PlayerSummary and PlayerStatistics in one)
    /// </summary>
    public class HudStatistics
    {
        public string Name { get; set; }
        public int Hands { get; set; }
        public decimal HandsWonPercent { get; set; }
        public decimal VPIP { get; set; }
        public decimal PFR { get; set; }
        public decimal ATS { get; set; }
        public decimal AF { get; set; }
    }
}
