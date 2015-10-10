using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using HandHistories.Parser.MoneyMaker.Configuration;
using HandHistories.Parser.MoneyMaker.EntityFramework;
using HandHistories.SimpleObjects.Entities;

namespace HandHistories.Parser.MoneyMaker
{
    public partial class MmForm : Form
    {
        //files monitoring...
        private FileSystemWatcher _watcher;
        private delegate void UpdateWatchTextDelegate(string newText);
        private readonly string _handHistoriesFolder;
        private HandHistoryContext _context;

        private FormSettings _formSettings;

        public MmForm()
        {
            InitializeComponent();
            _handHistoriesFolder = SettingsConfig.GetConfig("HandHistoryFolder");
            _watcher=new FileSystemWatcher();
            _watcher.Changed += OnChanged;
            _watcher.Created += OnCreated;
            _context=new HandHistoryContext();
            _context.Database.Initialize(false);

            _formSettings=new FormSettings();
            
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            BeginInvoke(new UpdateWatchTextDelegate(UpdateStatistics), string.Format("File:{0} created", e.FullPath));
        }
        
        //todo:updating statistics
        private void UpdateStatistics(string newtext)
        {
            
            ShowFileChanging(newtext);
            dataGrdVwHud.Update();
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            BeginInvoke(new UpdateWatchTextDelegate(ShowFileChanging), string.Format("File:{0} {1} ", e.FullPath, e.ChangeType));
        }

        private void MmForm_Load(object sender, EventArgs e)
        {
            InitializeWatching();
        }

        private void InitializeWatching()
        {
            _watcher.Path = _handHistoriesFolder;
            _watcher.Filter = "*.txt";
            _watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName;
            _watcher.EnableRaisingEvents = true;
        }

        private void ShowFileChanging(string text)
        {
            lblInfo.Text += text;
            lblInfo.Text += "\n";
        }

        //todo:not working...
        private void reinitializeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _context.Database.Initialize(true);
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _formSettings.ShowDialog();
        }

        

    }
}
