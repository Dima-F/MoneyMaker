using System.Collections.Generic;
using System.Linq;
using HandHistories.SimpleObjects.Entities;
using HandHistories.SimpleObjects.Tools;
using MoneyMaker.BLL.ViewEntities;

namespace MoneyMaker.BLL.Tools
{
    public static class GameExtentions
    {
        public static IEnumerable<string> GetDistinctPlayerNames(this IEnumerable<PlayerHistory> playerHistories)
        {
            return playerHistories.Select(p => p.PlayerName).Distinct();
        }

        public static IEnumerable<string> GetLivePlayerNames(this IEnumerable<Game> games)
        {
            var lastGame = games.Last();
            return lastGame.PlayerHistories.Where(ph => ph.GameNumber == lastGame.GameNumber).Select(p => p.PlayerName).Distinct();
        }

        public static IEnumerable<string> GetLivePlayerNames(this IEnumerable<Game> games, IEnumerable<PlayerHistory> playerHistories)
        {
            var lastGame = games.Last();
            return playerHistories.Where(ph => ph.GameNumber == lastGame.GameNumber).Select(p => p.PlayerName).Distinct();
        }

        public static IEnumerable<SeatType> GetDistinctLimits(this IEnumerable<Game> games)
        {
            return games.Where(g => g.SeatType != SeatType.Unknown).Select(g => g.SeatType).Distinct();
        }

        public static IEnumerable<Game> GetGamesForPlayer(this IEnumerable<Game> games, string player)
        {
            return games.Where(game => game.HandActions.Any(ha => ha.Source == player));
        }

        public static IEnumerable<Game> GetGamesForLimit(this IEnumerable<Game> games, SeatType seatType)
        {
            return games.Where(game => game.SeatType == seatType);
        }

        public static int GetHandsWonCountForPlayer(this IEnumerable<HandAction> actions, string player)
        {
            return actions.Count(ha => ha.Source == player
                    && (ha.HandActionType == HandActionType.WINS || ha.HandActionType == HandActionType.WINS_SIDE_POT));
        }

        public static int VPIPCountForPlayer(this IEnumerable<Game> games, string player)
        {
            return games.Count(g => g.HandActions.Any(ha => ha.Source == player
                    && ha.Street == Street.Preflop
                    && ha.HandActionType != HandActionType.BIG_BLIND
                    && ha.HandActionType != HandActionType.SMALL_BLIND
                    && (ha.HandActionType == HandActionType.CALL || ha.HandActionType == HandActionType.RAISE)));
        }

        public static int PFRCountForPlayer(this IEnumerable<Game> games, string player)
        {
            return games.Count(g => g.HandActions.Any(ha => ha.Source == player
                                                            && ha.Street == Street.Preflop
                                                            && ha.HandActionType == HandActionType.RAISE));
        }

        public static decimal GetAFPercentForPlayer(this IEnumerable<Game> games, string player)
        {
            var passiveActCount = 0;
            var agrassiveActCount = 0;
            foreach (var game in games)
            {
                foreach (var action in game.HandActions.Where(ha => ha.Source == player
                    && ha.Street != Street.Preflop && ha.Street != Street.Showdown && ha.Street != Street.Null))
                {
                    if (action.HandActionType == HandActionType.CALL
                        || action.HandActionType == HandActionType.CHECK)
                        passiveActCount++;
                    if (action.HandActionType == HandActionType.BET
                        || action.HandActionType == HandActionType.RAISE)
                        agrassiveActCount++;
                }
            }
            return passiveActCount == 0 ? (decimal)agrassiveActCount : (decimal)agrassiveActCount / (decimal)passiveActCount;
        }

        public static decimal GetATSPercentForPlayer(this IEnumerable<Game> games, string player)
        {
            var atsSituationCount = 0;
            var atsRaiseCount = 0;
            foreach (var g in games)
            {
                byte buttonPosition = g.ButtonPosition;
                byte cutofPosition = DefineCutofPosition(g);
                //сначала проверяем, на какой позиции находится игрок - нас интересует CO и BTN
                byte playerSeatNumber = g.PlayerHistories.Find(p => p.PlayerName == player).SeatNumber;
                if (!(playerSeatNumber == buttonPosition || playerSeatNumber == cutofPosition))
                    continue;
                //кэшируем все действия игроков на префлопе
                var allPlayersPreflopHandActions = g.HandActions.Where(ha => !string.IsNullOrEmpty(ha.Source) && ha.Street == Street.Preflop).ToList();
                //действия игрока
                var playerPreflopHandActions = allPlayersPreflopHandActions.Where(ha => ha.Source == player);
                //действия всех остальных игроков до флопа кроме блайндов
                var otherPlayersPreflopActions = allPlayersPreflopHandActions.Where(ha => ha.Source != player
                    && ha.HandActionType != HandActionType.BIG_BLIND
                    && ha.HandActionType != HandActionType.SMALL_BLIND);
                //если кто-то из них сказал кол или рейз, это не стиллингова ситуация
                if (otherPlayersPreflopActions.Any(ha => ha.HandActionType == HandActionType.CALL || ha.HandActionType == HandActionType.RAISE))
                    continue;
                //здесь стилинговая ситуация
                atsSituationCount++;
                if (playerPreflopHandActions.Any(ha => ha.HandActionType == HandActionType.RAISE))
                    atsRaiseCount++;//возможность стила и игрок сделал рейз
            }
            return atsSituationCount == 0 ? 0 : (decimal)atsRaiseCount / (decimal)atsSituationCount * 100;
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
        
        //mucking
        public static bool WasMucking(this Game game)
        {
            return game.HandActions.Find(ha => ha.HandActionType == HandActionType.MUCKS) != null;
        }

        public static Muck GetMuck(this Game game)
        {
            var player = game.HandActions.Find(ha => ha.HandActionType == HandActionType.MUCKS).Source;
            var cards = game.PlayerHistories.Find(ph => ph.PlayerName == player).HoleCards.ConvertByteCardsToString();
            var muck = new Muck() {PlayerName = player, Cards = cards};
            return muck;
        }

        public static decimal CalculateTotalProfit(this IEnumerable<Game> games, string player)
        {
            return games.SelectMany(game => game.HandActions.Where(ha => ha.Source == player)).Sum(ha => ha.Amount);
        }

        public static decimal CalculateProfit(this Game game, string player)
        {
            return game.HandActions.Where(ha => ha.Source == player).Sum(ha => ha.Amount);
        }

        public static decimal CalculateHeroProfit(this Game game)
        {
            return game.HandActions.Where(ha => ha.Source == game.Hero).Sum(ha => ha.Amount);
        }


        //Ф:Вся сложность в том, что в истории рук Poker888 за столами 9max позиции нумеруются от 1 до 10, а не от 1 до 9. Просто пропускается из
        //неизвесных мне причин, например восьмая позиция. Поетому алгоритм метода слегка упрощен.
        private static byte DefineCutofPosition(Game game)
        {
            byte buttonPosition = game.ButtonPosition;
            //позиции, на которых сидят игроки
            var positions = game.PlayerHistories.Select(player => player.SeatNumber).ToList();
            var index = positions.IndexOf(buttonPosition);
            if (index != -1)
            {
                //игрок сидит на батоне
                return index > 0 ? positions[index - 1] : positions.Last();
            }
            //игрок не сидит на батоне
            return NearestInArrayValue(positions, buttonPosition);
        }

        private static byte NearestInArrayValue(List<byte> positions, byte buttonPosition)
        {
            var initialPosition = buttonPosition;
            while (positions.IndexOf(initialPosition) != -1)
            {
                if (initialPosition > 0)
                    initialPosition--;
                else
                {
                    initialPosition = positions[positions.Count() - 1];
                }
            }
            return initialPosition;
        }
    }
}
