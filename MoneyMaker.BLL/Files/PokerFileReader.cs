using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

namespace MoneyMaker.BLL.Files
{
    public static class PokerFileReader
    {
        /// <summary>
        /// Ф:Делает попытку чтения файла. Если файл занят, ждет освобождения и повторяет.
        /// </summary>
        public static string ReadFileWithWaiting(string path)
        {
            var file = new FileInfo(path);
            var builder = new StringBuilder();
            FileStream fs;
            while (true)
            {
                try
                {
                    fs = file.Open(FileMode.Open, FileAccess.ReadWrite);
                    break;
                }
                catch (IOException exception)
                {
                    Debug.WriteLine(exception.Message);
                    Thread.Sleep(1000);
                }
            }
            using (var reader = new StreamReader(fs, Encoding.Default))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    builder.AppendLine(line);
                }
            }
            fs.Close();
            return builder.ToString();
        }
    }
}
