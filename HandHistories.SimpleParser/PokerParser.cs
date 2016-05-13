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
        //коллекция - словарь, которая содержит в себе инф. о наименовании позиций в зависимости от количества игроков за столом (он же ключ словаря)
        //принцип пропадания позиций с уменьшением стола может отличатся от общепринятого и выбран мною для последующего удобства вычисления статистики.
        private readonly Dictionary<byte, List<PositionType>> _positionsDictionary = new Dictionary<byte, List<PositionType>>
        {
            {2, new List<PositionType>() {PositionType.B, PositionType.SB} },
            {3, new List<PositionType>() {PositionType.B, PositionType.SB, PositionType.BB} },
            {4, new List<PositionType>() {PositionType.B, PositionType.SB, PositionType.BB, PositionType.CO} },
            {5, new List<PositionType>() {PositionType.B, PositionType.SB, PositionType.BB, PositionType.MP, PositionType.CO } },
            {6, new List<PositionType>() {PositionType.B, PositionType.SB, PositionType.BB, PositionType.UTG, PositionType.MP, PositionType.CO } },
            {7, new List<PositionType>() {PositionType.B, PositionType.SB, PositionType.BB, PositionType.UTG, PositionType.MP, PositionType.MP2, PositionType.CO } },
            {8, new List<PositionType>() {PositionType.B, PositionType.SB, PositionType.BB, PositionType.UTG, PositionType.UTG2, PositionType.MP, PositionType.MP2, PositionType.CO } },
            {9, new List<PositionType>() {PositionType.B, PositionType.SB, PositionType.BB, PositionType.UTG, PositionType.UTG2, PositionType.MP, PositionType.MP2, PositionType.MP3, PositionType.CO } },
            {10, new List<PositionType>() {PositionType.B, PositionType.SB, PositionType.BB, PositionType.UTG, PositionType.UTG2, PositionType.UTG3, PositionType.MP, PositionType.MP2, PositionType.MP3, PositionType.CO } }
        };
        protected const string NameOfSystem = "System";

        protected static readonly Regex GameSplitRegex = new Regex("(?:\r\n\r\n)|(?:\n{3,})", RegexOptions.Compiled);

        protected abstract byte StartPlayerRow { get; }

        protected abstract bool IsTournament { get; }

        protected abstract NumberFormatInfo Format { get; }
        
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
                game.PlayerHistories = ParsePlayers(game, multipleLines);
                IEnumerable<HandAction> allHandActions = ParseHandActions(game, multipleLines).ToArray();
                foreach (var playerHistory in game.PlayerHistories)
                {
                    var playerActions = allHandActions.Where(ha => ha.PlayerName == playerHistory.PlayerName).ToArray();
                    playerHistory.HandActions.AddRange(playerActions);
                }
                games.Add(game);
            }
            return games;
        }

        public abstract IDictionary<string, string> GetInfoFromPath(string path);

        public virtual  List<PlayerHistory> ParsePlayers(Game game, IReadOnlyList<string> multipleLines)
        {
            var players = new List<PlayerHistory>();
            for (var i = 0; i < game.NumberOfPlayers; i++)
            {
                string oneLine = multipleLines[StartPlayerRow + i];
                var player = new PlayerHistory
                {
                    GameNumber = game.GameNumber,
                    PlayerName = FindPlayerName(oneLine),
                    SeatNumber = FindSeatNumber(oneLine),
                    StartingStack = FindPlayerStartingStack(oneLine),
                    HoleCards = new byte[2],
                    HandActions = new List<HandAction>(),
                    Game = game
                };
                players.Add(player);
            }
            InitializePositionsOfPlayers(players,game.ButtonPosition);
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
                    throw new ParserException($"Unnown limit type -> {gameTypeString.ToLower()}",DateTime.Now);
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
            var s = value.Replace("$", "").Trim();
            s = s.Replace(((char)160).ToString(), "");//удалить все неразрывные пробелы nbsp (разделяет разряды)
            if (!s.Contains("+"))
                return double.Parse(s,Format);
            var parts = s.Split('+');
            return parts.Sum(part => double.Parse(part, Format));
        }

        
        private static IEnumerable<string> SplitAllTextInMultipleGames(string allHandsText)
        {
            return GameSplitRegex.Split(allHandsText)//разделить входную строку по шаблону регулярного выраж.
                            .Where(s => !String.IsNullOrWhiteSpace(s))//откинуть пустые строки
                            .Select(s => s.Trim('\r', '\n')).ToList();//удалить начальные и конечные управляющие символи
        }

        private static List<string> SplitOneGameTextInMultipleLines(string gameText)
        {
            var text = gameText.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < text.Length; i++)
            {
                text[i] = text[i].Trim();
            }
            return text.ToList();
        }

        private  void InitializePositionsOfPlayers(List<PlayerHistory> players, byte buttonPosition)
        {
            var buttonIndex = DefineButtonIndex(players, buttonPosition);
            if(buttonIndex==-1)
                throw new ParserException("Can't define button index");
            var count = (byte)players.Count;
            if(count<2||count>10)
                    throw new ParserException($"Wrong players count:{players.Count}",DateTime.Now);
            List<PositionType> positions = _positionsDictionary[count];
            if(count!=positions.Count)
                throw new ParserException("Players count and positions amount must be equal.", DateTime.Now);
            for (var i = 0; i < count; i++)
            {
                if(i + buttonIndex>count-1)
                    players[buttonIndex + i - count].Position = positions[i];
                else
                    players[i + buttonIndex].Position = positions[i];
            }
        }

        private int DefineButtonIndex(List<PlayerHistory> players, int buttonPosition)
        {
            var buttonPlayer = players.Find(p => p.SeatNumber == buttonPosition);
            var index =  players.IndexOf(buttonPlayer);
            if (index != -1)
                return index;
            var minSeat = players.Select(p => p.SeatNumber).Min();
            if (buttonPosition > minSeat)
            {
                var nearestPlayer = players.Last(p => p.SeatNumber < buttonPosition);
                return players.IndexOf(nearestPlayer);
            }
            else
            {
                return players.Count - 1;
            }
        }
    }
}
