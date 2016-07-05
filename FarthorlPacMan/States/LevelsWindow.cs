using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FarthorlPacMan;

namespace FarthorlPacMan.States
{
    public partial class LevelsWindow : Form
    {
        private string selectedLevel = string.Empty;

        private Dictionary<string, string> levelsFilePaths;

        public LevelsWindow()
        {
            InitializeComponent();
            this.levelsFilePaths = new ExtractAllLevels().ExctractLevels();
        }

        private void CsharpLevel_Click(object sender, EventArgs e)
        {
            this.selectedLevel = "CSharpLove";
            this.pictureBox1.BackgroundImage = global::FarthorlPacMan.Properties.Resources.CsharpImage;
        }
        private void Labirint_Level_Click(object sender, EventArgs e)
        {
            this.selectedLevel = "Labirint";
            this.pictureBox1.BackgroundImage = global::FarthorlPacMan.Properties.Resources.LabirintImage;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.selectedLevel = "Labirint2";
            this.pictureBox1.BackgroundImage = global::FarthorlPacMan.Properties.Resources.Labirint2Image;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.selectedLevel = "Euro2016";
            this.pictureBox1.BackgroundImage = global::FarthorlPacMan.Properties.Resources.EuroLevelImage;
        }

        private void ChooseLevel_Click(object sender, EventArgs e)
        {
            GameWindow.Level = levelsFilePaths[this.selectedLevel];
            Close();
        }
    }
}