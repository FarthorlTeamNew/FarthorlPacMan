using System.Drawing;

namespace FarthorlPacMan.Fruits
{
    public class Brezel : Fruit
    {
        private static Image brezel = Image.FromFile(@"DataFiles\Images\Fruit\Brezel.bmp");
        public Brezel(int x, int y, Graphics graphicsFruit, Engine engine) : base(x, y, graphicsFruit, engine)
        {
        }

        public override void DrawFruit()
        {
            this.GraphicsFruit.DrawImage(brezel, this.FruitPositionX * this.QuadrantDimension, this.FruitPositionY * this.QuadrantDimension);
        }
    }
}
