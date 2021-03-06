﻿using System.ComponentModel.DataAnnotations;

namespace HandHistories.SimpleObjects.Entities
{
    public class HandAction
    {
        //PK:
        [Key]
        public int Id { get; set; }
        //FK:
        [Required]
        public int PlayerHistoryId { get; set; }
        [Required]
        public int Index { get; set; }//номер строки в конкретной истории, может использоваться для сортировки действий в порядке их следования.
        [Required]
        public string PlayerName { get; set; }//PlayerHistory.PlayerName or System name
        [Required]
        public HandActionType HandActionType { get; set; }

        public double Amount { get; set; }
        [Required]
        public Street Street { get; set; }
        //nav keys:
        public virtual PlayerHistory PlayerHistory { get;set; }

    }
}
