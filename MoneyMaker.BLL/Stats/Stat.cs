using System.Collections.Generic;

namespace MoneyMaker.BLL.Stats
{
    /// <summary>
    /// Ф:Стат
    /// </summary>
    public struct Stat
    {
        public string Name;
        public decimal Value;
        public bool Enabled;
    }

    /// <summary>
    /// Ф:Коллекция статов одного игрока
    /// </summary>
    public class PlayerStats : List<Stat>
    {
        public string Player { get; private set; }
        public PlayerStats(string name, int hands)
        {
            Player = name;
            Add(new Stat { Name = "Hands", Value = hands, Enabled = true });
        }
    }
}
