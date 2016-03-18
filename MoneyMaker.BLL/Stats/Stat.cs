using System.Collections.Generic;
using System.Linq;
using HandHistories.SimpleObjects.Entities;
using MoneyMaker.BLL.Tools;

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
}
