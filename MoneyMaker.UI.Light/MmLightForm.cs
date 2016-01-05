using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using HandHistories.SimpleObjects.Entities;
using HandHistories.SimpleObjects.Tools;
using HandHistories.SimpleParser;
using MoneyMaker.BLL;

namespace MoneyMaker.UI.Light
{
    public partial class MmLightForm : Form
    {
        //Ф:Он имеет одинаковую сигнатуру из FileTrackingDelegate, но я выделил его в отдельный делегат 
        //из за его отличающейся назначения.
        public delegate void FillInfoDelegate(string path);

        //forms...
        private readonly FormSettings _formSettings;

        private readonly FileTrackingManager _fileTrackingManager;

        private bool _trackingState;


        public MmLightForm()
        {
            _trackingState = false;
            InitializeComponent();
            _formSettings = new FormSettings();
            _fileTrackingManager = new FileTrackingManager();
            _fileTrackingManager.PokerFileChanged += OnPokerFileChanged;
            _fileTrackingManager.Initialize("*.txt", NotifyFilters.LastWrite | NotifyFilters.FileName, Properties.Settings.Default.FileTrackingFolder);
            Properties.Settings.Default.PropertyChanged += OnSettingsPropertyChanged;
        }

        private void OnSettingsPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "FileTrackingFolder")
                _fileTrackingManager.Initialize("*.txt", NotifyFilters.LastWrite | NotifyFilters.FileName, Properties.Settings.Default.FileTrackingFolder);
        }

        private void OnPokerFileChanged(object sender, FileSystemEventArgs e)
        {
            BeginInvoke(new FillInfoDelegate(FillHudInfo), e.FullPath);
        }

        private void FillHudInfo(string fullPath)
        {
            var hudInitializer = new HudInitializer(new Poker888CashParser(), fullPath);
            hudInfoTxtBx.Text = hudInitializer.GetHudInfo();
            DrawHeroCards(hudInitializer);
            DrawMuckCards(hudInitializer);
            DrawGraphic(hudInitializer);
            var hudStatsCollection = hudInitializer.ParseHudStatistics();
            hudGrdVw.DataSource = new BindingSource
            {
                DataSource = hudStatsCollection
            };
        }

        private void DrawGraphic(HudInitializer hudInitializer)
        {
            profitChart.Titles.Add(new Title("Profit graphic"));
            profitChart.Series["Series1"].ChartType = SeriesChartType.Spline;
            var profits = hudInitializer.GetHeroProfits().ToList();
            var totalProfit = 0m;
            for (var i = 0; i < profits.Count(); i++)
            {
                totalProfit += profits[i];
                profitChart.Series["Series1"].Points.AddXY(i+1, totalProfit);
            }
            profitChart.Series["Series1"].LegendText = "Hero";
        }

        private void DrawMuckCards(HudInitializer hudInitializer)
        {
            var muck = hudInitializer.GetMucking();
            if (muck != null)
            {
                var cardsArray = muck.Cards.Split(',');
                muckLabel.Text = muck.PlayerName;
                pictureBoxMuck1.Image = CardsImageManager.GetImageCard((Card)cardsArray[0].ConvertStringCardToByte());
                pictureBoxMuck2.Image = CardsImageManager.GetImageCard((Card)cardsArray[1].ConvertStringCardToByte());
            }
            else
            {
                muckLabel.Text = "Mucking:";
                pictureBoxMuck1.Image = CardsImageManager.GetShirt();
                pictureBoxMuck2.Image = CardsImageManager.GetShirt();
            }
        }

        private void DrawHeroCards(HudInitializer hudInitializer)
        {
            var heroCards = hudInitializer.GetHeroCards();
            if (!string.IsNullOrEmpty(heroCards))
            {
                var heroArray = heroCards.Split(',');
                pictureBoxHero1.Image = CardsImageManager.GetImageCard((Card)heroArray[0].ConvertStringCardToByte());
                pictureBoxHero2.Image = CardsImageManager.GetImageCard((Card)heroArray[1].ConvertStringCardToByte());
            }
            else
            {
                pictureBoxHero1.Image = CardsImageManager.GetShirt();
                pictureBoxHero2.Image = CardsImageManager.GetShirt();
            }
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
    }
}
