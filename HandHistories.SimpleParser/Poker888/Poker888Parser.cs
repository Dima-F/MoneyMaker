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
        
        private static readonly Regex GameTypeRegex = new Regex(@"(?<=Blinds)[\w|\s]+(?=-\s\*{3})");
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

        protected override byte StartPlayerRow
        {
            get { return 6; }
        }

        public  override IEnumerable<HandAction> ParseHandActions(Game game, IReadOnlyList<string> multipleLines)
        {
            var handActions = new List<HandAction>();
            var currentStreet = Street.Preflop;
            for (var i = StartPlayerRow + game.NumberOfPlayers; i < multipleLines.Count; i++)
            {
                var ha = new HandAction { GameNumber = game.GameNumber };
                if (multipleLines[i].StartsWith("**"))
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
                    if (multipleLines[i].ToLower().Contains("down cards"))
                    {
                        continue;
                    }
                    if (multipleLines[i].ToLower().Contains("summary"))
                    {
                        ha.Source = NameOfSystem;
                        ha.HandActionType = HandActionType.SUMMARY;
                        handActions.Add(ha);
                        currentStreet = Street.Showdown;
                        continue;
                    }
                    throw new Exception("No defined action");
                }

                if (multipleLines[i].ToLower().StartsWith("dealt to"))
                {
                    ha.Source = ha.Source = multipleLines[i].Split(' ')[2].Trim();
                    ha.Street = currentStreet;
                    ha.HandActionType = HandActionType.DEALT_HERO_CARDS;
                    game.Hero = ha.Source;
                    game.PlayerHistories.Find(p => p.PlayerName == ha.Source).HoleCards.InitializeNewCards(FindHeroCards(multipleLines[i]));
                    handActions.Add(ha);
                }


                else//line don't starts with ** and "dealt to"
                {
                    ha.Source = multipleLines[i].Split(' ')[0].Trim();
                    ha.Street = currentStreet;
                    var action = multipleLines[i].Split(' ')[1].Trim();
                    switch (action)
                    {
                        case "posts":
                            if (multipleLines[i].Split(' ')[2].Trim() == "big")
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
                            game.PlayerHistories.Find(p => p.PlayerName == ha.Source).HoleCards.InitializeNewCards(FindShowdounCards(multipleLines[i]));
                            handActions.Add(ha);
                            continue;
                        case "mucks":
                            ha.HandActionType = HandActionType.MUCKS;
                            game.PlayerHistories.Find(p => p.PlayerName == ha.Source).HoleCards.InitializeNewCards(FindMuckCards(multipleLines[i]));
                            handActions.Add(ha);
                            continue;
                        case "collected":
                            ha.HandActionType = HandActionType.WINS;
                            ha.Amount = DefineActionAmount(ha, multipleLines[i]);
                            handActions.Add(ha);
                            continue;
                    }
                }
            }
            CheckAllHandActionsForUncalled(handActions);
            return handActions;
        }

        protected override bool FindBadHand(string inputString)
        {
            return ErrorRegex.IsMatch(inputString);
        }

        protected override int FindGameNumber(IEnumerable<string> hand)
        {
            var line = hand.ToList()[0];
            var matchGameNumber = GameNumberRegex.Match(line).Value.Trim(' ');
            return Int32.Parse(matchGameNumber);
        }

        protected override DateTime FindDateOfGame(IEnumerable<string> hand)
        {
            var line = hand.ToList()[2];
            var matchDateOfHand = DateTimeRegex.Match(line).Value.Trim(' ');
            //Change string so it becomes 2012-02-04 23:59:48
            var correctDateOfHand = DateRegex.Replace(matchDateOfHand, "$3-$2-$1 ");
            return DateTime.Parse(correctDateOfHand);
        }
        
        protected override LimitType FindGameType(IEnumerable<string> hand)
        {
            var line = hand.ToList()[2];
            var initialMatch = GameTypeRegex.Match(line).Value;
            string gameTypeString = initialMatch.Trim();
            return ConvertGameTypeEnum(gameTypeString);
        }

        protected override MoneyType FindMoneyType(IEnumerable<string> hand)
        {
            var line = hand.ToList()[3];
            var initialMatch = MoneyTypeRegex.Match(line).Value.ToLower();
            if (initialMatch.Contains("real"))
                return MoneyType.RealMoney;
            if (initialMatch.Contains("play"))
                return MoneyType.PlayMoney;
            throw new Exception("Something wrong with parsing hand's money type.");
        }

        protected override byte FindNumberOfPlayers(IEnumerable<string> hand)
        {
            var line = hand.ToList()[5];
            return Byte.Parse(NumberOfPlayersRegex.Match(line).Value.Trim());
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

        protected override decimal FindPlayerStartingStack(string line)
        {
            var s = PlayerStackRegex.Match(line).Value.Replace("$","").Trim();
            s = s.Replace(((char)160).ToString(), "");//удалить все неразрывные пробелы nbsp (разделяет разряды)
            var numberFormatInfo = new NumberFormatInfo {NumberDecimalSeparator = ","};
            var d = Decimal.Parse(s, numberFormatInfo);
            return d;
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

        protected virtual decimal DefineActionAmount(HandAction handAction, string inputLine)
        {
            var match =
                ActionAmountRegex.Match(inputLine)
                    .Value.Replace("$", "")
                    .Replace(" ", "")
                    .Replace(((char) 160).ToString(), "");//удалить все неразрывные пробелы nbsp (разделяет разряды)
            var numberFormatInfo = new NumberFormatInfo { NumberDecimalSeparator = "," };
            decimal amount;
            if (!match.Contains("+"))
                amount= decimal.Parse(match,numberFormatInfo);
            else
            {
                var parts = match.Split('+');
                amount = parts.Sum(part => decimal.Parse(part,numberFormatInfo));
            }
            switch (handAction.HandActionType)
            {
                case HandActionType.BET:
                case HandActionType.BIG_BLIND:
                case HandActionType.SMALL_BLIND:
                case HandActionType.CALL:
                case HandActionType.ALL_IN:
                case HandActionType.ANTE:
                case HandActionType.RAISE:
                    return -amount;
                case HandActionType.WINS:
                case HandActionType.WINS_SIDE_POT:
                case HandActionType.WINS_THE_LOW:
                    return amount;
            }
            throw new Exception("Undefined amount action!");
        }
        #endregion
        
    }
}
