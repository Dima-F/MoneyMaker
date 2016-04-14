using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using HandHistories.SimpleObjects.Entities;
using HandHistories.SimpleObjects.Tools;

namespace HandHistories.SimpleParser.PokerStars
{
    public  class PokerStarsParser:PokerParser
    {
        private static readonly Regex GameNumberRegex = new Regex(@"(?<=\#)\d{6,}(?=\:)", RegexOptions.Compiled);
        private static readonly Regex DateTimeRegex = new Regex(@"(?<=-\s*)\d{4}\/\d{2}\/\d{2}\s+\d{2}:\d{2}:\d{2}(?=\s+EET)", RegexOptions.Compiled);
        private static readonly Regex DateRegex = new Regex(@"(\d+)/(\d+)/(\d+)", RegexOptions.Compiled);
        private static readonly Regex TableNameRegex = new Regex(@"(?<=').+(?=')", RegexOptions.Compiled);
        private static readonly Regex BlindsRegex = new Regex(@"(?<=\().+(?=\))", RegexOptions.Compiled);
        private static readonly Regex LimitTypeRegex = new Regex(@"(?<=:).+(?=\()",RegexOptions.Compiled);
        private static readonly Regex MoneyTypeRegex = new Regex(@"(?<=\().+(?=\))",RegexOptions.Compiled);
        private static readonly Regex SeatTypeRegex = new Regex(@"(?<='\s+).+(?=\s+\()", RegexOptions.Compiled);
        private static readonly Regex ButtonPositionRegex = new Regex(@"(?<=#)\d{1,2}(?=\s*is)", RegexOptions.Compiled);
        private static readonly Regex NumberOfPlayersRegex = new Regex(@"Seat\s\d{1,2}:", RegexOptions.Compiled);
        private static readonly Regex PlayerNameRegex = new Regex(@"(?<=Seat\s\d{1,2}:\s*).+(?=\()");
        private static readonly Regex PlayerSeatRegex = new Regex(@"(?<=Seat\s)\d+(?=:)");
        private static readonly Regex PlayerStackRegex = new Regex(@"(?<=Seat.+\().+(?=\sin)");

        private static readonly Regex AllInCallAmountRegex = new Regex(@"(?<=:\s+calls\s+).+(?=\s+and\sis\sall-in)");
        private static readonly Regex AllInRaiseAmountRegex = new Regex(@"(?<=:\s+raises\s+).+(?=\sto\s.+\s+and\sis\sall-in)");
        private static readonly Regex FlopCardsRegex = new Regex(@"(?<=FLOP\s*\*\*\*\s*\[\s*).+(?=\s*\])");
        private static readonly Regex TurnCardRegex = new Regex(@"(?<=TURN.+]\s*\[).+(?=\s*\])");
        private static readonly Regex RiverCardRegex = new Regex(@"(?<=RIVER.+]\s*\[).+(?=\s*\])");
        private static readonly Regex ShowdounCardsRegex = new Regex(@"(?<=shows\s*\[\s*).+(?=\s*\])");
        private static readonly Regex MuckCardsRegex = new Regex(@"(?<=mucked\s*\[\s*).+(?=\s*\])");
        private static readonly Regex HeroCardsRegex = new Regex(@"(?<=Dealt\sto\s\b.*\b\s*\[\s*).+(?=\s*\])");
        private static readonly Regex HeroNameRegex = new Regex(@"(?<=Dealt\s+to\s+).+(?=\s+\[)");
        private static readonly Regex CollectedPotRegex = new Regex(@"(?<=collected\s).+(?=\sfrom pot)");
        private static readonly Regex CollectedMainPotRegex = new Regex(@"(?<=collected\s).+(?=\sfrom main pot)");
        private static readonly Regex CollectedSidePotRegex = new Regex(@"(?<=collected\s).+(?=\sfrom side pot)");
        private static readonly Regex UncalledBetAmountRegex = new Regex(@"(?<=Uncalled\sbet\s+\(\s*).+(?=\s*\)\sreturned\sto)");
        private static readonly Regex UncalledBetPlayerRegex = new Regex(@"(?<=returned\sto\s).+(?=)");
        private static readonly Regex ErrorRegex = new Regex(@"");
        
        protected override byte StartPlayerRow => 2;
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

        public override IEnumerable<HandAction> ParseHandActions(Game game, IReadOnlyList<string> multipleLines)
        {
            var handActions = new List<HandAction>();
            var currentStreet = Street.Preflop;
            for (var i = StartPlayerRow + game.NumberOfPlayers; i < multipleLines.Count; i++)
            {
                var ha = new HandAction { GameNumber = game.GameNumber };
                if (multipleLines[i].StartsWith("***"))
                {
                    if (multipleLines[i].ToLower().Contains("flop"))
                    {
                        ha.Source = NameOfSystem;
                        ha.HandActionType = HandActionType.DEALING_FLOP;
                        game.BoardCards.InitializeNewCards(FindFlopCards(multipleLines[i]));
                        handActions.Add(ha);
                        currentStreet = Street.Flop;
                        continue;
                    }
                    if (multipleLines[i].ToLower().Contains("turn"))
                    {
                        ha.Source = NameOfSystem;
                        ha.HandActionType = HandActionType.DEALING_TURN;
                        game.BoardCards[3] = FindTurnCard(multipleLines[i]);
                        handActions.Add(ha);
                        currentStreet = Street.Turn;
                        continue;
                    }
                    if (multipleLines[i].ToLower().Contains("river"))
                    {
                        ha.Source = NameOfSystem;
                        ha.HandActionType = HandActionType.DEALING_RIVER;
                        game.BoardCards[4] = FindRiverCard(multipleLines[i]);
                        handActions.Add(ha);
                        currentStreet = Street.River;
                        continue;
                    }
                    if (multipleLines[i].ToLower().Contains("hole"))
                    {
                        continue;
                    }
                    if (multipleLines[i].ToLower().Contains("show down"))
                    {
                        ha.Source = NameOfSystem;
                        ha.HandActionType = HandActionType.SUMMARY;
                        handActions.Add(ha);
                        currentStreet = Street.Showdown;
                        continue;
                    }
                    if (multipleLines[i].ToLower().Contains("summary"))
                    {
                        break;//пока что summary строки не будем обрабатывать.
                    }
                    throw new NotImplementedException("No defined action with ***");
                } //end ***
                if (multipleLines[i].ToLower().StartsWith("dealt to"))
                {
                    ha.Source = HeroNameRegex.Match(multipleLines[i]).Value.Trim();
                    ha.Street = currentStreet;
                    ha.HandActionType = HandActionType.DEALT_HERO_CARDS;
                    game.Hero = ha.Source;
                    game.PlayerHistories.Find(p => p.PlayerName == ha.Source).HoleCards.InitializeNewCards(FindHeroCards(multipleLines[i]));
                    handActions.Add(ha);
                    continue;
                }
                
                
                //hand actions of type "player:action"
                if (multipleLines[i].Contains(':'))
                {
                    var parts = multipleLines[i].Split(':');
                    ha.Source = parts[0].Trim();
                    ha.Street = currentStreet;
                    var action = parts[1].Trim().Split(' ')[0];
                    switch (action)
                    {
                        case "posts":
                            if (parts[1].Trim().Split(' ')[1] == "big")
                            {
                                ha.HandActionType = HandActionType.BIG_BLIND;
                                ha.Amount = DefineActionAmount(ha, multipleLines[i]);
                                handActions.Add(ha);
                            }
                            else
                            {
                                ha.HandActionType = HandActionType.SMALL_BLIND;
                                ha.Amount = DefineActionAmount(ha, multipleLines[i]);
                                handActions.Add(ha);
                            }
                            continue;
                        case "folds":
                            ha.HandActionType = HandActionType.FOLD;
                            handActions.Add(ha);
                            continue;
                        case "checks":
                            ha.HandActionType = HandActionType.CHECK;
                            handActions.Add(ha);
                            continue;
                        case "calls":
                            ha.HandActionType = multipleLines[i].Contains("all-in") ? HandActionType.ALL_IN_CALL : HandActionType.CALL;
                            ha.Amount = DefineActionAmount(ha, multipleLines[i]);
                            handActions.Add(ha);
                            continue;
                        case "bets":
                            ha.HandActionType = HandActionType.BET;
                            ha.Amount = DefineActionAmount(ha, multipleLines[i]);
                            handActions.Add(ha);
                            continue;
                        case "raises":
                            ha.HandActionType = multipleLines[i].Contains("all-in") ? HandActionType.ALL_IN_RAISE : HandActionType.RAISE;
                            ha.Amount = DefineActionAmount(ha, multipleLines[i]);
                            handActions.Add(ha);
                            continue;
                        case "doesn't":
                            ha.HandActionType = HandActionType.DIDNT_SHOW_HAND;
                            handActions.Add(ha);
                            continue;
                        case "shows":
                            ha.HandActionType = HandActionType.SHOW;
                            game.PlayerHistories.Find(p => p.PlayerName == ha.Source).HoleCards.InitializeNewCards(FindShowdounCards(multipleLines[i]));
                            handActions.Add(ha);
                            continue;
                        case "mucks":
                            ha.HandActionType = HandActionType.MUCKS;
                            game.PlayerHistories.Find(p => p.PlayerName == ha.Source).HoleCards.InitializeNewCards(FindMuckCards(multipleLines));
                            handActions.Add(ha);
                            continue;
                        default:
                            throw new NotImplementedException("Cann't parse string with ':' and unnown word");
                    }
                }
                if (multipleLines[i].ToLower().Contains("uncalled bet"))
                {
                    ha.HandActionType = HandActionType.UNCALLED_BET;
                    ha.Source = UncalledBetPlayerRegex.Match(multipleLines[i]).Value;
                    ha.Amount = DefineActionAmount(ha, multipleLines[i]);
                    handActions.Add(ha);
                    continue;
                }
                if (multipleLines[i].ToLower().Contains("collected"))
                {
                    if (multipleLines[i].ToLower().Contains("main"))
                    {
                        ha.HandActionType = HandActionType.WINS_MAIN_POT;
                    }
                    else if (multipleLines[i].ToLower().Contains("side"))
                    {
                        ha.HandActionType = HandActionType.WINS_SIDE_POT;
                    }
                    else
                    {
                        ha.HandActionType = HandActionType.WINS;
                    }
                    
                    ha.Amount = DefineActionAmount(ha, multipleLines[i]);
                    handActions.Add(ha);
                    continue;
                }
                //hand actions without tracking:
                if (multipleLines[i].ToLower().Contains("leaves the table"))
                {
                    continue;
                }
                if (multipleLines[i].ToLower().Contains("joins the table"))
                {
                    continue;
                }
                if (multipleLines[i].ToLower().Contains("will be allowed to play"))
                {
                    continue;
                }
                if (multipleLines[i].ToLower().Contains("is disconnected"))
                {
                    continue;
                }
                if (multipleLines[i].ToLower().Contains("was removed from the table"))
                {
                    continue;
                }
                throw new NotImplementedException("not parsed string");
            }
            return handActions;
        }

        

        protected override ulong FindGameNumber(IEnumerable<string> hand)
        {
            var line = hand.ToList()[0];
            var matchGameNumber = GameNumberRegex.Match(line).Value.Trim(' ');
            return UInt64.Parse(matchGameNumber);
        }

        protected override DateTime FindDateOfGame(IEnumerable<string> hand)
        {
            var line = hand.ToList()[0];
            var matchDateOfHand = DateTimeRegex.Match(line).Value.Trim(' ');
            //Change string so it becomes yyyy-mm-dd hh:mm:ss
            var correctDateOfHand = DateRegex.Replace(matchDateOfHand, "$1-$2-$3");
            return DateTime.Parse(correctDateOfHand);
        }

        protected override string FindTableName(IEnumerable<string> hand)
        {
            var line = hand.ToList()[1];
            return TableNameRegex.Match(line).Value;
        }

        protected override decimal FindSmallBlind(IEnumerable<string> hand)
        {
            var line = hand.ToList()[0];
            var s = BlindsRegex.Match(line).Value.Split('/')[0].Replace("$", "").Trim();
            s = s.Replace(((char)160).ToString(), "");//удалить все неразрывные пробелы nbsp (разделяет разряды)
            var numberFormatInfo = new NumberFormatInfo { NumberDecimalSeparator = "," };
            var d = Decimal.Parse(s, numberFormatInfo);
            return d;
        }

        protected override decimal FindBigBlind(IEnumerable<string> hand)
        {
            var line = hand.ToList()[0];
            var s = BlindsRegex.Match(line).Value.Split('/')[1].Replace("$", "").Trim();
            s = s.Replace(((char)160).ToString(), "");//удалить все неразрывные пробелы nbsp (разделяет разряды)
            var numberFormatInfo = new NumberFormatInfo { NumberDecimalSeparator = "," };
            var d = Decimal.Parse(s, numberFormatInfo);
            return d;
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

        protected override byte FindNumberOfPlayers(string hand)
        {
            return (byte)(NumberOfPlayersRegex.Matches(hand).Count/2);
        }

        protected override byte FindButtonPosition(IEnumerable<string> hand)
        {
            var line = hand.ToList()[1];
            return Byte.Parse(ButtonPositionRegex.Match(line).Value);
        }

        protected override string FindPlayerName(string line)
        {
            return PlayerNameRegex.Match(line).Value.Trim();
        }
        protected override byte FindSeatNumber(string line)
        {
            return byte.Parse(PlayerSeatRegex.Match(line).Value.Trim(), CultureInfo.InvariantCulture);
        }

        protected override decimal FindPlayerStartingStack(string line)
        {
            var s = PlayerStackRegex.Match(line).Value.Replace("$", "").Trim();
            s = s.Replace(((char)160).ToString(), "");//удалить все неразрывные пробелы nbsp (разделяет разряды)
            var numberFormatInfo = new NumberFormatInfo { NumberDecimalSeparator = "," };
            return Decimal.Parse(s, numberFormatInfo);
        }
        //todo:need to finish
        protected override bool FindBadHand(string inputString)
        {
            return false;
        }
        #region Parse Hand Action virtual methods
        protected virtual byte[] FindFlopCards(string inputString)
        {
            string[] strCards = FlopCardsRegex.Match(inputString).Value.Trim().Split(' ');
            var byteArray = new byte[3];
            for (int i = 0; i < strCards.Length; i++)
            {
                byteArray[i] = strCards[i].ConvertStringCardToByte();
            }
            return byteArray;
        }
        protected virtual byte FindTurnCard(string inputString)
        {
            string strCard = TurnCardRegex.Match(inputString).Value.Trim();
            return strCard.ConvertStringCardToByte();
        }
        protected virtual byte FindRiverCard(string inputString)
        {
            string strCard = RiverCardRegex.Match(inputString).Value.Trim();
            return strCard.ConvertStringCardToByte();
        }
        protected virtual byte[] FindShowdounCards(string inputString)
        {
            string[] strCards = ShowdounCardsRegex.Match(inputString).Value.Trim().Split(' ');
            var byteArray = new byte[2];
            for (int i = 0; i < strCards.Length; i++)
            {
                byteArray[i] = strCards[i].ConvertStringCardToByte();
            }
            return byteArray;
        }
        protected virtual byte[] FindMuckCards(IEnumerable<string> lines)
        {
            var sb=new StringBuilder();
            foreach (var line in lines)
            {
                sb.AppendLine(line);
            }
            string[] strCards = MuckCardsRegex.Match(sb.ToString()).Value.Trim().Split(' ');
            var byteArray = new byte[2];
            for (int i = 0; i < strCards.Length; i++)
            {
                byteArray[i] = strCards[i].ConvertStringCardToByte();
            }
            return byteArray;
        }
        protected virtual byte[] FindHeroCards(string inputString)
        {
            string[] strCards = HeroCardsRegex.Match(inputString).Value.Replace(" ", "").Split(',');
            var byteArray = new byte[2];
            for (var i = 0; i < strCards.Length; i++)
            {
                byteArray[i] = strCards[i].ConvertStringCardToByte();
            }
            return byteArray;
        }
        //todo:need to test
        protected virtual decimal DefineActionAmount(HandAction handAction, string inputLine)
        {
            string strAmout;
            switch (handAction.HandActionType)
            {
                case HandActionType.ALL_IN_CALL:
                    strAmout = AllInCallAmountRegex.Match(inputLine).Value.Replace("$", "").Replace(" ", "")
                    .Replace(((char)160).ToString(), "");
                    break;

                case HandActionType.ALL_IN_RAISE:
                    strAmout = AllInRaiseAmountRegex.Match(inputLine).Value.Replace("$", "").Replace(" ", "")
                    .Replace(((char)160).ToString(), "");
                    break;

                case HandActionType.WINS:
                    strAmout = CollectedPotRegex.Match(inputLine).Value.Replace("$", "").Replace(" ", "")
                    .Replace(((char)160).ToString(), "");
                    break;

                case HandActionType.WINS_MAIN_POT:
                    strAmout = CollectedMainPotRegex.Match(inputLine).Value.Replace("$", "").Replace(" ", "")
                    .Replace(((char)160).ToString(), "");
                    break;

                case HandActionType.WINS_SIDE_POT:
                    strAmout = CollectedSidePotRegex.Match(inputLine).Value.Replace("$", "").Replace(" ", "")
                    .Replace(((char)160).ToString(), "");
                    break;
                case HandActionType.UNCALLED_BET:
                    strAmout = UncalledBetAmountRegex.Match(inputLine).Value.Replace("$", "").Replace(" ", "")
                    .Replace(((char)160).ToString(), "");
                    break;

                default:
                    strAmout = inputLine.Split(' ').Last().Replace("$", "").Replace(" ", "")
                    .Replace(((char)160).ToString(), "");//удалить все неразрывные пробелы nbsp (разделяет разряды)
                    break;
            }
            
            var numberFormatInfo = new NumberFormatInfo { NumberDecimalSeparator = "," };
            decimal amount;
            if (!strAmout.Contains("+"))
                amount = decimal.Parse(strAmout, numberFormatInfo);
            else
            {
                var parts = strAmout.Split('+');
                amount = parts.Sum(part => decimal.Parse(part, numberFormatInfo));
            }
            switch (handAction.HandActionType)
            {
                case HandActionType.BET:
                case HandActionType.BIG_BLIND:
                case HandActionType.SMALL_BLIND:
                case HandActionType.CALL:
                case HandActionType.ALL_IN_CALL:
                case HandActionType.ALL_IN_RAISE:
                case HandActionType.ANTE:
                case HandActionType.RAISE:
                    return -amount;
                case HandActionType.WINS:
                case HandActionType.WINS_SIDE_POT:
                case HandActionType.WINS_MAIN_POT:
                case HandActionType.UNCALLED_BET://неотвечена ставка возвращается с положительным знаком
                    return amount;
            }
            throw new Exception("Undefined amount action!");
        }
        #endregion
    }
}
