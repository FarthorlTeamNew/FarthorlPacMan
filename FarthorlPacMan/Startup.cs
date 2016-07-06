namespace FarthorlPacMan
{
    using System;
    using System.Windows.Forms;

    public static class Startup
    {
        // Game starts from here
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new InitialWindow());
        }
    }
}