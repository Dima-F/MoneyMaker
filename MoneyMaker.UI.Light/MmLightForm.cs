using System;
using System.Diagnostics;
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



        public MmLightForm()
        {
            InitializeComponent();
            _formSettings = new FormSettings();
            _fileTrackingManager = new FileTrackingManager();
            _fileTrackingManager.PokerFileChanged += OnPokerFileChanged;
            _fileTrackingManager.Initialize("*.txt", NotifyFilters.LastWrite | NotifyFilters.FileName);
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
            profitChart.Series["Hero"].ChartType = SeriesChartType.Spline;
            var profits = hudInitializer.GetHeroProfits().ToList();
            var totalProfit = 0m;
            profitChart.Series["Hero"].Points.AddY(totalProfit);
            for(var i=0;i<profits.Count();i++)
            {
                totalProfit += profits[i];
                profitChart.Series["Hero"].Points.AddY(totalProfit);
            }
            profitChart.Series["Hero"].LegendText = "bla bla";
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

        private void btnStart_Click(object sender, EventArgs e)
        {
            _fileTrackingManager.EnableTracking = true;
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            _formSettings.ShowDialog();
        }
    }
}
