using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using HandHistories.SimpleObjects.Entities;

namespace HandHistories.SimpleParser.PokerStars
{
    public class PokerStarsTournamentParser:PokerStarsParser
    {

        private static readonly Regex SeatTypeRegex = new Regex(@"(?<='\s+).+(?=\sSeat\s#)", RegexOptions.Compiled);
        private static readonly Regex LimitTypeRegex = new Regex(@"(?<=.+\+.+\s).+(?=-\sLevel)", RegexOptions.Compiled);
        private static readonly Regex MoneyTypeRegex = new Regex(@"(?<=,\s).+(?=\sHold'em)", RegexOptions.Compiled);
        protected override bool IsTournament => true;
        public override IDictionary<string, string> GetInfoFromPath(string path)
        {
            var dictionary = new Dictionary<string, string>();
            var parts = path.Split(' ');
            dictionary["Date"] = DateTime.ParseExact(parts[0].Substring(parts[0].Length - 8), "yyyyMdd", null).ToShortDateString();
            dictionary["Table number"] = parts[1];
            dictionary["Limit"] = Regex.Match(path, @"(?<=\D\d{9,11}\s)\D+(?=\s\d)").Value;
            dictionary["Buy in"] = Regex.Match(path, @"(?<=Hold'em ).+(?=)").Value;
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
            var line = hand.ToList()[0];
            var initialMatch = MoneyTypeRegex.Match(line).Value.Trim();
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
