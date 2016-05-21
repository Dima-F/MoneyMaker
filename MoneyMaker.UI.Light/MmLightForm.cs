using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using HandHistories.SimpleObjects.Entities;
using HandHistories.SimpleParser;
using MoneyMaker.BLL.Files;
using MoneyMaker.BLL.Stats;
using MoneyMaker.BLL.Tools;
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

        public MmLightForm()
        {
            _huds=new Dictionary<string, HudForm>();
            _trackingState = false;
            InitializeComponent();
            _formSettings = new FormSettings();
            _aboutForm=new AboutForm();
            _fileTrackingManager = new FileTrackingManager();
            _fileTrackingManager.PokerFileChanged += OnPokerFileChanged;
            _fileTrackingManager.Initialize("*.txt", NotifyFilters.LastWrite | NotifyFilters.FileName, Settings.Default.FileTrackingFolder);
            Settings.Default.PropertyChanged += OnSettingsPropertyChanged;
            _allGames=new List<Game>();
        }

        private void OnSettingsPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "FileTrackingFolder")
                _fileTrackingManager.Initialize("*.txt", NotifyFilters.LastWrite | NotifyFilters.FileName, Properties.Settings.Default.FileTrackingFolder);
        }

        private void OnPokerFileChanged(object sender, FileSystemEventArgs e)
        {
            //ShowHudForm(e.FullPath);
            Action action = () => ShowHudForm(e.FullPath);
            Invoke(action);
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
                var hudForm=new HudForm(fullPath);
                hudForm.Show();
                _huds.Add(fullPath,hudForm);
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
            _allGames.Clear();
            textBoxSimpleParsing.Clear();
            lblSimpPars.Text = "Parsed files:";
            var directory = Settings.Default.HandHistoryFolder;
            var files = Directory.GetFiles(directory, "*.txt").Where(s => !s.Contains("Summary")).ToArray();
            progBarSimpleParsing.Value = 0;
            progBarSimpleParsing.Minimum = 0;
            progBarSimpleParsing.Maximum = files.Length;
            var builder=new StringBuilder();
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
            textBoxSimpleParsing.Text = builder.ToString();
            lblSimpPars.Text = $"Parsed files: {_allGames.Count}";
        }

        private void btnAllStat_Click(object sender, EventArgs e)
        {
            if (_allGames.Count == 0)
                btnSimplePars_Click(null, null);
            comboBoxPlayers.DataSource = _allGames.GetAllPlayerNames().ToList();
            var statOperator = new BaseStatOperator();
            datGrViewAllStats.DataSource = statOperator.GetPlayerStatsList(_allGames).ToDataTable();
            MinimizeGridWidth();
        }

        private void MinimizeGridWidth()
        {
            datGrViewAllStats.Columns[0].Width = 65;
            for (int i = 1; i < datGrViewAllStats.Columns.Count; i++)
            {
                datGrViewAllStats.Columns[i].Width = 48;
            }
        }
    }
}
