using System.Drawing;

namespace FarthorlPacMan
{
    public static class Global
    {
        public const int QuadrantSize = 50;
        public const int DrawingSpeed = 6;
        public const int XMax = 24;
        public const int YMax = 13;
        public const int GhostElements = 4;
        public static Color WallColor {get { return Color.Cyan; } }
        public static Color pacManColor {get { return Color.Yellow; } }
    }
}
