using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using HandHistories.SimpleObjects.Entities;

namespace HandHistories.SimpleParser.Poker888
{
    public class Poker888CashParser:Poker888Parser
    {
        private static readonly Regex TableNameAndSeatTypeRegex = new Regex(@"(?<=Table)[\w|\s]+(?=\(.+\))");

        protected override bool IsTournament
        {
            get { return false; }
        }

        public override IDictionary<string, string> GetMainInfo(string path)
        {
            Dictionary<string, string> dictionary =new Dictionary<string,string>();
            var parts = path.Split(' ');
            dictionary["Room"] = parts[0].Substring(0, parts[0].Length - 8);
            dictionary["Date"] = DateTime.ParseExact(parts[0].Substring(parts[0].Length - 8), "yyyyMdd", null).ToShortDateString();
            dictionary["Table"] = parts[1];
            dictionary["Blinds"] = parts[2];
            dictionary["Limit"] = string.Join(" ", parts.Skip(3).ToArray());
            return dictionary;
        }

        protected override string FindTableName(IEnumerable<string> hand)
        {
            var line = hand.ToList()[3];
            var initialMatch = TableNameAndSeatTypeRegex.Match(line).Value;
            return initialMatch.Trim().Split(' ').First();
        }

        protected override SeatType FindSeatType(IEnumerable<string> hand)
        {
            var h  = hand as string[] ?? hand.ToArray();
            var line = h[3];
            var tableName = FindTableName(h);
            var initialMatch = TableNameAndSeatTypeRegex.Match(line).Value;
            string seatTypeString = initialMatch.Replace(tableName, "").Trim();
            return ConvertSeatEnum(seatTypeString);
        }
    }
}
