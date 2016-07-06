using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FarthorlPacMan;

namespace FarthorlPacMan.States
{
    public partial class LevelsWindow : Form
    {
        private Dictionary<string, string> _levelsFilePaths;

        public LevelsWindow()
        {
            this.InitializeComponent();
            this._levelsFilePaths = new ExtractAllLevels().ExctractLevels();
        }

        private void CsharpLevel_Click(object sender, EventArgs e)
        {
            GameWindow.Level = this._levelsFilePaths["CSharpLove"];
            this.Close();
        }
        private void Labirint_Level_Click(object sender, EventArgs e)
        {
            GameWindow.Level = this._levelsFilePaths["Labirint"];
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GameWindow.Level = this._levelsFilePaths["Labirint2"];
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GameWindow.Level = this._levelsFilePaths["Euro2016"];
            this.Close();
        }

        private void Labirint_Level_MouseHover(object sender, EventArgs e)
        {
            this.pictureBox1.BackgroundImage = Properties.Resources.LabirintImage;
        }

        private void Labirint2_MouseHover(object sender, EventArgs e)
        {
            this.pictureBox1.BackgroundImage = Properties.Resources.Labirint2Image;
        }

        private void CsharpLevel_MouseHover(object sender, EventArgs e)
        {
            this.pictureBox1.BackgroundImage = Properties.Resources.CsharpImage;
        }

        private void EuroLevel_MouseHover(object sender, EventArgs e)
        {
            this.pictureBox1.BackgroundImage = Properties.Resources.EuroLevelImage;
        }
    }
}