using HandHistories.SimpleObjects.Entities;

namespace HandHistories.Parser.MoneyMaker.BLL.ViewEntities
{
    public class PlayerSummary
    {
        public string Name { get; set; }
        public SeatType Limit { get; set; }
        public int Hands { get; set; }
        public int HandsWon { get; set; }
        public decimal HandsWonPercent { get; set; }
        public int Sessions { get; set; }
    }
}
