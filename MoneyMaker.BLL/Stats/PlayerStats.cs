using System.Collections.Generic;

namespace MoneyMaker.BLL.Stats
{
    /// <summary>
    /// Ф:Коллекция статов одного игрока
    /// </summary>
    public class PlayerStats : List<Stat>
    {
        public string Player { get; }
        public PlayerStats(string name)
        {
            Player = name;
        }
    }
}
