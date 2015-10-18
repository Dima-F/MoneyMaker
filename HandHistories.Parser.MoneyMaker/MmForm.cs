using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using HandHistories.Parser.MoneyMaker.Configuration;
using HandHistories.Parser.MoneyMaker.EntityFramework;
using HandHistories.Parser.MoneyMaker.Tools;
using HandHistories.SimpleObjects.Entities;

namespace HandHistories.Parser.MoneyMaker
{
    public partial class MmForm : Form
    {
        //forms...
        private FormSettings _formSettings;
        //files monitoring...
        private FileSystemWatcher _watcher;
        private delegate void UpdateWatchTextDelegate(string newText);

        private readonly string _handHistoriesFolder;
        private HandHistoryContext _context;
        private IEnumerable<string> _oponentNames; 
        
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

        #region Fields
        
        public IEnumerable<string> OponentNames
        {
            //lazy load pattern
            get 
            {
                return _oponentNames ?? (_oponentNames = _context.PlayerHistories.Select(p => p.PlayerName).Distinct());
            }
        }

        #endregion

        #region Method-handlers...

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            BeginInvoke(new UpdateWatchTextDelegate(UpdateStatistics), string.Format("File:{0} created", e.FullPath));
        }
        
        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            BeginInvoke(new UpdateWatchTextDelegate(ShowFileChanging), string.Format("File:{0} {1} ", e.FullPath, e.ChangeType));
        }

        private void MmForm_Load(object sender, EventArgs e)
        {
            InitializeWatching();
            LoadPlayersList();
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


        private void textBoxSearch_Enter(object sender, EventArgs e)
        {
            textBoxSearch.Text = "";
        }

        private void textBoxSearch_Leave(object sender, EventArgs e)
        {
            if(textBoxSearch.Text.Trim()=="")
                textBoxSearch.Text = "Search...";
        }

        private void nameList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var listBox = sender as ListBox;
            if(listBox==null)return;
            string name = listBox.Items[listBox.SelectedIndex].ToString();
            FillMainTab(name);
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox == null) return;
            var currentText = textBox.Text.Trim();
            FindMyString(currentText);
        }

        #endregion

        #region Method-workers...

        private void LoadPlayersList()
        {
            foreach (var oponent in OponentNames)
            {
                nameList.Items.Add(oponent);
            }
        }

        private void FindMyString(string searchString)
        {
            // Ensure we have a proper string to search for.
            if (searchString != string.Empty && searchString!="Search...")
            {
                // Find the item in the list and store the index to the item.
                int index = nameList.FindString(searchString);
                // Determine if a valid index is returned. Select the item if it is valid.
                if (index != -1)
                    nameList.SetSelected(index, true);
                else
                    MessageBox.Show("The search string did not match any items in the list");
            }
        }

        private IEnumerable<object> GetPlayerListByName(string name)
        {
            return from ph in _context.PlayerHistories
                where ph.PlayerName == name
                select new {ph.Id, ph.GameNumber, ph.PlayerName, ph.StartingStack};
        }

        private IEnumerable<object> GetOpponentsHandsCount()
        {
            foreach (var name in OponentNames.Where(name => !name.IsNullOrEmpty()))
            {
                int count = _context.PlayerHistories.Count(ph => ph.PlayerName == name);
                yield return new { name, count };
            }
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
            textBoxWatch.Text += text;
            textBoxWatch.Text += "\n";
        }

        //todo:updating statistics
        private void UpdateStatistics(string newtext)
        {
            ShowFileChanging(newtext);
        }

        private void FillGrid<T>(IEnumerable<T> collection)
        {
            var source = new BindingSource { DataSource = collection.ToList() };
            gridSummary.DataSource = source;
        }

        private void FillMainTab(string name)
        {
            FillGeneralPage(name);
        }

        private void FillGeneralPage(string name)
        {
            txBoxPlayerName.Text = name;
            FillSummaryGrid(name);
            FillStatisticsGrid(name);
        }

        private void FillStatisticsGrid(string name)
        {
            throw new NotImplementedException();
        }

        private void FillSummaryGrid(string name)
        {
            throw new NotImplementedException();
        }

        #endregion

        

        



    }
}
