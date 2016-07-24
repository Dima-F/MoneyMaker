using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using HandHistories.SimpleObjects.Entities;
using HandHistories.SimpleParser;
using MoneyMaker.BLL.Files;
using MoneyMaker.BLL.Stats;
using MoneyMaker.BLL.Tools;
using MoneyMaker.UI.Light.BLL;
using MoneyMaker.UI.Light.Properties;

namespace MoneyMaker.UI.Light
{
    public partial class MmLightForm : Form
    {
        //forms...
        private readonly FormSettings _formSettings;

        private readonly AboutForm _aboutForm;

        private readonly FileTrackingManager _fileTrackingManager;

        private bool _trackingState;

        private readonly IDictionary<string, HudForm> _huds;

        //buffer for simple parsing
        private List<Game> _allGames;

        private List<Game> _liveGames;
        
        public MmLightForm()
        {
            _huds = new Dictionary<string, HudForm>();
            _trackingState = false;
            InitializeComponent();
            _formSettings = new FormSettings();
            _aboutForm = new AboutForm();
            _fileTrackingManager = new FileTrackingManager();
            _fileTrackingManager.PokerFileChanged += OnPokerFileChanged;
            _fileTrackingManager.Initialize("*.txt", NotifyFilters.LastWrite | NotifyFilters.FileName, Settings.Default.FileTrackingFolder);
            Settings.Default.PropertyChanged += OnSettingsPropertyChanged;
            _allGames = new List<Game>();
            _liveGames = new List<Game>();
        }

        private void OnSettingsPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "FileTrackingFolder")
                _fileTrackingManager.Initialize("*.txt", NotifyFilters.LastWrite | NotifyFilters.FileName, Properties.Settings.Default.FileTrackingFolder);
        }

        private void OnPokerFileChanged(object sender, FileSystemEventArgs e)
        {
            //ShowHudForm(e.FullPath);
            Invoke((Action)(()=>ShowHudForm(e.FullPath)));
        }

        private void pictureBoxTrack_Click(object sender, EventArgs e)
        {
            _trackingState = !_trackingState;
            _fileTrackingManager.EnableTracking = _trackingState;
            ToggleTrackingButton(_trackingState);
        }

        private void ToggleTrackingButton(bool trackingState)
        {
            pictureBoxTrack.Image = trackingState ? Properties.Resources.track_of : Properties.Resources.track_on;
        }

        private void pictureBoxSettings_Click(object sender, EventArgs e)
        {
            _formSettings.ShowDialog();
        }

        private void MmLightForm_Load(object sender, EventArgs e)
        {
            ToggleTrackingButton(_trackingState);
        }

        private void ShowHudForm(string fullPath)
        {
            if (!_huds.ContainsKey(fullPath))
            {
                var hudForm = new HudForm(fullPath);
                hudForm.Show();
                _huds.Add(fullPath, hudForm);
            }
            else
            {
                _huds[fullPath].FillHud();
                _huds[fullPath].Show();
            }
        }

        private void pictureBoxAbout_Click(object sender, EventArgs e)
        {
            _aboutForm.ShowDialog();
        }

        private void btnSimplePars_Click(object sender, EventArgs e)
        {
            ParseAndVisualize();
        }

        private async void btnAllStat_Click(object sender, EventArgs e)
        {
            ParseAndVisualize();

            List<Game> games = checkBoxLive.Checked ? _liveGames : _allGames;

            comboBoxPlayers.DataSource = games.GetAllPlayerNames().ToList();
            //Debug.WriteLine("Current thread ID - {0}",Thread.CurrentThread.ManagedThreadId);
            await Task.Run(() =>
            {
                StatOperator statOperator = null;
                if (cbAllStats.Checked)
                        statOperator = new BaseStatOperator(checkBoxLive.Checked, Settings.Default.LiveGamesCount);
                else statOperator = new ConditionalStatOperator(checkBoxLive.Checked);
                    
                //Thread.Sleep(5000);
                var table = statOperator.GetPlayerStatsList(games).ToDataTable();
                //Debug.WriteLine("Current thread ID - {0}", Thread.CurrentThread.ManagedThreadId);
                Invoke((Action)delegate
                {
                    datGrViewAllStats.DataSource = table;
                    MinimizeGridWidth();
                });
            });
        }

        private void MinimizeGridWidth()
        {
            if (datGrViewAllStats.Columns.Count == 0) return;
            datGrViewAllStats.Columns[0].Width = 65;
            for (int i = 1; i < datGrViewAllStats.Columns.Count; i++)
            {
                datGrViewAllStats.Columns[i].Width = 48;
            }
        }

        //Оставлен для тестирования и отладки.
        private void btnStatSyncron_Click(object sender, EventArgs e)
        {
            if(_allGames.Count==0)
                ParseAndVisualize();
            List<Game> games = checkBoxLive.Checked ? _liveGames : _allGames;

            comboBoxPlayers.DataSource = games.GetAllPlayerNames().ToList();
            
            //start diagnostic --------------------------------------------------------------------------
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            StatOperator statOperator = null;
            if (cbAllStats.Checked)
                statOperator = new BaseStatOperator(checkBoxLive.Checked, Settings.Default.LiveGamesCount);
            else statOperator = new ConditionalStatOperator(checkBoxLive.Checked);
            var table = statOperator.GetPlayerStatsList(games).ToDataTable();
            datGrViewAllStats.DataSource = table;
            stopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;
            // Format and display the TimeSpan value.
            string elapsedTime = $"Time of statistics estimation - {ts.Milliseconds} ms";
            
            //end diagnostic ----------------------------------------------------------------------------
            MinimizeGridWidth();
            MessageBox.Show(elapsedTime);
        }

        private void ParseAndVisualize()
        {
            if (_allGames.Count != 0)
            {
                _allGames.Clear();
                _liveGames.Clear();
            }
                
            textBoxSimpleParsing.Clear();
            progBarSimpleParsing.Value = 0;
            lblGamesCount.Text = string.Empty;
            var directory = Settings.Default.HandHistoryFolder;
            var files = Directory.GetFiles(directory, "*.txt").Where(s => !s.Contains("Summary")).ToArray();
            progBarSimpleParsing.Maximum = files.Length;
            var builder = new StringBuilder();
            foreach (var file in files)
            {
                var shortPath = Path.GetFileNameWithoutExtension(file);
                var text = PokerFileReader.ReadFile(file);
                var parser = ParserFactory.CreateParser(shortPath);
                var games = parser.ParseGames(text);
                _allGames.AddRange(games);
                builder.AppendLine($"***{shortPath}");
                progBarSimpleParsing.Increment(1);
            }
            lblGamesCount.Text = _allGames.Count.ToString();
            textBoxSimpleParsing.Text = builder.ToString();
            _liveGames.AddRange(_allGames.GetLive(Settings.Default.LiveHours));
        }
    }
}
