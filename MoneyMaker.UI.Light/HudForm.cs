using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using HandHistories.SimpleObjects.Entities;
using HandHistories.SimpleObjects.Tools;
using HandHistories.SimpleParser;
using MoneyMaker.BLL.Hud;

namespace MoneyMaker.UI.Light
{
    public partial class HudForm : Form
    {
        private readonly string _keyPath;

        public HudForm(string key)
        {
            
            InitializeComponent();
            _keyPath = key;
            Text = _keyPath;
        }

        public void FillHudInfo(string fullPath)
        {
            IHandHistoryParser parser = ParserFactory.CreateParser(Path.GetFileNameWithoutExtension(fullPath));
            var hudInitializer = new HudInitializer(parser, fullPath);
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

            profitChart.Series["Series1"].Points.Clear();
            var profits = hudInitializer.GetHeroProfits().ToList();
            var totalProfit = 0m;
            for (var i = 0; i < profits.Count(); i++)
            {
                totalProfit += profits[i];
                profitChart.Series["Series1"].Points.AddXY(i + 1, totalProfit);
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

        private void HudForm_Load(object sender, EventArgs e)
        {
            FillHudInfo(_keyPath);
        }

        private void HudForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}
