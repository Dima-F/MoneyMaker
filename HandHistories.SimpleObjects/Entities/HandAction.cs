using System.ComponentModel.DataAnnotations;

namespace HandHistories.SimpleObjects.Entities
{
    public class HandAction
    {
        //PK:
        [Key]
        public int Id { get; set; }
        //FK:
        [Required]
        public int GameNumber { get; set; }

        public string PlayerName { get; set; }
        [Required]
        public HandActionType HandActionType { get; set; }

        public decimal Amount { get; set; }
        [Required]
        public Street Street { get; set; }

        //nav keys:
        public virtual Game Game { get;set; }

    }
}
