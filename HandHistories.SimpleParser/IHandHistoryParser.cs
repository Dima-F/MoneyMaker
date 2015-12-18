using System.Collections.Generic;
using HandHistories.SimpleObjects.Entities;

namespace HandHistories.SimpleParser
{
    /// <summary>
    /// Ф:Интерфейс парсинга игр (рук). Прим: парсинг игроков и типов действий агрегирован в 1 общий метод.
    /// </summary>
    public interface IHandHistoryParser 
    {
        List<Game> ParseGames(string allHandsText);
    }
}
