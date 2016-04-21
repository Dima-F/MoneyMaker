using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using HandHistories.SimpleObjects.Entities;
using HandHistories.SimpleParser.PokerStars;

namespace HandHistories.SimpleParser
{
    public abstract class PokerParser
    {
        protected const string NameOfSystem = "System";

        protected static readonly Regex GameSplitRegex = new Regex("(?:\r\n\r\n)|(?:\n{3,})", RegexOptions.Compiled);

        protected abstract byte StartPlayerRow { get; }

        protected abstract bool IsTournament { get; }
        
        public List<Game> ParseGames(string allHandsText)
        {
            var games = new List<Game>();
            IEnumerable<string> multipleGames = SplitAllTextInMultipleGames(allHandsText);
            foreach (var concreteGame in multipleGames)
            {
                if (FindBadHand(concreteGame))
                    continue;
                var game = new Game { BoardCards = new byte[5] };
                var multipleLines = SplitOneGameTextInMultipleLines(concreteGame);
                game.GameNumber = FindGameNumber(multipleLines);
                game.HandText = concreteGame;
                game.DateOfHand = FindDateOfGame(multipleLines);
                game.TableName = FindTableName(multipleLines);
                game.SmallBlind = FindSmallBlind(multipleLines);
                game.BigBlind = FindBigBlind(multipleLines);
                game.LimitType = FindLimitType(multipleLines);
                game.MoneyType = FindMoneyType(multipleLines);
                game.SeatType = FindSeatType(multipleLines);
                game.ButtonPosition = FindButtonPosition(multipleLines);
                game.NumberOfPlayers = FindNumberOfPlayers(concreteGame);
                game.PlayerHistories = ParsePlayers(game.GameNumber, game.NumberOfPlayers, multipleLines);
                IEnumerable<HandAction> allHandActions = ParseHandActions(game, multipleLines).ToArray();
                foreach (var playerHistory in game.PlayerHistories)
                {
                    var playerActions = allHandActions.Where(ha => ha.Source == playerHistory.PlayerName).ToArray();
                    playerHistory.HandActions.AddRange(playerActions);
                }
                games.Add(game);
            }
            return games;
        }

        public abstract IDictionary<string, string> GetInfoFromPath(string path);

        public virtual  List<PlayerHistory> ParsePlayers(ulong gameNumber, byte numberOfPlayers, IReadOnlyList<string> multipleLines)
        {
            var players = new List<PlayerHistory>();
            for (var i = 0; i < numberOfPlayers; i++)
            {
                string oneLine = multipleLines[StartPlayerRow + i];
                var player = new PlayerHistory
                {
                    GameNumber = gameNumber,
                    PlayerName = FindPlayerName(oneLine),
                    SeatNumber = FindSeatNumber(oneLine),
                    StartingStack = FindPlayerStartingStack(oneLine),
                    HoleCards = new byte[2],
                    HandActions = new List<HandAction>()
                };
                players.Add(player);
            }
            return players;
        }

        public  abstract  IEnumerable<HandAction> ParseHandActions(Game game, IReadOnlyList<string> multipleLines);

        protected abstract ulong FindGameNumber(IEnumerable<string> hand);

        protected abstract DateTime FindDateOfGame(IEnumerable<string> hand);

        protected abstract string FindTableName(IEnumerable<string> hand);

        protected abstract double FindSmallBlind(IEnumerable<string> hand);

        protected abstract double FindBigBlind(IEnumerable<string> hand);

        protected abstract LimitType FindLimitType(IEnumerable<string> hand);

        protected abstract MoneyType FindMoneyType(IEnumerable<string> hand);

        protected abstract SeatType FindSeatType(IEnumerable<string> hand);

        protected abstract byte FindNumberOfPlayers(string hand);

        protected abstract byte FindButtonPosition(IEnumerable<string> hand);

        protected abstract string FindPlayerName(string line);

        protected abstract byte FindSeatNumber(string line);

        protected abstract double FindPlayerStartingStack(string line);

        protected abstract bool FindBadHand(string inputString);
        
        private static IEnumerable<string> SplitAllTextInMultipleGames(string allHandsText)
        {
            return GameSplitRegex.Split(allHandsText)//разделить входную строку по шаблону регулярного выраж.
                            .Where(s => !String.IsNullOrWhiteSpace(s))//откинуть пустые строки
                            .Select(s => s.Trim('\r', '\n')).ToList();//удалить начальные и конечные управляющие символи
        }

        private static  List<string> SplitOneGameTextInMultipleLines(string gameText)
        {
            var text = gameText.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < text.Length; i++)
            {
                text[i] = text[i].Trim();
            }
            return text.ToList();
        }

        //todo:need to refactoring and restructing
        protected static void CheckAllHandActionsForUncalled(IList<HandAction> handActions)
        {
            var count = handActions.Count;
            var lastAgrassiveAction =
                handActions.LastOrDefault(ha => ha.HandActionType == HandActionType.BET || ha.HandActionType == HandActionType.RAISE 
                || ha.HandActionType == HandActionType.ALL_IN_RAISE);
            if (lastAgrassiveAction == null) return;
            var index = handActions.IndexOf(lastAgrassiveAction);
            for (var i = index; i < count; i++)
            {
                if (handActions[i].HandActionType == HandActionType.CALL || handActions[i].HandActionType == HandActionType.ALL_IN_CALL)
                    return;
            }
            //need to refactoring
            lastAgrassiveAction.Amount = 0;
            var uncalledBetHandAction = new HandAction
            {
                Source = lastAgrassiveAction.Source,
                HandActionType = HandActionType.UNCALLED_BET,
                Street = lastAgrassiveAction.Street,
                Amount = -lastAgrassiveAction.Amount
            };
            handActions.Add(uncalledBetHandAction);
        }

        protected static SeatType ConvertSeatEnum(string seatTypeString)
        {
            switch (seatTypeString.Trim().ToLower())
            {
                case "6 max":
                case "6-max":
                    return SeatType._6Max;
                case "9 max":
                case "9-max":
                    return SeatType._FullRing_9Handed;
                case "10 max":
                case "10-max":
                    return SeatType._FullRing_10Handed;

                default:
                    return SeatType.Unknown;
            }
        }

        protected static LimitType ConvertLimitTypeEnum(string gameTypeString)
        {
            switch (gameTypeString.ToLower())
            {
                case "no limit holdem"://888
                case "hold'em no limit"://PokerStars
                    return LimitType.NoLimit;
                default:
                    throw new NotImplementedException();
            }
        }

        protected MoneyType ConvertMoneyTypeEnum(string moneyTypeString)
        {
            if(this is PokerStarsTournamentParser)
                return moneyTypeString.Contains('$') ? MoneyType.RealMoney : MoneyType.PlayMoney;
            return moneyTypeString.ToLower().Contains("play") ? MoneyType.PlayMoney : MoneyType.RealMoney;
            
        }

        protected double GetCleanAmount(string value)
        {
            var s = value.Replace("$", "").Replace(',', '.').Trim();
            s = s.Replace(((char)160).ToString(), "");//удалить все неразрывные пробелы nbsp (разделяет разряды)
            if (!s.Contains("+"))
                return double.Parse(s, new NumberFormatInfo { CurrencyDecimalSeparator = "." });
            var parts = s.Split('+');
            return parts.Sum(part => double.Parse(part, new NumberFormatInfo { CurrencyDecimalSeparator = "." }));
        }
    }
}
