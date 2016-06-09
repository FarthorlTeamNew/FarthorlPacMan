using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.Threading;

namespace FarthorlPacMan
{
    static class Startup
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            SoundPlayer munch;        
            //munch = new SoundPlayer("../../DataFiles/pacman_beginning.wav");
            munch = new SoundPlayer("../../DataFiles/munch.wav");
            munch.PlayLooping();
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GameWindow());
        }
    }
}
