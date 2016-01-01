using System;
using System.Linq;

namespace MoneyMaker.BLL.Tools
{
    /// <summary>
    /// Ф:Парсит основную инфу сессии из имени файла
    /// </summary>
    public static class PathPoker888ParserExtention
    {
        public static string GetTableFromFileName(this string fileName)
        {
            return fileName.Split(' ')[1];
        }

        public static string GetLimitFromFileName(this string fileName)
        {
            string[] parts = fileName.Split(' ');
            var lastParts = parts.Skip(3).ToArray();
            return string.Join( " ",lastParts);
        }

        public static string GetBlindsFromFileName(this string fileName)
        {
            return fileName.Split(' ')[2];
        }

        public static DateTime GetDateFromFileName(this string fileName)
        {
            var roomDate = fileName.Split(' ')[0];
            var dateString = roomDate.Substring(roomDate.Length - 8);
            return DateTime.ParseExact(dateString, "yyyyMdd", null);
        }

        public static string GetRoomFromFileName(this string fileName)
        {
            var roomDate = fileName.Split(' ')[0];
            return roomDate.Substring(0, roomDate.Length - 8);
        }

    }
}
