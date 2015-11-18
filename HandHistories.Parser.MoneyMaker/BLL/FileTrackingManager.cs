using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandHistories.Parser.MoneyMaker.Configuration;

namespace HandHistories.Parser.MoneyMaker.BLL
{
    public delegate void FileTrackingDelegate(string path);
    /// <summary>
    /// Ф:Обертка над FileSystemWatcher
    /// Пояснения до событийной модели:менеджер ловит событие FileSystemWatcher по изменению файла в директории
    /// и в обработчике генерирует свое в том случае, если имеется хотябы один подписчик.
    /// </summary>
    public class FileTrackingManager
    {
        //files monitoring...
        private FileSystemWatcher _watcher;
        private string _folderPath;

        public FileTrackingManager(string folderPath):this()
        {
            _folderPath = folderPath;
        }

        public FileTrackingManager()
        {
            _folderPath = SettingsConfig.GetConfig("FileTrackingFolder");
            _watcher = new FileSystemWatcher();
            _watcher.Changed += OnChanged;
        }

        public event FileSystemEventHandler PokerFileChanged;

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            if (PokerFileChanged != null)
                PokerFileChanged(sender, e);
        }

        //example:filter="*.txt"  notifyFilters=NotifyFilters.LastWrite | NotifyFilters.FileName
        public void Initialize(string filter, NotifyFilters notifyFilters)
        {
            _watcher.Path = _folderPath;
            _watcher.Filter = "*.txt";
            _watcher.NotifyFilter = notifyFilters;
        }

        public bool EnableTracking
        {
            set { _watcher.EnableRaisingEvents = value; }
        }



    }
}
