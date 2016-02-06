using System;
using System.Linq;

namespace HandHistories.SimpleParser
{
    /// <summary>
    /// Ф:Фабрика парсеров. Реализована примитивно, нуждается в усовершенствовании.
    /// </summary>
    public static class ParserFactory
    {
        public static IHandHistoryParser CreateParser(string path)
        {
            if (path.Contains('\\') || path.Contains(".txt"))
                path = System.IO.Path.GetFileNameWithoutExtension(path);
            if (path.Contains("888poker"))
            {
                return new Poker888CashParser();
            }
            if (path.ToLower().Contains("pokerstars"))
            {
                return new PokerStarsCashParser();
            }
            throw new Exception("Unable to parse hand history file name");
        }
    }
}
