using System.Collections.Generic;
using HandHistories.SimpleObjects.Entities;

namespace HandHistories.SimpleParser
{
    /// <summary>
    /// Ф:Интерфейс парсинга разширеного класса истории рук
    /// </summary>
    public interface IHandHistoryParser 
    {
        List<Game> ParseGames(string allHandsText);
        List<PlayerHistory> ParsePlayers(string allHandsText);
        List<HandAction> ParseHandActions(string allHandsText);
    }
}
