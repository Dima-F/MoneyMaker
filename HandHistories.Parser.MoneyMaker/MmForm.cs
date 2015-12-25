using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using HandHistories.Parser.MoneyMaker.BLL;
using HandHistories.Parser.MoneyMaker.BLL.Repositories;
using HandHistories.Parser.MoneyMaker.BLL.Tools;
using HandHistories.Parser.MoneyMaker.EntityFramework;
using HandHistories.SimpleObjects.Entities;
using HandHistories.SimpleObjects.Tools;
using HandHistories.SimpleParser;

namespace HandHistories.Parser.MoneyMaker
{
    public partial class MmForm : Form
    {
        //Ф:Он имеет одинаковую сигнатуру из FileTrackingDelegate, но я выделил его в отдельный делегат 
        //из за его отличающейся назначения.
        public delegate void FillInofDelegate(string path);

        private Stopwatch _stopwatch;
        //forms...
        private FormSettings _formSettings;

        private FileTrackingManager _fileTrackingManager;

        private HudInitializer _hudInitializer;
        
        private IEnumerable<string> _oponentNames;

        private readonly IRepository _repository;

        public MmForm()
        {
            _stopwatch = new Stopwatch();
            _repository = new ContextDbRepository(new HandHistoryContext());
            InitializeComponent();
            _formSettings=new FormSettings();
            _fileTrackingManager = new FileTrackingManager();
            _fileTrackingManager.PokerFileChanged += OnPokerFileChanged;
            _fileTrackingManager.Initialize("*.txt", NotifyFilters.LastWrite | NotifyFilters.FileName);
            _hudInitializer = new HudInitializer(new Poker888CashHandHistoryParser());
        }
        

        #region Fields
        
        public IEnumerable<string> OponentNames
        {
            //lazy load pattern
            get 
            {
                return _oponentNames ?? (_oponentNames = _repository.OponentNames);
            }
        }

        #endregion

        #region Method-handlers...

        private void OnPokerFileChanged(object sender, FileSystemEventArgs e)
        {
            BeginInvoke(new FileTrackingDelegate(ShowFileChanging), string.Format("Poker file {0} changed at {1}", e.FullPath, DateTime.Now.ToShortTimeString()));
            BeginInvoke(new FillInofDelegate(FillHudInfo), e.FullPath);
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            startToolStripMenuItem.Enabled = false;
            stopTrackCurrentSessionToolStripMenuItem.Enabled = true;
            _fileTrackingManager.EnableTracking = true;
        }

        private void stopTrackCurrentSessionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            startToolStripMenuItem.Enabled = true;
            stopTrackCurrentSessionToolStripMenuItem.Enabled = false;
            _fileTrackingManager.EnableTracking = false;
        }

        private void MmForm_Load(object sender, EventArgs e)
        {
            LoadPlayersList();
        }

        //todo:not working...
        private void reinitializeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //_context.Database.Initialize(true);
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

        private void FillHudInfo(string fullPath)
        {
            _hudInitializer.Path = fullPath;
            hudInfoTxtBx.Text = _hudInitializer.GetHudInfo();
            DrawHeroCards();
            DrawMuckCards();
            var hudStatsCollection = _hudInitializer.ParseHudStatistics();
            hudGrdVw.DataSource = new BindingSource
            {
                DataSource = hudStatsCollection
            };
        }

        private void DrawMuckCards()
        {
            var muck = _hudInitializer.GetMucking();
            if (muck != null)
            {
                var cardsArray = muck.Cards.Split(',');
                muckLabel.Text = muck.PlayerName;
                pictureBoxMuck1.Image = CardsImageManager.GetImageCard((Card) cardsArray[0].ConvertStringCardToByte());
                pictureBoxMuck2.Image = CardsImageManager.GetImageCard((Card) cardsArray[1].ConvertStringCardToByte());
            }
            else
            {
                muckLabel.Text = "Mucking";
                pictureBoxMuck1.Image = CardsImageManager.GetShirt();
                pictureBoxMuck2.Image = CardsImageManager.GetShirt();
            }
        }

        private void DrawHeroCards()
        {
            var heroCards = _hudInitializer.GetHeroCards();
            if (!heroCards.IsNullOrEmpty())
            {
                var heroArray = heroCards.Split(',');
                pictureBoxHero1.Image = CardsImageManager.GetImageCard((Card) heroArray[0].ConvertStringCardToByte());
                pictureBoxHero2.Image = CardsImageManager.GetImageCard((Card) heroArray[1].ConvertStringCardToByte());
            }
            else
            {
                pictureBoxHero1.Image = CardsImageManager.GetShirt();
                pictureBoxHero2.Image = CardsImageManager.GetShirt();
            }
        }

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

        private void ShowFileChanging(string text)
        {
            textBoxWatch.Text += text;
            textBoxWatch.Text += "\r\n";
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
            _stopwatch.Start();
            var playerStatistics = _repository.GetPlayerStatisticsByName(name).ToList();
            _stopwatch.Stop();
            Debug.WriteLine("Filling statistics grid in " + _stopwatch.Elapsed.Seconds);
            _stopwatch.Reset();
            gridStatistics.DataSource = new BindingSource
            {
                DataSource = playerStatistics
            };
        }

        private void FillSummaryGrid(string name)
        {
            _stopwatch.Start();
            var playerSummaries = _repository.GetPlayerSummariesByName(name).ToList();
            _stopwatch.Stop();
            Debug.WriteLine("Filling summary grid in "+_stopwatch.Elapsed.Seconds);
            _stopwatch.Reset();
            gridSummary.DataSource = new BindingSource
            {
                DataSource = playerSummaries
            };
        }

        #endregion

        



        

        



    }
}
