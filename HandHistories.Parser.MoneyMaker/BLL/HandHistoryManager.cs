using System;
using System.Collections.Generic;
using System.Linq;
using HandHistories.Parser.MoneyMaker.EntityFramework;
using HandHistories.SimpleObjects.Entities;

namespace HandHistories.Parser.MoneyMaker.BLL
{

    /// <summary>
    /// Ф:MmForm не должен выполнять никакой работы по извлечению и кэшированию данных из контекста. Для этого существует этот класс.
    /// </summary>
    public  class HandHistoryManager:IRepositoryManager
    {
        private HandHistoryContext _context;

        public HandHistoryManager(HandHistoryContext context)
        {
            _context = context;
            _context.Database.Initialize(false);
        }

        public IEnumerable<string> OponentNames
        {
            get { return _context.PlayerHistories.Select(p => p.PlayerName).Distinct(); }
        }

        //todo:need to refactor and has grouping bag
        public IEnumerable<PlayerSummary> GetPlayerSummariesByName(string name)
        {
            if(!OponentNames.Contains(name))
                throw new Exception(string.Format("Player with name {0} is not found!", name));
            var playerGames = GetPlayerGames(name);//* 2 qs
            var limits = GetPlayerLimits(playerGames);//* 1qs
            foreach (var limit in limits)
            {
                SeatType l = limit;
                var limitPlayerGames = playerGames.Where(g => g.SeatType == l).ToList();
                var handsWon = _context.HandActions.Count(ha => ha.PlayerName == name 
                    && (ha.HandActionType==HandActionType.WINS || ha.HandActionType==HandActionType.WINS_SIDE_POT));

                var sessionGroups = from lg in limitPlayerGames
                    group lg by new {lg.TableName, lg.DateOfHand.Date};

                var playerSummary = new PlayerSummary
                {
                    Name = name,
                    Limit = limit,
                    Hands = limitPlayerGames.Count(),
                    HandsWon = handsWon,
                    Sessions = sessionGroups.Count()
                };
                playerSummary.HandsWonPercent =
                    decimal.Round((decimal) playerSummary.HandsWon/(decimal) playerSummary.Hands*100, 2);
                yield return playerSummary;
            }
        }

        //todo:need to refactor
        public IEnumerable<PlayerStatistics> GetPlayerStatisticsByName(string name)
        {
            if (!OponentNames.Contains(name))
                throw new Exception(string.Format("Player with name {0} is not found!", name));
            var playerGames = GetPlayerGames(name).ToList();
            var limits = GetPlayerLimits(playerGames);
            foreach (var l in limits)
            {
                SeatType limit = l;
                var limitGames = playerGames.Where(g => g.SeatType == limit).ToList();
                var limitGameCount = limitGames.Count();
                var valPutCount = limitGames.Count(g => g.HandActions.Any(ha => ha.PlayerName == name 
                    && ha.Street == Street.Preflop 
                    && ha.HandActionType != HandActionType.BIG_BLIND 
                    && ha.HandActionType != HandActionType.SMALL_BLIND 
                    && (ha.HandActionType == HandActionType.CALL || ha.HandActionType == HandActionType.RAISE)));

                var preflopRaiseCount = limitGames.Count(g => g.HandActions.Any(ha => ha.PlayerName == name 
                    && ha.Street==Street.Preflop && ha.HandActionType==HandActionType.RAISE));

                var atsPercent = CalculateATSPercent(limitGames, name);
                var afpfPercent = CalculateAFPercent(limitGames, name);
                var playerStatistics = new PlayerStatistics
                {
                    Name = name,
                    Limit = l,
                    VPIP = decimal.Round((decimal)valPutCount/(decimal)limitGameCount*100,2),
                    PFR = decimal.Round((decimal)preflopRaiseCount / (decimal)limitGameCount * 100, 2),
                    ATS = decimal.Round((decimal)atsPercent, 2),
                    AF = decimal.Round((decimal)afpfPercent, 2)
                };
                yield return playerStatistics;
            }
        }

        private decimal CalculateAFPercent(IEnumerable<Game> games, string name)
        {
            var passiveActCount = 0;
            var agrassiveActCount = 0;
            foreach (var game in games)
            {
                foreach (var action in game.HandActions.Where(ha=> ha.PlayerName==name
                    && ha.Street!=Street.Preflop && ha.Street!=Street.Showdown && ha.Street!=Street.Null))
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

        //todo:need to refactor
        private decimal CalculateATSPercent(IEnumerable<Game> games, string playerName)
        {
            var atsSituationCount = 0;
            var atsRaiseCount = 0;
            foreach (var g in games)
            {
                byte buttonPosition = g.ButtonPosition;
                byte cutofPosition = DefineCutofPosition(g);
                //сначала проверяем, на какой позиции находится игрок - нас интересует CO и BTN
                byte playerSeatNumber = g.PlayerHistories.Find(p => p.PlayerName == playerName).SeatNumber;
                if(!(playerSeatNumber==buttonPosition||playerSeatNumber==cutofPosition))
                    continue;
                //кэшируем все действия игроков на префлопе
                var allPlayersPreflopHandActions = g.HandActions.Where(ha => !string.IsNullOrEmpty(ha.PlayerName) && ha.Street==Street.Preflop).ToList();
                //действия игрока
                var playerPreflopHandActions = allPlayersPreflopHandActions.Where(ha => ha.PlayerName == playerName);
                //действия всех остальных игроков до флопа кроме блайндов
                var otherPlayersPreflopActions = allPlayersPreflopHandActions.Where(ha=>ha.PlayerName!=playerName
                    && ha.HandActionType != HandActionType.BIG_BLIND
                    && ha.HandActionType != HandActionType.SMALL_BLIND);
                //если кто-то из них сказал кол или рейз, это не стиллингова ситуация
                if (otherPlayersPreflopActions.Any(ha => ha.HandActionType == HandActionType.CALL || ha.HandActionType == HandActionType.RAISE))
                    continue;
                //здесь стилинговая ситуация
                atsSituationCount++;
                if(playerPreflopHandActions.Any(ha => ha.HandActionType==HandActionType.RAISE))
                atsRaiseCount++;//возможность стила и игрок сделал рейз
            }
            return atsSituationCount == 0 ? 0 : (decimal)atsRaiseCount / (decimal)atsSituationCount * 100;
        }


        //Ф:Вся сложность в том, что в истории рук Poker888 за столами 9max позиции нумеруются от 1 до 10, а не от 1 до 9. Просто пропускается из
        //неизвесных мне причин, например восьмая позиция. Поетому алгоритм метода слегка упрощен.
        private byte DefineCutofPosition(Game game)
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

        private byte NearestInArrayValue(List<byte> positions, byte buttonPosition)
        {
            var initialPosition = buttonPosition;
            while (positions.IndexOf(initialPosition)!=-1)
            {
                if (initialPosition > 0)
                    initialPosition--;
                else
                {
                    initialPosition = positions[positions.Count()-1];
                }
            }
            return initialPosition;
        }

        private static IEnumerable<SeatType> GetPlayerLimits(IEnumerable<Game> playerGames)
        {
            //извлекаем неповторяющийся лимиты
            return playerGames.Where(g => g.SeatType != SeatType.Unknown).Select(g => g.SeatType).Distinct().ToList();
        }

        private List<Game> GetPlayerGames(string name)
        {
            //извлекаем номера рук, в которых участвовал игрок
            var gameNumbers = _context.PlayerHistories.Where(ph => ph.PlayerName == name).Select(ph => ph.GameNumber).Distinct();
            //извлекаем граф игр(рук) по найденым номерам и 
            return _context.Games.Where(g => gameNumbers.Contains(g.GameNumber)).ToList();
        }
    }
}
