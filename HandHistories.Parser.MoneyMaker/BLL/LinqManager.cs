using System;
using System.Collections.Generic;
using System.Linq;
using HandHistories.Parser.MoneyMaker.EntityFramework;
using HandHistories.Parser.MoneyMaker.Tools;
using HandHistories.SimpleObjects.Entities;

namespace HandHistories.Parser.MoneyMaker.BLL
{

    /// <summary>
    /// Ф:MmForm не должен выполнять никакой работы по извлечению и кэшированию данных из контекста. Для этого существует этот класс.
    /// </summary>
    public  class LinqManager:IRepositoryManager
    {
        private HandHistoryContext _context;

        public LinqManager(HandHistoryContext context)
        {
            _context = context;
            _context.Database.Initialize(false);
        }

        public IEnumerable<string> OponentNames
        {
            get { return _context.PlayerHistories.Select(p => p.PlayerName).Distinct(); }
        }

        public IEnumerable<PlayerSummary> GetPlayerSummariesByName(string name)
        {
            if(!OponentNames.Contains(name))
                throw new Exception(string.Format("Player with name {0} is not found!", name));
            var playerGames = PlayerGames(name);
            var limits = GetPlayerLimits(playerGames);
            foreach (var limit in limits)
            {
                var handsWon =
                    _context.HandActions.Count(ha => ha.PlayerName == name && (ha.HandActionType==HandActionType.WINS || ha.HandActionType==HandActionType.WINS_SIDE_POT));
                var playerSummary = new PlayerSummary
                {
                    Name = name,
                    Limit = limit,
                    Hands = playerGames.Count(g => g.SeatType==limit),
                    HandsWon = handsWon
                };
                playerSummary.HandsWonPercent =
                    decimal.Round((decimal) playerSummary.HandsWon/(decimal) playerSummary.Hands*100, 2);
                yield return playerSummary;
            }
        }

        public IEnumerable<PlayerStatistics> GetPlayerStatisticsByName(string name)
        {
            if (!OponentNames.Contains(name))
                throw new Exception(string.Format("Player with name {0} is not found!", name));
            var playerGames = PlayerGames(name);
            var limits = GetPlayerLimits(playerGames);
            foreach (var l in limits)
            {
                SeatType limit = l;
                var limitGames = playerGames.Where(g => g.SeatType == limit);
                var enumerable = limitGames as Game[] ?? limitGames.ToArray();
                var limitGameCount = enumerable.Count();
                var valPutCount = enumerable.Count(g => g.HandActions.Any(ha => ha.PlayerName == name && ha.Street == Street.Preflop && ha.HandActionType != HandActionType.BIG_BLIND && ha.HandActionType != HandActionType.SMALL_BLIND && (ha.HandActionType == HandActionType.CALL || ha.HandActionType == HandActionType.RAISE)));
                var playerStatistics = new PlayerStatistics()
                {
                    Name = name,
                    Limit = l,
                    VPIP = decimal.Round((decimal)valPutCount/(decimal)limitGameCount*100,2)
                };
                yield return playerStatistics;
            }
        }

        private static IEnumerable<SeatType> GetPlayerLimits(IEnumerable<Game> playerGames)
        {
            //извлекаем неповторяющийся лимиты
            return playerGames.Where(g => g.SeatType != SeatType.Unknown).Select(g => g.SeatType).Distinct().ToList();
        }

        private List<Game> PlayerGames(string name)
        {
            //извлекаем номера рук, в которых участвовал игрок
            var gameNumbers = _context.PlayerHistories.Where(ph => ph.PlayerName == name).Select(ph => ph.GameNumber).Distinct();
            //извлекаем граф игр(рук) по найденым номерам и 
            return _context.Games.Where(g => gameNumbers.Contains(g.GameNumber)).ToList();
        }

        private IEnumerable<object> GetOpponentsHandsCount()
        {
            foreach (var name in OponentNames.Where(name => !name.IsNullOrEmpty()))
            {
                int count = _context.PlayerHistories.Count(ph => ph.PlayerName == name);
                yield return new { name, count };
            }
        }
    }
}
