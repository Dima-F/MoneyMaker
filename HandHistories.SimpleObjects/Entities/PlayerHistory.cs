using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HandHistories.SimpleObjects.Entities
{
    /// <summary>
    /// Ф:Сущность истории игрока в конкретной игре
    /// </summary>
    public class PlayerHistory
    {
        //PK:
        [Key]
        public int Id { get; set; }
        //FK:
        [Required]
        public ulong GameNumber { get; set; }
        [Required]
        public string PlayerName { get; set; }
        [Required]
        public double StartingStack { get; set; }
        [Required]
        public byte SeatNumber { get; set; }
        [Required]
        public PositionType Position { get; set; }
        public byte[] HoleCards { get; set; }
        //foreign keys
        public virtual Game Game { get; set; }
        public virtual List<HandAction> HandActions { get; set; }
    }
}
