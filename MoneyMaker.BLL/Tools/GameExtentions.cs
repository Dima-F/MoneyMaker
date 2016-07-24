using System;
using System.Collections.Generic;
using System.Linq;
using HandHistories.SimpleObjects.Entities;
using HandHistories.SimpleObjects.Tools;
using MoneyMaker.BLL.Hud;

namespace MoneyMaker.BLL.Tools
{
    public static class GameExtentions
    {
        public static IEnumerable<string> GetDistinctPlayerNames(this IEnumerable<PlayerHistory> playerHistories)
        {
            return playerHistories.Select(p => p.PlayerName).Distinct();
        }
        
        public static IEnumerable<string> GetLivePlayerNames(this IEnumerable<Game> games, int lastGamesCount)
        {
            var enumerable = games as Game[] ?? games.ToArray();
            var gamesCount = enumerable.Length;
            var last3Games = enumerable.Skip(gamesCount - lastGamesCount);
            var playerHistories = new List<PlayerHistory>();
            foreach (var g in last3Games)
            {
                playerHistories.AddRange(g.PlayerHistories);
            }
            return playerHistories.Select(p => p.PlayerName).Distinct();
        }

        public static IEnumerable<Game> GetLive(this IEnumerable<Game> games, int lastHours)
        {
            return games.Where(g => DateTime.Now - g.DateOfHand < TimeSpan.FromHours(lastHours));
        }
        
        public static IEnumerable<string> GetAllPlayerNames(this IEnumerable<Game> games)
        {
            var playerHistories = new List<PlayerHistory>();
            foreach (var g in games)
            {
                playerHistories.AddRange(g.PlayerHistories);
            }
            return playerHistories.Select(p => p.PlayerName).Distinct();
        }

        public static IEnumerable<SeatType> GetDistinctLimits(this IEnumerable<Game> games)
        {
            return games.Where(g => g.SeatType != SeatType.Unknown).Select(g => g.SeatType).Distinct();
        }

        public static IEnumerable<Game> GetGamesForPlayer(this IEnumerable<Game> games, string player)
        {
            return games.Where(game => game.PlayerHistories.Any(ph =>ph.PlayerName == player));
        }
        
        public static double GetHandsWonPercentForPlayerGames(this IEnumerable<Game> games, string player)
        {
            var pgs = games.ToArray();
            var gamesCount = pgs.Count();
            var wonCount = pgs.Count(g => g.IsWinGameForPlayer(player));
            return gamesCount==0 ? 0 : (double)wonCount / gamesCount * 100;
        }

        public static List<HandAction> AllPreflopHandActions(this Game game)
        {
            var has=new List<HandAction>();
            foreach (var playerHistory in game.PlayerHistories)
            {
                has.AddRange(playerHistory.HandActions.Where(ha => ha.Street == Street.Preflop));
            }
            return has;
        }

        public static List<HandAction> AllFlopHandActions(this Game game)
        {
            var has = new List<HandAction>();
            foreach (var playerHistory in game.PlayerHistories)
            {
                has.AddRange(playerHistory.HandActions.Where(ha => ha.Street == Street.Flop));
            }
            return has;
        }

        public static List<HandAction> AllTurnHandActions(this Game game)
        {
            var has = new List<HandAction>();
            foreach (var playerHistory in game.PlayerHistories)
            {
                has.AddRange(playerHistory.HandActions.Where(ha => ha.Street == Street.Turn));
            }
            return has;
        }

        public static List<HandAction> AllRiverHandActions(this Game game)
        {
            var has = new List<HandAction>();
            foreach (var playerHistory in game.PlayerHistories)
            {
                has.AddRange(playerHistory.HandActions.Where(ha => ha.Street == Street.River));
            }
            return has;
        }

        public static bool SawFlopForPlayer(this Game game, string player)
        {
            var ph = game.PlayerHistories.First(p => p.PlayerName == player);
            return
                ph.HandActions.RealActions()
                    .Any(ha => ha.Street == Street.Flop || ha.HandActionType == HandActionType.SHOW);
        }

        public static bool IsWinGameForPlayer(this Game game, string player)
        {
            var ph = game.PlayerHistories.First(p => p.PlayerName == player);
            return ph.HandActions.Any(ha => ha.IsWinAction());
        }

        public static PlayerHistory GetPlayerHistoryForAction(this Game game, HandAction ha)
        {
            return game.PlayerHistories.FirstOrDefault(ph => ph.PlayerName== ha.PlayerName);
        }
        
        /// <summary>
        /// Ф:Следует проверять возвращенное значение на null
        /// </summary>
        public static byte[] GetHeroCardsFromLastGame(this IEnumerable<Game> games)
        {
            var lastGame = games.Last();
            var hero = lastGame.PlayerHistories.FirstOrDefault(ph => ph.PlayerName == lastGame.Hero);
            return hero != null ? hero.HoleCards : null;
        }
        
        public static Muck GetMuck(this Game game)
        {
            foreach (var playerHistory in game.PlayerHistories)
            {
                foreach (var handAction in playerHistory.HandActions)
                {
                    if (handAction.HandActionType == HandActionType.MUCKS)
                    {
                        var cards = playerHistory.HoleCards.ConvertByteCardsToString();
                        return new Muck {PlayerName = playerHistory.PlayerName, Cards = cards};
                    }
                }
            }
            return null;
        }
        
        public static double CalculateTotalProfit(this IEnumerable<Game> games, string player)
        {
            double sum = 0;
            foreach (Game g in games)
            {
                var playerHistory = g.PlayerHistories.First(ph => ph.PlayerName == player);
                sum += playerHistory.HandActions.Sum(ha => ha.Amount);
            }
            return sum;
        }

        public static double CalculateHeroProfit(this Game game)
        {
            double sum = 0;
            var playerHistory = game.PlayerHistories.Find(ph => ph.PlayerName == game.Hero);
            if (playerHistory == null) return 0;
            sum += playerHistory.HandActions.Sum(ha => ha.Amount);
            return sum;
        }

        /// <summary>
        /// Ф:Проверяет ситуацию, когда кто-то сделал рейз из позней позиции в неоткрытом банке
        /// </summary>
        public static bool WasStealToPlayer (this Game game, PlayerHistory ph)
        {
            var playersInStealSituations =
                game.PlayerHistories.Where(p => p.PlayerName != ph.PlayerName && game.IsStealSituationForPlayer(p));
            return playersInStealSituations.Any(p=>p.HandActions.RealActions().First().IsRaiseAction());
        }

        /// <summary>
        /// Ф:Проверяет стиллинговую ситуацию для игрока
        /// </summary>
        public static bool IsStealSituationForPlayer(this Game game, PlayerHistory playerHistory)
        {
            var playersCount = game.PlayerHistories.Count;
            if (playersCount < 3)
                return false;
            if (!playerHistory.HandActions.PreflopHandActions().RealActions().Any())
                return false;
            if (!playerHistory.IsInStealPosition())
                return false;
            var allPreflopHandActions = game.AllPreflopHandActions().RealActions().ToList();
            var positionAction = allPreflopHandActions.Where(ha => ha.PlayerName == playerHistory.PlayerName).OrderBy(ha => ha.Index).First();

            if (!allPreflopHandActions.AllFoldedBeforeAction(positionAction))
                return false;
            return true;
        }
        
    }
}
