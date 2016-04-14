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
        public ulong GameNumber { get; set; }
        [Required]
        public string Source { get; set; }
        [Required]
        public HandActionType HandActionType { get; set; }

        public decimal Amount { get; set; }
        [Required]
        public Street Street { get; set; }

        //nav keys:
        public virtual Game Game { get;set; }

    }
}
