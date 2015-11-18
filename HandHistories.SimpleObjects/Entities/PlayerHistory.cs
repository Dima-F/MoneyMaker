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
        public int GameNumber { get; set; }
        [Required]
        public string PlayerName { get; set; }
        [Required]
        public decimal StartingStack { get; set; }

        [Required]
        public byte SeatNumber { get; set; }

        public byte[] HoleCards { get; set; }

        //foreign keys
        public virtual Game Game { get; set; }
    }
}
