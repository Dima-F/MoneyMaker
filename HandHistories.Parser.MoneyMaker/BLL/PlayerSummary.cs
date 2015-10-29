using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using HandHistories.SimpleObjects.Entities;

namespace HandHistories.Parser.MoneyMaker.BLL
{
    public class PlayerSummary
    {
        public string Name { get; set; }
        public SeatType Limit { get; set; }
        public int Hands { get; set; }
        [Display(Name = "Hands won")]
        public int HandsWon { get; set; }
        [Description("Hands won (%)")]
        public decimal HandsWonPercent { get; set; }
    }
}
