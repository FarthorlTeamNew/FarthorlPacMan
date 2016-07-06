using System.Drawing;

namespace FarthorlPacMan.Fruits
{
    public class Peach : Fruit
    {
        private static Image peach = Image.FromFile(@"DataFiles\Images\Fruit\Peach.bmp");
        public Peach(int X, int Y, Graphics graphicsFruit, Engine engine) : base(X, Y, graphicsFruit, engine)
        {
        }

        public override void DrawFruit()
        {
            this.graphicsFruit.DrawImage(peach, this.fruitPositionX *this.QuadrantDimension, this.fruitPositionY *this.QuadrantDimension);
        }
    }
}
