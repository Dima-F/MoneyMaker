using System.Collections.Generic;
using HandHistories.SimpleObjects.Entities;

namespace HandHistories.SimpleParser
{
    /// <summary>
    /// Ф:Интерфейс задает поведение парсинга игр, историй игроков и действий.
    /// </summary>
    public interface IHandHistoryParser 
    {
        List<Game> ParseGames(string allHandsText);
        List<PlayerHistory> ParsePlayers(string allHandsText);
        List<HandAction> ParseHandActions(string allHandsText);
    }
}
