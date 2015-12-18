using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using HandHistories.SimpleObjects.Entities;

namespace HandHistories.SimpleParser
{
    /// <summary>
    /// Ф:Реализация класса парсинга истории рук для кэш игры покеррума Poker888
    /// </summary>
    public class Poker888CashHandHistoryParser:IHandHistoryParser
    {
        private static readonly Regex GameSplitRegex = new Regex("(?:\r\n\r\n)|(?:\n\n\n)", RegexOptions.Compiled);

        public List<Game> ParseGames(string allHandsText)
        {
            var games=new List<Game>();
            IEnumerable<string> multipleGames = SplitAllTextInMultipleGames(allHandsText);
            foreach (var concreteGame in multipleGames)
            {
                if(concreteGame.FindBadHand())
                    continue;
                var game = new Game {BoardCards = new byte[5]};
                var multipleLines = SplitOneGameTextInMultipleLines(concreteGame);
                game.GameNumber = multipleLines[0].FindGameNumber();
                game.HandText = concreteGame;
                game.DateOfHand = multipleLines[2].FindDateOfGame();
                game.TableName = multipleLines[3].FindTableName();
                game.GameType = multipleLines[2].FindGameType();
                game.SeatType = multipleLines[3].FindSeatType();
                game.ButtonPosition = multipleLines[4].FindButtonPosition();
                game.NumberOfPlayers = multipleLines[5].FindNumberOfPlayers();
                game.PlayerHistories = ParsePlayers(game.GameNumber, game.NumberOfPlayers, multipleLines);
                game.HandActions = ParseHandActions(game, multipleLines).ToList();
                games.Add(game);
            }
            return games;
        }

        #region MethodWorkers

        private IEnumerable<HandAction> ParseHandActions(Game game, IReadOnlyList<string> multipleLines)
        {
            var handActions = new List<HandAction>();
            var currentStreet = Street.Preflop;
            for (var i = 6 + game.NumberOfPlayers; i < multipleLines.Count; i++)
            {
                var ha = new HandAction {GameNumber = game.GameNumber};
                if (multipleLines[i].StartsWith("**"))
                {
                    if (multipleLines[i].ToLower().Contains("flop"))
                    {
                        ha.HandActionType = HandActionType.DEALING_FLOP;
                        game.BoardCards.InitializeNewCards(multipleLines[i].FindFlopCards());
                        handActions.Add(ha);
                        currentStreet = Street.Flop;
                        continue;
                    }
                    if (multipleLines[i].ToLower().Contains("turn"))
                    {
                        ha.HandActionType = HandActionType.DEALING_TURN;
                        game.BoardCards[3]=multipleLines[i].FindTurnCard();
                        handActions.Add(ha);
                        currentStreet = Street.Turn;
                        continue;
                    }
                    if (multipleLines[i].ToLower().Contains("river"))
                    {
                        ha.HandActionType = HandActionType.DEALING_RIVER;
                        game.BoardCards[4] = multipleLines[i].FindRiverCard();
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
                        ha.HandActionType = HandActionType.SUMMARY;
                        handActions.Add(ha);
                        currentStreet = Street.Showdown;
                        continue;
                    }
                    throw new Exception("No defined action");
                }

                if (multipleLines[i].ToLower().StartsWith("dealt to"))
                {
                    ha.PlayerName = ha.PlayerName = multipleLines[i].Split(' ')[2].Trim();
                    ha.Street = currentStreet;
                    ha.HandActionType = HandActionType.DEALT_HERO_CARDS;
                    game.Hero = ha.PlayerName;
                    game.PlayerHistories.Find(p => p.PlayerName == ha.PlayerName).HoleCards.InitializeNewCards(multipleLines[i].FindHeroCards());
                    handActions.Add(ha);
                }


                else//line don't starts with ** and "dealt to"
                {
                    ha.PlayerName = multipleLines[i].Split(' ')[0].Trim();
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
                            ha.Amount = DefineActionAmount(ha,multipleLines[i]);
                            handActions.Add(ha);
                            continue;
                        case "bets":
                            ha.HandActionType = HandActionType.BET;
                            ha.Amount = DefineActionAmount(ha,multipleLines[i]);
                            handActions.Add(ha);
                            continue;
                        case "raises":
                            ha.HandActionType = HandActionType.RAISE;
                            ha.Amount = DefineActionAmount(ha,multipleLines[i]);
                            handActions.Add(ha);
                            continue;
                        case "shows":
                            ha.HandActionType = HandActionType.SHOW;
                            game.PlayerHistories.Find(p=>p.PlayerName==ha.PlayerName).HoleCards.InitializeNewCards(multipleLines[i].FindShowdounCards());
                            handActions.Add(ha);
                            continue;
                        case "mucks":
                            ha.HandActionType = HandActionType.MUCKS;
                            game.PlayerHistories.Find(p => p.PlayerName == ha.PlayerName).HoleCards.InitializeNewCards(multipleLines[i].FindMuckCards());
                            handActions.Add(ha);
                            continue;
                        case "collected":
                            ha.HandActionType = HandActionType.WINS;
                            ha.Amount = DefineActionAmount(ha,multipleLines[i]);
                            handActions.Add(ha);
                            continue;
                    }
                }
            }
            return handActions;
        }

        private List<PlayerHistory> ParsePlayers(int gameNumber, byte numberOfPlayers, IReadOnlyList<string> multipleLines)
        {
            var players=new List<PlayerHistory>();
            for (var i = 0; i < numberOfPlayers; i++)
            {
                string oneLine = multipleLines[6 + i];
                // Expected format:
                // Seat 1: josesoyXD ( 2,37$ )
                var player=new PlayerHistory
                {
                    GameNumber = gameNumber, 
                    PlayerName = oneLine.FindPlayerName(),
                    SeatNumber = oneLine.FindSeatNumber(),
                    StartingStack = oneLine.FindPlayerStartingStack(),
                    HoleCards = new byte[2]
                };
                players.Add(player);
            }
            return players;
        }

        private IEnumerable<string> SplitAllTextInMultipleGames(string allHandsText)
        {
            return GameSplitRegex.Split(allHandsText)//разделить входную строку по шаблону регулярного выраж.
                            .Where(s => !string.IsNullOrWhiteSpace(s))//откинуть пустые строки
                            .Select(s => s.Trim('\r', '\n')).ToList();//удалить начальные и конечные управляющие символи
        }

        private List<string> SplitOneGameTextInMultipleLines(string gameText)
        {
            var text = gameText.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < text.Length; i++)
            {
                text[i] = text[i].Trim();
            }
            return text.ToList();
        }

        /// <summary>
        /// Ф:Обворачивает DefineActionAmount() и добавляет знак (положительный и отрицательный) в зависимости от дохода
        /// </summary>
        private decimal DefineActionAmount(HandAction handAction, string inputLine)
        {
            var amount = inputLine.FindActionAmount();
            switch (handAction.HandActionType)
            {
                case HandActionType.BET:
                case HandActionType.BIG_BLIND:
                case HandActionType.SMALL_BLIND:
                case HandActionType.CALL:
                case HandActionType.ALL_IN:
                case HandActionType.ANTE:
                case HandActionType.RAISE:
                    amount = -amount;
                    break;
            }
            return amount;
        }

        #endregion

    }
}
