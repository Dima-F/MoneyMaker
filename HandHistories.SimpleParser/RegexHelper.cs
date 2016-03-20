using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using HandHistories.SimpleObjects.Entities;
using HandHistories.SimpleObjects.Tools;

namespace HandHistories.SimpleParser
{
    /// <summary>
    /// Ф:Класс методов разширения для поиска значений в документе истории рук с помощью регулярных выражений
    /// </summary>
    public static  class RegexHelper
    {
        //шаблон рег. выраж.
        private static readonly Regex GameNumberRegex=new Regex(@"\d{9}", RegexOptions.Compiled);
        private static readonly Regex DateTimeRegex = new Regex(@"\d{2}\s\d{2}\s\d{4}\s\d{2}:\d{2}:\d{2}", RegexOptions.Compiled);
        private static readonly Regex DateRegex = new Regex(@"(\d+) (\d+) (\d+) ", RegexOptions.Compiled);
        private static readonly Regex TableNameAndSeatTypeRegex = new Regex(@"(?<=Table)[\w|\s]+(?=\(Real Money\))");
        private static readonly Regex GameTypeRegex = new Regex(@"(?<=Blinds)[\w|\s]+(?=-\s\*{3})");
        private static readonly Regex ButtonPositionRegex = new Regex(@"(?<=Seat\s)\d+(?=\sis the button)");
        private static readonly Regex NumberOfPlayersRegex = new Regex(@"(?<=Total number of players : )\d+");
        private static readonly Regex PlayerNameRegex = new Regex(@"(?<=Seat\s\d+:).+(?=\()");
        private static readonly Regex PlayerSeatRegex = new Regex(@"(?<=Seat\s)\d+(?=:)");
        private static readonly Regex PlayerStackRegex = new Regex(@"(?<=\s\(\s).+(?=\$\s\))|(?<=\s\(\s\$).+(?=\s\))");
        private static readonly Regex ActionAmountRegex = new Regex(@"(?<=\[\s*).+(?=\$\s*\])|(?<=\[\s*\$).+(?=\s*\])");
        private static readonly Regex FlopCardsRegex = new Regex(@"(?<=flop\s*\*\*\s*\[\s*).+(?=\s*\])");
        private static readonly Regex TurnCardRegex = new Regex(@"(?<=turn\s*\*\*\s*\[\s*).+(?=\s*\])");
        private static readonly Regex RiverCardRegex = new Regex(@"(?<=river\s*\*\*\s*\[\s*).+(?=\s*\])");
        private static readonly Regex ShowdounCardsRegex = new Regex(@"(?<=shows\s*\[\s*).+(?=\s*\])");
        private static readonly Regex MuckCardsRegex = new Regex(@"(?<=mucks\s*\[\s*).+(?=\s*\])");
        private static readonly Regex HeroCardsRegex = new Regex(@"(?<=Dealt\sto\s\b.*\b\s*\[\s*).+(?=\s*\])");
        private static readonly Regex ErrorRegex = new Regex(@"Seat\s\d{1,2}:\s+\(\s*\$.{1,5}\s\)|Seat\s\d{1,2}:\s+\(\s*.{1,5}\$\s\)");


        public static int FindGameNumber(this string inputString)
        {
            var matchGameNumber = GameNumberRegex.Match(inputString).Value.Trim(' ');
            return Int32.Parse(matchGameNumber);
        }

        public static DateTime FindDateOfGame(this string inputString)
        {
            var matchDateOfHand = DateTimeRegex.Match(inputString).Value.Trim(' ');
            //Change string so it becomes 2012-02-04 23:59:48
            var correctDateOfHand = DateRegex.Replace(matchDateOfHand, "$3-$2-$1 ");
            return DateTime.Parse(correctDateOfHand);
        }

        public static string FindTableName(this string inputString)
        {
            var initialMatch = TableNameAndSeatTypeRegex.Match(inputString).Value;
            return initialMatch.Trim().Split(' ').First();
        }

        public static GameType FindGameType(this string inputString)
        {
            var initialMatch = GameTypeRegex.Match(inputString).Value;
            string gameTypeString= initialMatch.Trim();
            return ConvertGameTypeEnum(gameTypeString);
        }

        public static SeatType FindSeatType(this string inputString)
        {
            var tableName = FindTableName(inputString);
            var initialMatch = TableNameAndSeatTypeRegex.Match(inputString).Value;
            string seatTypeString = initialMatch.Replace(tableName, "").Trim();
            return ConvertSeatEnum(seatTypeString);
        }

        public static byte FindButtonPosition(this string inputString)
        {
            return Byte.Parse(ButtonPositionRegex.Match(inputString).Value);
        }

        public static byte FindNumberOfPlayers(this string inputString)
        {
            return Byte.Parse(NumberOfPlayersRegex.Match(inputString).Value.Trim());
        }

        public static string FindPlayerName(this string inputString)
        {
            return PlayerNameRegex.Match(inputString).Value.Trim();
        }

        public static byte FindSeatNumber(this string inputString)
        {
            return byte.Parse(PlayerSeatRegex.Match(inputString).Value.Trim(), CultureInfo.InvariantCulture);
        }

        public static  decimal  FindPlayerStartingStack(this string inputString)
        {
            var s = PlayerStackRegex.Match(inputString).Value.Trim();
            return decimal.Parse(s.Replace(",", "."), CultureInfo.InvariantCulture);
        }

        public static decimal FindActionAmount(this string inputString)
        {
            var numberOfDollarSign = SimbolCount(inputString, '$');
            if (numberOfDollarSign <= 1)
                return decimal.Parse(ActionAmountRegex.Match(inputString).Value.Replace(',', '.').Trim(), CultureInfo.InvariantCulture);
            var parts = ActionAmountRegex.Match(inputString).Value.Split('+');
            return parts.Sum(part => decimal.Parse(part.Replace(',', '.').Trim().Trim('$').Trim(), CultureInfo.InvariantCulture));
        }

        public static byte[] FindFlopCards(this string inputString)
        {
            string[] strCards = FlopCardsRegex.Match(inputString).Value.Replace(" ", "").Split(',');
            var byteArray=new byte[3];
            for (int i = 0; i < strCards.Length; i++)
            {
                byteArray[i] = strCards[i].ConvertStringCardToByte();
            }
            return byteArray;
        }

        public static byte[] FindHeroCards(this string inputString)
        {
            string[] strCards = HeroCardsRegex.Match(inputString).Value.Replace(" ", "").Split(',');
            var byteArray = new byte[2];
            for (var i = 0; i < strCards.Length; i++)
            {
                byteArray[i] = strCards[i].ConvertStringCardToByte();
            }
            return byteArray;
        }

        public static byte FindTurnCard(this string inputString)
        {
            string strCard = TurnCardRegex.Match(inputString).Value.Trim();
            return strCard.ConvertStringCardToByte();
        }

        public static byte FindRiverCard(this string inputString)
        {
            string strCard = RiverCardRegex.Match(inputString).Value.Trim();
            return strCard.ConvertStringCardToByte();
        }

        public static byte[] FindShowdounCards(this string inputString)
        {
            string[] strCards = ShowdounCardsRegex.Match(inputString).Value.Trim().Replace(" ", "").Split(',');
            var byteArray = new byte[2];
            for (int i = 0; i < strCards.Length; i++)
            {
                byteArray[i] = strCards[i].ConvertStringCardToByte();
            }
            return byteArray;
        }

        public static byte[] FindMuckCards(this string inputString)
        {
            string[] strCards = MuckCardsRegex.Match(inputString).Value.Replace(" ", "").Split(',');
            var byteArray = new byte[2];
            for (int i = 0; i < strCards.Length; i++)
            {
                byteArray[i] = strCards[i].ConvertStringCardToByte();
            }
            return byteArray;
        }

        public static bool FindBadHand(this string inputString)
        {
            return ErrorRegex.IsMatch(inputString);
        }







        private static SeatType ConvertSeatEnum(string seatTypeString)
        {
            switch (seatTypeString.ToLower())
            {
                case "9 max":
                    return SeatType._FullRing_9Handed;
                default:
                    return SeatType.Unknown;
            }
        }

        private static GameType ConvertGameTypeEnum(string gameTypeString)
        {
            switch (gameTypeString.ToLower())
            {
                case "no limit holdem":
                    return GameType.NoLimitHoldem;
                default:
                    return GameType.Any;
            }
        }

        private static  int SimbolCount(string inputString, char simbol)
        {
            var chars = inputString.ToCharArray();
            return chars.Count(c => c == simbol);
        }
    }
}
