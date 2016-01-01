using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HandHistories.SimpleObjects.Entities
{
    /// <summary>
    /// HandHistory
    /// Ф:Сущьность одной раздачи (игры, истории руки и т.д.)
    /// Уже заранее подразумевается,что покер рум - 888, а тип игры - кеш на реальные деньги. Позже нужно разширить модель и 
    /// внедрить в нее возможность вариации.
    /// </summary>
    public class Game
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GameNumber { get; set; }
        [Required]
        public DateTime DateOfHand { get; set; }
        [Required]
        public string TableName { get; set; }
        [Required]
        public SeatType SeatType { get; set; }
        [Required]
        public  GameType GameType { get; set; }
        [Required]
        public byte ButtonPosition { get; set; }
        [Required]
        public byte NumberOfPlayers { get; set; }
        [Required]
        public string HandText { get; set; }
        
        public string Hero { get; set; }

        public byte [] BoardCards { get; set; }

        public virtual List<PlayerHistory> PlayerHistories { get; set; } 

        public virtual List<HandAction> HandActions { get; set; }
 
    }
}
