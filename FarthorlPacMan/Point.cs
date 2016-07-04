using System.Drawing;

namespace FarthorlPacMan
{
    public class Point
    {
        public int PointDiameter { get; private set; }
        public int CenterX { get; private set; }
        public int CenterY { get; private set; }
        public Color PointColor { get; private set; }
        public Color PointFillColor { get; private set; }
        public bool IsPointCollected { get; private set; }

        public Point()
        {
            this.PointColor = Color.Blue;
            this.PointFillColor = Color.BlueViolet;
            this.IsPointCollected = false;
            this.PointDiameter = 10;
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

        //public int getX()
        //{
        //    return this.CenterX;
        //}

        //public int getY()
        //{
        //    return this.CenterY;
        //}

        //public bool isEatPoint()
        //{
        //    return this.IsCollected;
        //}

        //public int getDiameter()
        //{
        //    return PointDiameter;
        //}

        //public Color color()
        //{
        //    return this.PointColor;
        //}

        //public Color fillColor()
        //{
        //    return this.PointFillColor;
        //}
    }
}