using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using HandHistories.SimpleObjects.Entities;

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
                game.LimitType = FindGameType(multipleLines);
                game.MoneyType = FindMoneyType(multipleLines);
                game.SeatType = FindSeatType(multipleLines);
                game.ButtonPosition = FindButtonPosition(multipleLines);
                game.NumberOfPlayers = FindNumberOfPlayers(multipleLines);
                game.PlayerHistories = ParsePlayers(game.GameNumber, game.NumberOfPlayers, multipleLines);
                game.HandActions = ParseHandActions(game, multipleLines).ToList();
                games.Add(game);
            }
            return games;
        }

        public abstract IDictionary<string, string> GetMainInfo(string path);

        /// <summary>
        /// Ф:Пока я не буду делать метод абстрактным и перемещать реализацию у Poker888Parser, а оставлю возможность переопределения потомками.
        /// Возможно этот подход придется изменить в связи с новыми обстоятельствами, возникшими при анализе истории рук рума Покерстарс.
        /// </summary>
        public virtual  List<PlayerHistory> ParsePlayers(int gameNumber, byte numberOfPlayers, IReadOnlyList<string> multipleLines)
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
                    HoleCards = new byte[2]
                };
                players.Add(player);
            }
            return players;
        }

        public  abstract  IEnumerable<HandAction> ParseHandActions(Game game, IReadOnlyList<string> multipleLines);

        protected abstract int FindGameNumber(IEnumerable<string> hand);

        protected abstract DateTime FindDateOfGame(IEnumerable<string> hand);

        protected abstract string FindTableName(IEnumerable<string> hand);

        protected abstract LimitType FindGameType(IEnumerable<string> hand);

        protected abstract MoneyType FindMoneyType(IEnumerable<string> hand);

        protected abstract SeatType FindSeatType(IEnumerable<string> hand);

        protected abstract byte FindNumberOfPlayers(IEnumerable<string> hand);

        protected abstract byte FindButtonPosition(IEnumerable<string> hand);

        protected abstract string FindPlayerName(string line);

        protected abstract byte FindSeatNumber(string line);

        protected abstract decimal FindPlayerStartingStack(string line);

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
        
        //todo:возможно нужно проверить ситуацию по олинам, когда один дает больше другого, тогда остаток тоже нужно нивелировать.
        protected static void CheckAllHandActionsForUncalled(IList<HandAction> handActions)
        {
            var count = handActions.Count;
            var lastAgrassiveAction =
                handActions.LastOrDefault(
                    ha => ha.HandActionType == HandActionType.BET || ha.HandActionType == HandActionType.RAISE || ha.HandActionType == HandActionType.ALL_IN);
            if (lastAgrassiveAction == null) return;
            var index = handActions.IndexOf(lastAgrassiveAction);
            for (var i = index; i < count; i++)
            {
                if (handActions[i].HandActionType == HandActionType.CALL || handActions[i].HandActionType == HandActionType.ALL_IN)
                    return;
            }
            //тип действия пока не меняем, чтобы не вызвать сбои в остальной части кода програмы.
            //lastAgrassiveAction.HandActionType = HandActionType.UNCALLED_BET;
            lastAgrassiveAction.Amount = 0;
        }

        protected static SeatType ConvertSeatEnum(string seatTypeString)
        {
            switch (seatTypeString.ToLower())
            {
                case "9 max":
                    return SeatType._FullRing_9Handed;
                case "6 max":
                    return SeatType._6Max;
                default:
                    return SeatType.Unknown;
            }
        }

        protected static LimitType ConvertGameTypeEnum(string gameTypeString)
        {
            switch (gameTypeString.ToLower())
            {
                case "no limit holdem":
                    return LimitType.NoLimit;
                default:
                    return LimitType.Any;
            }
        }
    }
}
