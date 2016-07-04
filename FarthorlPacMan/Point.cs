using System.Drawing;

namespace FarthorlPacMan
{
    public class Point
    {
        public Point()
        {

        }

        public Point(int centerX, int centerY)
        {
            this.CenterX = centerX;
            this.CenterY = centerY;
            this.PointColor = Color.Blue;
            this.PointFillColor = Color.BlueViolet;
            this.PointDiameter = 10;
            this.IsCollected = false;
        }

        public int CenterX { get; set; }
        public int CenterY { get; set; }
        public Color PointColor { get; }
        public Color PointFillColor { get; }
        public int PointDiameter { get; }
        public bool IsCollected { get; private set; }

        public void EatPoint()
        {
            this.IsCollected = true;
        }

        public int getX()
        {
            return this.CenterX;
        }

        public int getY()
        {
            return this.CenterY;
        }

        public bool isEatPoint()
        {
            return this.IsCollected;
        }

        public int getDiameter()
        {
            return PointDiameter;
        }

        public Color color()
        {
            return this.PointColor;
        }

        public Color fillColor()
        {
            return this.PointFillColor;
        }
    }
}