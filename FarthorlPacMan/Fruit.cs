using System.Drawing;

namespace FarthorlPacMan
{
    public abstract class Fruit
    {
        protected Engine engine;
        protected Graphics graphicsFruit;
        protected int fruitPositionX;
        protected int fruitPositionY;
        protected int QuadrantDimension = Global.QuadrantSize;

        public Fruit(int X, int Y, Graphics graphicsFruit, Engine engine)
        {
            this.fruitPositionX = X;
            this.fruitPositionY = Y;
            this.engine = engine;
            this.graphicsFruit = graphicsFruit;
        }

        // graphicsFruit.DrawImage(apple, fruitPositionX * QuadrantDimension, fruitPositionY * QuadrantDimension);
        public abstract void DrawFruit();
    }
}
