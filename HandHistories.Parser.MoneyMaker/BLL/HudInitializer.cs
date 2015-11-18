using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandHistories.SimpleObjects.Entities;
using HandHistories.SimpleParser;

namespace HandHistories.Parser.MoneyMaker.BLL
{
    /// <summary>
    /// Ф:Этот класс принимает в качестве параметра конструктору путь к отслеживаемому файлу. Он должен всякий раз при изменении этого
    /// файла проводить парсинг всех игр заново, без сохранения в БД,
    /// </summary>
    public class HudInitializer
    {
        private IHandHistoryParser _innerParser;
        private string _path;

        public HudInitializer(IHandHistoryParser innerParser, string path)
        {
            _innerParser = innerParser;
            _path = path;
        }

        public List<IEnumerable<PlayerStatistics>> ParsePlayersStatistics()
        {
            string fileText = ReadFile(_path);
            List<Game> games = _innerParser.ParseGames(fileText);
            List<PlayerHistory> playerHistories = _innerParser.ParsePlayers(fileText);

        }

        private string ReadFile(string path)
        {
            throw new NotImplementedException();
        }
    }
}
