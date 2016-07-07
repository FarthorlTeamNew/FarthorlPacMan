using System.Drawing;

namespace FarthorlPacMan
{
    public abstract class Fruit
    {
        protected Graphics GraphicsFruit;
        protected int QuadrantDimension = Global.QuadrantSize;
        public int fruitPositionX;
        public int fruitPositionY;
        public int fruitWidth = 35;
        public int fruitHeigt = 35;

        protected Fruit(int x, int y, Graphics graphicsFruit)
        {
            this.FruitPositionX = x;
            this.FruitPositionY = y;
            this.GraphicsFruit = graphicsFruit;
        }

        public Image Image { get; protected set; }

        public int FruitPositionX
        {
            get { return this.fruitPositionX; }
            set
            {
                this.fruitPositionX = value;
            }
        }
        public int FruitPositionY
        {
            get { return fruitPositionY; }
            set
            {
                fruitPositionY = value;
            }
        }

        public virtual void DrawFruit()
        {
        }

        public abstract void ActivatePowerup();
    }
}