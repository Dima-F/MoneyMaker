using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using HandHistories.SimpleObjects.Entities;

namespace HandHistories.SimpleParser.PokerStars
{
    public class PokerStarsCashParser:PokerStarsParser
    {

        private static readonly Regex LimitTypeRegex = new Regex(@"(?<=:).+(?=\()", RegexOptions.Compiled);
        private static readonly Regex MoneyTypeRegex = new Regex(@"(?<=\().+(?=\))", RegexOptions.Compiled);
        private static readonly Regex SeatTypeRegex = new Regex(@"(?<='\s+).+(?=\s+\()", RegexOptions.Compiled);
        protected override bool IsTournament => false;
        public override IDictionary<string, string> GetInfoFromPath(string path)
        {
            var dictionary = new Dictionary<string, string>();
            var parts = path.Split(' ');
            dictionary["Date"] = DateTime.ParseExact(parts[0].Substring(parts[0].Length - 8), "yyyyMdd", null).ToShortDateString();
            dictionary["Table"] = Regex.Match(path, @"(?<=\d{8}\s).+(?=\s-\s[0-9|$|,|/.]+-[0-9|$|,|/.]+)").Value;
            dictionary["Blinds"] = Regex.Match(path, @"(?<=\s-\s).+(?=\s-\s)").Value;
            dictionary["Limit"] = Regex.Match(path, @"(?<= Money\s).+(?=)").Value;
            return dictionary;
        }
        protected override LimitType FindLimitType(IEnumerable<string> hand)
        {
            var line = hand.ToList()[0];
            var initialMatch = LimitTypeRegex.Match(line).Value.Trim();
            return ConvertLimitTypeEnum(initialMatch);
        }
        protected override MoneyType FindMoneyType(IEnumerable<string> hand)
        {
            var line = hand.ToList()[1];
            var initialMatch = MoneyTypeRegex.Match(line).Value;
            return ConvertMoneyTypeEnum(initialMatch);
        }
        protected override SeatType FindSeatType(IEnumerable<string> hand)
        {
            var line = hand.ToList()[1];
            string seatTypeString = SeatTypeRegex.Match(line).Value.Replace('-', ' ');
            return ConvertSeatEnum(seatTypeString);
        }
    }
}
