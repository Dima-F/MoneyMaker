using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using HandHistories.SimpleObjects.Entities;

namespace HandHistories.SimpleParser.Poker888
{
    public class Poker888TournamentParser:Poker888Parser
    {
        private static readonly Regex TableNameRegex = new Regex(@"(?<=Tournament\s+#\d{7,}\s).+(?=\s\d+\s)");
        private static readonly Regex SeatTypeRegex = new Regex(@"(?<=Table\s+#\d{1,}\s).+(?=\s\()");
        protected override bool IsTournament
        {
            get { return true; }
        }
        
        public override IDictionary<string, string> GetMainInfo(string path)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            var parts = path.Split(' ');
            dictionary["Room"] = parts[0].Substring(0, parts[0].Length - 8);
            dictionary["Date"] = DateTime.ParseExact(parts[0].Substring(parts[0].Length - 8), "yyyyMdd", null).ToShortDateString();
            if (path.ToLower().Contains("sit & go"))
            {
                dictionary["Type"] = "Sit & Go";
                dictionary["Buy in"] = parts[4];
            }
            else
            {
                dictionary["Type"] = "Tournament";
                dictionary["Buy in"] = parts[2];
            }
            
            dictionary["Limit"] = string.Join(" ", parts.Skip(parts.Length-3).ToArray());
            return dictionary;
        }

        /// <summary>
        /// Ф:Обусловимся, что имя стола для турнира  - это бай ин + порядковый номер стола.
        /// </summary>
        protected override string FindTableName(IEnumerable<string> hand)
        {
            var line = hand.ToList()[3];
            return TableNameRegex.Match(line).Value;
        }

        protected override SeatType FindSeatType(IEnumerable<string> hand)
        {
            var line = hand.ToList()[3];
            var s =  SeatTypeRegex.Match(line).Value;
            return ConvertSeatEnum(s);
        }
    }
}
