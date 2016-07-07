namespace FarthorlPacMan
{
    using System.Drawing;

    public abstract class Fruit
    {
        protected Graphics GraphicsFruit;
        public int fruitPositionX;
        public int fruitPositionY;
        public int fruitWidth = 20;
        public int fruitHeigt = 20;
        public Image Image { get; protected set; }

        public int FruitPositionX
        {
            get { return fruitPositionX; }
            set
            {
                fruitPositionX = value;
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

        protected int QuadrantDimension = Global.QuadrantSize;

        protected Fruit(int x, int y, Graphics graphicsFruit)
        {
            this.FruitPositionX = x;
            this.FruitPositionY = y;
            this.GraphicsFruit = graphicsFruit;
        }

        public virtual void DrawFruit()
        {
        }

        public abstract void ActivatePowerup();
    }
}
