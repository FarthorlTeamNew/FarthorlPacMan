using System.Drawing;

namespace FarthorlPacMan.Fruits
{
    public class Apple : Fruit
    {
        private static  Image _apple = Image.FromFile(@"DataFiles\Images\Fruit\Apple.bmp");

        public Apple(int x, int y, Graphics graphicsFruit, Engine engine) : base(x, y, graphicsFruit, engine)
        {            
        }

        public override void DrawFruit()
        {
            this.GraphicsFruit.DrawImage(_apple, this.FruitPositionX * this.QuadrantDimension, this.FruitPositionY * this.QuadrantDimension);
        }
    }
}