using System;
using System.Windows.Forms;

namespace FarthorlPacMan
{
    static class Startup
    {
        // Game starts from here
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new InitialWindow());
        }
    }
}