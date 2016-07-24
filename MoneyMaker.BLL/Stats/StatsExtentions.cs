using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MoneyMaker.BLL.Stats
{
    public static class StatsExtentions
    {
        /// <summary>
        /// Ф:Получает набор статов игроков и формирует таблицу DataTable, которую 
        /// удобно привязывать к DataGridView
        /// </summary>
        public static  DataTable ToDataTable(this List<PlayerStats> hudStatsCollection)
        {
            var table = new DataTable();
            if (hudStatsCollection.Count == 0)
                return table;
            table.Columns.Add("Player");
            foreach (var stat in hudStatsCollection.First())
            {
                table.Columns.Add(stat.Name);
            }
            foreach (var ps in hudStatsCollection)
            {
                var row = table.NewRow();
                row[0] = ps.Player;
                for (var j = 0; j < ps.Count; j++)
                {
                    row[j + 1] = ps[j].Value;
                }
                table.Rows.Add(row);
            }
            return table;
        }
    }
}
