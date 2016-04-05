using System;
using System.Linq;
using HandHistories.SimpleParser.Poker888;

namespace HandHistories.SimpleParser
{
    /// <summary>
    /// Ф:Фабрика парсеров. Реализована примитивно, нуждается в усовершенствовании.
    /// </summary>
    public static class ParserFactory
    {
        public static PokerParser CreateParser(string path)
        {
            var p = path.ToLower();
            if (p.Contains('\\') || p.Contains(".txt"))
                p = System.IO.Path.GetFileNameWithoutExtension(p);
            if (p.Contains("888poker"))
            {
                if(p.Contains("sit & go") || p.Contains("tournament"))
                    return new Poker888TournamentParser();
                return new Poker888CashParser();
            }
            //if (p.Contains("pokerstars"))
            //{
            //    return new PokerStarsCashParser();
            //}
            throw new Exception("Unable to parse hand history file name");
        }
    }
}
