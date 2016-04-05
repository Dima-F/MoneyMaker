using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using HandHistories.SimpleObjects.Entities;
using HandHistories.SimpleObjects.Tools;
using MoneyMaker.BLL.Hud;
using MoneyMaker.BLL.Stats;
using MoneyMaker.UI.Light.BLL;

namespace MoneyMaker.UI.Light
{
    public partial class HudForm : Form
    {
        private readonly string _keyPath;

        public HudForm(string key)
        {
            
            InitializeComponent();
            _keyPath = key;
            Text = Path.GetFileNameWithoutExtension(_keyPath);
        }

        public void FillHud()
        {
            IStatOperator sOperator=new ConditionalStatOperator();
            var hudTable = new HudTable(sOperator, _keyPath);
            hudInfoTxtBx.Text = hudTable.GetHudInfo();
            DrawHeroCards(hudTable);
            DrawMuckCards(hudTable);
            DrawGraphic(hudTable);
            hudGrdVw.DataSource = FillDataTable(hudTable.GetPlayerStatsList());
            MinimizeGridWidth();
        }

        private void DrawGraphic(HudTable hudTable)
        {

            profitChart.Series["Series1"].Points.Clear();
            profitChart.Series["Series1"].IsVisibleInLegend = false;
            var profits = hudTable.GetHeroProfits().ToList();
            var totalProfit = 0m;
            for (var i = 0; i < profits.Count(); i++)
            {
                totalProfit += profits[i];
                profitChart.Series["Series1"].Points.AddXY(i + 1, totalProfit);
            }
            profitChart.Series["Series1"].LegendText = "Hero";
        }

        private void DrawMuckCards(HudTable hudTable)
        {
            var muck = hudTable.GetMucking();
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

        private void DrawHeroCards(HudTable hudTable)
        {
            var heroCards = hudTable.GetHeroCards();
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
            FillHud();
        }

        private void HudForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void MinimizeGridWidth()
        {
            hudGrdVw.Columns[0].Width = 65;
            for (int i = 1; i < hudGrdVw.Columns.Count; i++)
            {
                hudGrdVw.Columns[i].Width = 48;
            }
        }

        /// <summary>
        /// Ф:Получает набор статов игроков и формирует таблицу DataTable, которую 
        /// удобно привязывать к DataGridView
        /// </summary>
        private DataTable FillDataTable(List<PlayerStats> hudStatsCollection)
        {
            var table = new DataTable();
            table.Columns.Add("Player");
            foreach (var stat in hudStatsCollection.First())
            {
                table.Columns.Add(stat.Name);
            }
            foreach (var ps in hudStatsCollection)
            {
                var row = table.NewRow();
                row[0] = ps.Player;
                for (var j = 0; j < ps.Count; j++)
                {
                    row[j + 1] = ps[j].Value;
                }
                table.Rows.Add(row);
            }
            return table;
        }
        
    }
}
