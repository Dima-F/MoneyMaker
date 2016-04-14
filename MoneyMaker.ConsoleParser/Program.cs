using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HandHistories.SimpleParser;

namespace MoneyMaker.ConsoleParser
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = @"E:\TexasHoldem\PokerStars\VipNeborak\HH20160408 Aaltje III - 10-20 - Play Money No Limit Hold'em.txt";
            var shortPath = Path.GetFileNameWithoutExtension(path);
            var text = ReadFile(path);
            var parser = ParserFactory.CreateParser(shortPath);
            var games = parser.ParseGames(text);
            Console.Write("Parsed {0} games.",games.Count);
            Console.Read();
        }
        private static string ReadFile(string path)
        {
            var file = new FileInfo(path);
            var builder = new StringBuilder();
            var fs = file.Open(FileMode.Open, FileAccess.ReadWrite);
            using (var reader = new StreamReader(fs, Encoding.UTF8))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    builder.AppendLine(line);
                }
            }
            fs.Close();
            return builder.ToString();
        }
    }
}
