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
        private Dictionary<string, string> levelsFilePaths;

        public LevelsWindow()
        {
            InitializeComponent();
            this.levelsFilePaths = new ExtractAllLevels().ExctractLevels();
        }

        private void CsharpLevel_Click(object sender, EventArgs e)
        {
            GameWindow.Level =  levelsFilePaths["CSharpLove"];
            Close();
        }
        private void Labirint_Level_Click(object sender, EventArgs e)
        {
            GameWindow.Level = levelsFilePaths["Labirint"];
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GameWindow.Level = levelsFilePaths["Labirint2"];
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GameWindow.Level = levelsFilePaths["Euro2016"];
            Close();
        }
    }
}