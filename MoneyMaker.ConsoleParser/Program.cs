using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using HandHistories.SimpleObjects.Entities;
using HandHistories.SimpleParser;

namespace MoneyMaker.ConsoleParser
{
    class Program
    {
        static void Main(string[] args)
        {
            var directory = @"E:\TexasHoldem\888Poker\HandsHistory\VipNeborak";
            var files = Directory.GetFiles(directory, "*.txt").Where(s => !s.Contains("Summary")).ToArray();
            var allGames = new List<Game>();
            foreach (var file in files)
            {
                allGames.AddRange(ParseFile(file));
            }
            Console.WriteLine("\nParsed files:");
            foreach (var file in files)
            {
                Console.WriteLine("\t*{0}",Path.GetFileNameWithoutExtension(file));
            }
            Console.Write("\nParsed {0} games.",allGames.Count);
            Console.Read();
        }

        private static List<Game> ParseFile(string file)
        {
            var shortPath = Path.GetFileNameWithoutExtension(file);
            var text = ReadFile(file);
            var parser = ParserFactory.CreateParser(shortPath);
            var games = parser.ParseGames(text);
            Console.WriteLine("Parsing for {0} is completed...",shortPath);
            return games;
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
