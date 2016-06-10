using System.Media;

namespace FarthorlPacMan
{
    using System;
    using System.Drawing;
    class Point
    {
        private int centerX;
        private int centerY;
        private Color pointColor = Color.Blue;
        private Color pointFillColor = Color.BlueViolet;
        private int pointDiameter = 10;
        private Boolean isCollected = false;

        public Point(int centerX, int centerY)
        {
            this.centerX = centerX;
            this.centerY = centerY;
        }

        public void EatPoint()
        {
            this.isCollected = true;
        }

        public int getX()
        {
            return this.centerX;
        }

        public int getY()
        {
            return this.centerY;
        }

        public bool isEatPoint()
        {
            return this.isCollected;
        }

        public int getDiameter()
        {
            return pointDiameter;
        }

        public Color color()
        {
            return this.pointColor;
        }

        public Color fillColor()
        {
            return this.pointFillColor;
        }
    }
}
