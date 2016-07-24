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
    public abstract  class PokerStarsParser:PokerParser
    {
        private static readonly Regex GameNumberRegex = new Regex(@"(?<=\#)\d{6,}(?=\:)");
        private static readonly Regex DateTimeRegex = new Regex(@"(?<=-\s*)\d{4}\/\d{2}\/\d{2}\s+\d{2}:\d{2}:\d{2}(?=\s+EET)");
        private static readonly Regex DateRegex = new Regex(@"(\d+)/(\d+)/(\d+)");
        private static readonly Regex TableNameRegex = new Regex(@"(?<=').+(?=')");
        private static readonly Regex BlindsInTopRegex = new Regex(@"(?<=\().+(?=\))");

        private static readonly Regex ButtonPositionRegex = new Regex(@"(?<=#)\d{1,2}(?=\s*is)");
        private static readonly Regex NumberOfPlayersRegex = new Regex(@"Seat\s\d{1,2}:");
        private static readonly Regex PlayerNameRegex = new Regex(@"(?<=Seat\s\d{1,2}:\s*).+(?=\()");
        private static readonly Regex PlayerSeatRegex = new Regex(@"(?<=Seat\s)\d+(?=:)");
        private static readonly Regex PlayerStackRegex = new Regex(@"(?<=Seat.+\().+(?=\sin)");

        private static readonly Regex AllInCallAmountRegex = new Regex(@"(?<=:\s+calls\s+).+(?=\s+and\sis\sall-in)");
        private static readonly Regex AllInRaiseAmountRegex = new Regex(@"(?<=:\s+raises\s+).+(?=\sto\s.+\s+and\sis\sall-in)");
        private static readonly Regex AllInBetAmountRegex = new Regex(@"(?<=:\s+bets\s+).+(?=\s+and\sis\sall-in)");
        private static readonly Regex AllInBlindsAmountRegex = new Regex(@"(?<=blind\s+).+(?=\s+and\sis\sall-in)");
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
        private static readonly Regex AnteAmountRegex = new Regex(@"(?<=the\sante\s).+(?=)");
        private static readonly Regex WinPlayerRegex = new Regex(@"(?<=).+(?=\s+collected)");
        private static readonly Regex ErrorRegex = new Regex(@"");
        
        protected override byte StartPlayerRow => 2;

        protected override NumberFormatInfo Format => new NumberFormatInfo
        {
            CurrencyDecimalSeparator = "."
        };

        public override IEnumerable<HandAction> ParseHandActions(Game game, IReadOnlyList<string> multipleLines)
        {
            var handActions = new List<HandAction>();
            var currentStreet = Street.Preflop;
            for (var i = StartPlayerRow + game.NumberOfPlayers; i < multipleLines.Count; i++)
            {
                var ha = new HandAction() { Index = i };
                var lowerLine = multipleLines[i].ToLower();
                if (multipleLines[i].StartsWith("***"))
                {
                    if (lowerLine.Contains("flop"))
                    {
                        game.BoardCards.InitializeNewCards(FindFlopCards(multipleLines[i]));
                        currentStreet = Street.Flop;
                        continue;
                    }
                    if (lowerLine.Contains("turn"))
                    {
                        game.BoardCards[3] = FindTurnCard(multipleLines[i]);
                        currentStreet = Street.Turn;
                        continue;
                    }
                    if (lowerLine.Contains("river"))
                    {
                        game.BoardCards[4] = FindRiverCard(multipleLines[i]);
                        currentStreet = Street.River;
                        continue;
                    }
                    if (lowerLine.Contains("hole"))
                    {
                        continue;
                    }
                    if (lowerLine.Contains("show down"))
                    {
                        currentStreet = Street.Showdown;
                        continue;
                    }
                    if (lowerLine.Contains("summary"))
                    {
                        break;//пока что summary строки не будем обрабатывать.
                    }
                    throw new ParserException($"No defined action with ***. Unnown line -> {multipleLines[i]}", DateTime.Now);
                } //end ***
                if (lowerLine.StartsWith("dealt to"))
                {
                    ha.PlayerName = HeroNameRegex.Match(multipleLines[i]).Value.Trim();
                    ha.Street = currentStreet;
                    ha.HandActionType = HandActionType.DEALT_HERO_CARDS;
                    game.Hero = ha.PlayerName;
                    game.PlayerHistories.Find(p => p.PlayerName == ha.PlayerName).HoleCards.InitializeNewCards(FindHeroCards(multipleLines[i]));
                    handActions.Add(ha);
                    continue;
                }
                
                
                //hand actions of type "player:action"
                if (multipleLines[i].Contains(':'))
                {
                    var parts = multipleLines[i].Split(':');
                    ha.PlayerName = parts[0].Trim();
                    ha.Street = currentStreet;
                    var actionParts = parts[1].Trim().Split(' ');
                    switch (actionParts[0])
                    {
                        case "posts":
                            var key = actionParts[1];
                            switch (key)
                            {
                                case "big":
                                    ha.HandActionType = multipleLines[i].Contains("all-in") ? HandActionType.All_IN_BB : HandActionType.BIG_BLIND;
                                    ha.Amount = DefineActionAmount(ha, multipleLines[i]);
                                    handActions.Add(ha);
                                    break;
                                case "small":
                                    ha.HandActionType = multipleLines[i].Contains("all-in") ? HandActionType.All_IN_SB : HandActionType.SMALL_BLIND;
                                    ha.Amount = DefineActionAmount(ha, multipleLines[i]);
                                    handActions.Add(ha);
                                    break;
                                case "the"://ante
                                    ha.HandActionType = HandActionType.ANTE;
                                    ha.Amount = DefineActionAmount(ha, multipleLines[i]);
                                    handActions.Add(ha);
                                    break;
                                default:
                                    throw new ParserException( $"No defined action that contains posts. Unnown line -> {multipleLines[i]}", DateTime.Now);
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
                            ha.HandActionType = multipleLines[i].Contains("all-in") ? HandActionType.ALL_IN_BET : HandActionType.BET;
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
                            game.PlayerHistories.Find(p => p.PlayerName == ha.PlayerName).HoleCards.InitializeNewCards(FindShowdounCards(multipleLines[i]));
                            handActions.Add(ha);
                            continue;
                        case "mucks":
                            ha.HandActionType = HandActionType.MUCKS;
                            game.PlayerHistories.Find(p => p.PlayerName == ha.PlayerName).HoleCards.InitializeNewCards(FindMuckCards(multipleLines));
                            handActions.Add(ha);
                            continue;
                        default:
                            throw new ParserException( $"Cann't parse string with ':' and unnown word. Unnown line -> {multipleLines[i]}", DateTime.Now);
                    }
                }
                //end contains : 
                if (lowerLine.Contains("uncalled bet"))
                {
                    ha.HandActionType = HandActionType.UNCALLED_BET;
                    ha.PlayerName = UncalledBetPlayerRegex.Match(multipleLines[i]).Value;
                    ha.Amount = DefineActionAmount(ha, multipleLines[i]);
                    handActions.Add(ha);
                    continue;
                }
                if (lowerLine.Contains("collected"))
                {
                    if (lowerLine.Contains("main"))
                    {
                        ha.HandActionType = HandActionType.WINS_MAIN_POT;
                    }
                    else if (lowerLine.Contains("side"))
                    {
                        ha.HandActionType = HandActionType.WINS_SIDE_POT;
                    }
                    else
                    {
                        ha.HandActionType = HandActionType.WINS;
                    }
                    ha.PlayerName = WinPlayerRegex.Match(multipleLines[i]).Value;
                    ha.Amount = DefineActionAmount(ha, multipleLines[i]);
                    handActions.Add(ha);
                    continue;
                }
                //hand actions without tracking:
                if (lowerLine.Contains("leaves the table"))
                {
                    continue;
                }
                if (lowerLine.Contains("joins the table"))
                {
                    continue;
                }
                if (lowerLine.Contains("will be allowed to play"))
                {
                    continue;
                }
                if (lowerLine.Contains("is disconnected"))
                {
                    continue;
                }
                if (lowerLine.Contains("was removed from the table"))
                {
                    continue;
                }
                if (lowerLine.Contains("finished the tournament"))
                {
                    continue;
                }
                if (lowerLine.Contains("said,"))
                {
                    continue;
                }
                if (lowerLine.Contains("has timed out"))
                {
                    continue;
                }
                if (lowerLine.Contains("is connected"))
                {
                    continue;
                }
                if (lowerLine.Contains("bounty for eliminating"))
                {
                    continue;
                }
                throw new ParserException($"Line with a new words -> {multipleLines[i]}", DateTime.Now);
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

        protected override double FindSmallBlind(IEnumerable<string> hand)
        {
            var line = hand.ToList()[0];
            var s = BlindsInTopRegex.Match(line).Value.Split('/')[0];
            return GetCleanAmount(s);
        }

        protected override double FindBigBlind(IEnumerable<string> hand)
        {
            var line = hand.ToList()[0];
            var s = BlindsInTopRegex.Match(line).Value.Split('/')[1];
            return GetCleanAmount(s);
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

        protected override double FindPlayerStartingStack(string line)
        {
            var s = PlayerStackRegex.Match(line).Value;
            return GetCleanAmount(s);
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
            string[] strCards = HeroCardsRegex.Match(inputString).Value.Trim().Split(' ');
            var byteArray = new byte[2];
            for (var i = 0; i < strCards.Length; i++)
            {
                byteArray[i] = strCards[i].ConvertStringCardToByte();
            }
            return byteArray;
        }

        //todo:need to test
        protected virtual double DefineActionAmount(HandAction handAction, string inputLine)
        {
            string strAmout;
            switch (handAction.HandActionType)
            {
                case HandActionType.ALL_IN_CALL:
                    strAmout = AllInCallAmountRegex.Match(inputLine).Value;
                    break;

                case HandActionType.ALL_IN_RAISE:
                    strAmout = AllInRaiseAmountRegex.Match(inputLine).Value;
                    break;
                case HandActionType.ALL_IN_BET:
                    strAmout = AllInBetAmountRegex.Match(inputLine).Value;
                    break;

                case HandActionType.WINS:
                    strAmout = CollectedPotRegex.Match(inputLine).Value;
                    break;

                case HandActionType.WINS_MAIN_POT:
                    strAmout = CollectedMainPotRegex.Match(inputLine).Value;
                    break;

                case HandActionType.WINS_SIDE_POT:
                    strAmout = CollectedSidePotRegex.Match(inputLine).Value;
                    break;
                case HandActionType.UNCALLED_BET:
                    strAmout = UncalledBetAmountRegex.Match(inputLine).Value;
                    break;
                    case HandActionType.ANTE:
                    strAmout = AnteAmountRegex.Match(inputLine).Value;
                    break;
                case HandActionType.All_IN_BB:
                case HandActionType.All_IN_SB:
                    strAmout = AllInBlindsAmountRegex.Match(inputLine).Value;
                    break;
                default:
                    strAmout = inputLine.Split(' ').Last();
                    break;
            }

            var amount = GetCleanAmount(strAmout);
            switch (handAction.HandActionType)
            {
                case HandActionType.BET:
                case HandActionType.BIG_BLIND:
                case HandActionType.SMALL_BLIND:
                case HandActionType.CALL:
                case HandActionType.ALL_IN_CALL:
                case HandActionType.ALL_IN_BET:
                case HandActionType.ALL_IN_RAISE:
                case HandActionType.All_IN_BB:
                case HandActionType.All_IN_SB:
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
