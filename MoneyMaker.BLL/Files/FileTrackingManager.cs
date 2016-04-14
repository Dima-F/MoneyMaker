using System;
using System.IO;

namespace MoneyMaker.BLL.Files
{
    /// <summary>
    /// Ф:Обертка над FileSystemWatcher
    /// Пояснения до событийной модели:менеджер ловит событие FileSystemWatcher по изменению файла в директории
    /// и в обработчике генерирует свое в том случае, если имеется хотябы один подписчик.
    /// Плюс еще до того, FileTrackingManager реализует механизм устранения двойной генерации события FileSystemWatcher.
    /// </summary>
    public sealed class FileTrackingManager
    {
        private readonly FileSystemWatcher _watcher;

        private DateTime _lastRead;

        public FileTrackingManager()
        {
            _watcher = new FileSystemWatcher();
            _watcher.Changed += OnChanged;
            _lastRead = DateTime.MinValue;
        }

        public string FolderPath { get; private set; }

        public event FileSystemEventHandler PokerFileChanged;

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            if (PokerFileChanged == null) return;
            var lastWriteTime = File.GetLastWriteTime(e.FullPath);
            if (lastWriteTime == _lastRead) return;//double write filtering
            if (e.FullPath.ToLower().Contains("summary.txt"))//filtering summary files
                return;
            PokerFileChanged(sender, e);
            _lastRead = lastWriteTime;
        }

        //example:filter="*.txt"  notifyFilters=NotifyFilters.LastWrite | NotifyFilters.FileName
        public void Initialize(string filter, NotifyFilters notifyFilters, string path)
        {
            FolderPath = path;
            _watcher.Path = path;
            _watcher.Filter = "*.txt";
            _watcher.NotifyFilter = notifyFilters;
        }

        public bool EnableTracking
        {
            set { _watcher.EnableRaisingEvents = value; }
        }




    }
}
