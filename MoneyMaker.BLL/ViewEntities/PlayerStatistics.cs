using HandHistories.SimpleObjects.Entities;

namespace MoneyMaker.BLL.ViewEntities
{
    public class PlayerStatistics
    {
        public string Name { get; set; }
        public SeatType Limit { get; set; }
        public decimal VPIP { get; set; }
        public decimal PFR { get; set; }
        public decimal ATS { get; set; }
        public decimal AF { get; set; }//agression factor
    }
}
