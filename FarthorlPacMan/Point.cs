namespace FarthorlPacMan
{
    using System.Drawing;

    public class Point
    {
        public static int PointDiameter { get; private set; }
        public int CenterX { get; private set; }
        public int CenterY { get; private set; }
        public Color PointColor { get; private set; }
        public static Color PointFillColor { get; private set; }
        public bool IsPointCollected { get; private set; }

        public Point()
        {
            this.PointColor = Color.Blue;
            PointFillColor = Color.BlueViolet;
            this.IsPointCollected = false;
            PointDiameter = 10;
        }

        public Point(int centerX, int centerY) :this()
        {
            this.CenterX = centerX;
            this.CenterY = centerY;
        }

        public void EatPoint()
        {
            this.IsPointCollected= true;
        }
    }
}