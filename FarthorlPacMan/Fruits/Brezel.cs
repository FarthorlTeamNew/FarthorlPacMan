using System.Drawing;

namespace FarthorlPacMan.Fruits
{
    public class Brezel : Fruit
    {
        private static Image brezel = Image.FromFile(@"DataFiles\Images\Fruit\Brezel.bmp");
        public Brezel(int X, int Y, Graphics graphicsFruit, Engine engine) : base(X, Y, graphicsFruit, engine)
        {
        }

        public override void DrawFruit()
        {
            this.graphicsFruit.DrawImage(brezel, this.fruitPositionX *this.QuadrantDimension, this.fruitPositionY *this.QuadrantDimension);
        }
    }
}
