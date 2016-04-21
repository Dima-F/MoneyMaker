using System;
using System.Linq;
using HandHistories.SimpleParser.Poker888;
using HandHistories.SimpleParser.PokerStars;

namespace HandHistories.SimpleParser
{
    /// <summary>
    /// Ф:Фабрика парсеров. На основании имени файла принимает решение, какой парсер создать.
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
            else
            {
                if(p.Contains('+'))
                    return new PokerStarsTournamentParser();
                else return new PokerStarsCashParser();
            }
        }
    }
}
