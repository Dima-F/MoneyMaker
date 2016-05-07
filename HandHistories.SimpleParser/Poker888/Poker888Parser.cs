using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using HandHistories.SimpleObjects.Entities;
using HandHistories.SimpleObjects.Tools;

namespace HandHistories.SimpleParser.Poker888
{
    public abstract  class Poker888Parser:PokerParser
    {
        //шаблон рег. выраж.
        private static readonly Regex GameNumberRegex = new Regex(@"\d{9}", RegexOptions.Compiled);
        private static readonly Regex DateTimeRegex = new Regex(@"\d{2}\s\d{2}\s\d{4}\s\d{2}:\d{2}:\d{2}", RegexOptions.Compiled);
        private static readonly Regex DateRegex = new Regex(@"(\d+) (\d+) (\d+) ", RegexOptions.Compiled);
        
        private static readonly Regex LimitTypeRegex = new Regex(@"(?<=Blinds)[\w|\s]+(?=-\s\*{3})");
        private static readonly Regex ButtonPositionRegex = new Regex(@"(?<=Seat\s)\d+(?=\sis the button)");
        private static readonly Regex NumberOfPlayersRegex = new Regex(@"(?<=Total number of players : )\d+");
        private static readonly Regex PlayerNameRegex = new Regex(@"(?<=Seat\s\d+:).+(?=\()");
        private static readonly Regex PlayerSeatRegex = new Regex(@"(?<=Seat\s)\d+(?=:)");
        private static readonly Regex PlayerStackRegex = new Regex(@"(?<=Seat.+\().+(?=\))");

        private static readonly Regex ActionAmountRegex = new Regex(@"(?<=\[)[^shcd]+(?=\])");
        private static readonly Regex FlopCardsRegex = new Regex(@"(?<=flop\s*\*\*\s*\[\s*).+(?=\s*\])");
        private static readonly Regex TurnCardRegex = new Regex(@"(?<=turn\s*\*\*\s*\[\s*).+(?=\s*\])");
        private static readonly Regex RiverCardRegex = new Regex(@"(?<=river\s*\*\*\s*\[\s*).+(?=\s*\])");
        private static readonly Regex ShowdounCardsRegex = new Regex(@"(?<=shows\s*\[\s*).+(?=\s*\])");
        private static readonly Regex MuckCardsRegex = new Regex(@"(?<=mucks\s*\[\s*).+(?=\s*\])");
        private static readonly Regex HeroCardsRegex = new Regex(@"(?<=Dealt\sto\s\b.*\b\s*\[\s*).+(?=\s*\])");
        private static readonly Regex ErrorRegex = new Regex(@"Seat\s\d{1,2}:\s+\(\s*\$.{1,5}\s\)|Seat\s\d{1,2}:\s+\(\s*.{1,5}\$\s\)");
        private static readonly Regex MoneyTypeRegex=new Regex(@"(?<=\().+(?=\))");

        private static readonly Regex SmallBlindRegex = new Regex(@"(?<=).+(?=/)");
        private static readonly Regex BigBlindRegex = new Regex(@"(?<=/).+(?=\sBlinds)");

        protected override byte StartPlayerRow => 6;

        protected override NumberFormatInfo Format => new NumberFormatInfo { CurrencyDecimalSeparator = ".", CurrencyGroupSeparator = "," };

        public  override IEnumerable<HandAction> ParseHandActions(Game game, IReadOnlyList<string> multipleLines)
        {
            var handActions = new List<HandAction>();
            var currentStreet = Street.Preflop;
            for (var i = StartPlayerRow + game.NumberOfPlayers; i < multipleLines.Count; i++)
            {
                var ha = new HandAction() { Index = i };
                var lowerLine = multipleLines[i].ToLower();
                if (multipleLines[i].StartsWith("**"))
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
                    if (lowerLine.Contains("down cards"))
                    {
                        continue;
                    }
                    if (lowerLine.Contains("summary"))
                    {
                        currentStreet = Street.Showdown;
                        continue;
                    }
                    throw new ParserException($"No defined action with **. Unnown line -> {multipleLines[i]}", DateTime.Now);
                }//end **

                if (lowerLine.StartsWith("dealt to"))
                {
                    ha.PlayerName  = multipleLines[i].Split(' ')[2].Trim();
                    ha.Street = currentStreet;
                    ha.HandActionType = HandActionType.DEALT_HERO_CARDS;
                    game.Hero = ha.PlayerName;
                    game.PlayerHistories.Find(p => p.PlayerName == ha.PlayerName).HoleCards.InitializeNewCards(FindHeroCards(multipleLines[i]));
                    handActions.Add(ha);
                    continue;
                }
                //line don't starts with ** and "dealt to"
                //put new conditional in this place if some new statemants will apear
                //.....
                //.....
                var parts = multipleLines[i].Split(' ');
                ha.PlayerName = parts[0].Trim();
                ha.Street = currentStreet;
                switch (parts[1].Trim())
                {
                    case "posts":
                        switch (parts[2].Trim())
                        {
                            case "big":
                                ha.HandActionType = HandActionType.BIG_BLIND;
                                ha.Amount = DefineActionAmount(ha, multipleLines[i]);
                                handActions.Add(ha);
                                break;
                            case "small":
                                ha.HandActionType = HandActionType.SMALL_BLIND;
                                ha.Amount = DefineActionAmount(ha, multipleLines[i]);
                                handActions.Add(ha);
                                break;
                            case "ante":
                                ha.HandActionType = HandActionType.ANTE;
                                ha.Amount = DefineActionAmount(ha, multipleLines[i]);
                                handActions.Add(ha);
                                break;
                            case "dead":
                                ha.HandActionType = HandActionType.DEAD_BLIND;
                                ha.Amount = DefineActionAmount(ha, multipleLines[i]);
                                handActions.Add(ha);
                                break;
                            default:
                                throw new ParserException($"Undefined action. Unnown line -> {multipleLines[i]}", DateTime.Now);
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
                        ha.HandActionType = HandActionType.CALL;
                        ha.Amount = DefineActionAmount(ha, multipleLines[i]);
                        handActions.Add(ha);
                        continue;
                    case "bets":
                        ha.HandActionType = HandActionType.BET;
                        ha.Amount = DefineActionAmount(ha, multipleLines[i]);
                        handActions.Add(ha);
                        continue;
                    case "raises":
                        ha.HandActionType = HandActionType.RAISE;
                        ha.Amount = DefineActionAmount(ha, multipleLines[i]);
                        handActions.Add(ha);
                        continue;
                    case "shows":
                        ha.HandActionType = HandActionType.SHOW;
                        game.PlayerHistories.Find(p => p.PlayerName == ha.PlayerName).HoleCards.InitializeNewCards(FindShowdounCards(multipleLines[i]));
                        handActions.Add(ha);
                        continue;
                    case "mucks":
                        ha.HandActionType = HandActionType.MUCKS;
                        game.PlayerHistories.Find(p => p.PlayerName == ha.PlayerName).HoleCards.InitializeNewCards(FindMuckCards(multipleLines[i]));
                        handActions.Add(ha);
                        continue;
                    case "collected":
                        ha.HandActionType = HandActionType.WINS;
                        ha.Amount = DefineActionAmount(ha, multipleLines[i]);
                        handActions.Add(ha);
                        continue;
                }
            }
            CheckAllHandActionsForUncalled(handActions);
            return handActions;
        }

        protected override bool FindBadHand(string inputString)
        {
            return ErrorRegex.IsMatch(inputString);
        }

        protected override ulong FindGameNumber(IEnumerable<string> hand)
        {
            var line = hand.ToList()[0];
            var matchGameNumber = GameNumberRegex.Match(line).Value.Trim(' ');
            return UInt64.Parse(matchGameNumber);
        }

        protected override double FindBigBlind(IEnumerable<string> hand)
        {
            var line = hand.ToList()[2];
            var s = BigBlindRegex.Match(line).Value;
            return GetCleanAmount(s);
        }

        protected override double FindSmallBlind(IEnumerable<string> hand)
        {
            var line = hand.ToList()[2];
            var s = SmallBlindRegex.Match(line).Value;
            return GetCleanAmount(s);
        }

        protected override DateTime FindDateOfGame(IEnumerable<string> hand)
        {
            var line = hand.ToList()[2];
            var matchDateOfHand = DateTimeRegex.Match(line).Value.Trim(' ');
            //Change string so it becomes 2012-02-04 23:59:48
            var correctDateOfHand = DateRegex.Replace(matchDateOfHand, "$3-$2-$1 ");
            return DateTime.Parse(correctDateOfHand);
        }
        
        protected override LimitType FindLimitType(IEnumerable<string> hand)
        {
            var line = hand.ToList()[2];
            var initialMatch = LimitTypeRegex.Match(line).Value;
            string gameTypeString = initialMatch.Trim();
            return ConvertLimitTypeEnum(gameTypeString);
        }

        protected override MoneyType FindMoneyType(IEnumerable<string> hand)
        {
            var line = hand.ToList()[3];
            var initialMatch = MoneyTypeRegex.Match(line).Value;
            return ConvertMoneyTypeEnum(initialMatch);
        }

        protected override byte FindNumberOfPlayers(string hand)
        {
            return Byte.Parse(NumberOfPlayersRegex.Match(hand).Value.Trim());
        }

        protected override byte FindButtonPosition(IEnumerable<string> hand)
        {
            var line = hand.ToList()[4];
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

        #region Parse Hand Action virtual methods
        protected virtual byte[] FindFlopCards(string inputString)
        {
            string[] strCards = FlopCardsRegex.Match(inputString).Value.Replace(" ", "").Split(',');
            var byteArray = new byte[3];
            for (int i = 0; i < strCards.Length; i++)
            {
                byteArray[i] = strCards[i].ConvertStringCardToByte();
            }
            return byteArray;
        }

        protected virtual byte FindTurnCard( string inputString)
        {
            string strCard = TurnCardRegex.Match(inputString).Value.Trim();
            return strCard.ConvertStringCardToByte();
        }

        protected virtual byte FindRiverCard( string inputString)
        {
            string strCard = RiverCardRegex.Match(inputString).Value.Trim();
            return strCard.ConvertStringCardToByte();
        }

        protected virtual byte[] FindShowdounCards(string inputString)
        {
            string[] strCards = ShowdounCardsRegex.Match(inputString).Value.Trim().Replace(" ", "").Split(',');
            var byteArray = new byte[2];
            for (int i = 0; i < strCards.Length; i++)
            {
                byteArray[i] = strCards[i].ConvertStringCardToByte();
            }
            return byteArray;
        }

        protected virtual byte[] FindMuckCards(string inputString)
        {
            string[] strCards = MuckCardsRegex.Match(inputString).Value.Replace(" ", "").Split(',');
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

        protected virtual double DefineActionAmount(HandAction handAction, string inputLine)
        {
            var match = ActionAmountRegex.Match(inputLine).Value;
            var amount = GetCleanAmount(match);
            switch (handAction.HandActionType)
            {
                case HandActionType.BET:
                case HandActionType.BIG_BLIND:
                case HandActionType.SMALL_BLIND:
                case HandActionType.DEAD_BLIND:
                case HandActionType.CALL:
                case HandActionType.ALL_IN_CALL:
                case HandActionType.ALL_IN_RAISE:
                case HandActionType.ANTE:
                case HandActionType.RAISE:
                    return -amount;
                case HandActionType.WINS:
                case HandActionType.WINS_SIDE_POT:
                case HandActionType.WINS_MAIN_POT:
                //case HandActionType.UNCALLED_BET:
                    return amount;
            }
            throw new Exception("Undefined amount action!");
        }
        #endregion
        

    }
}
