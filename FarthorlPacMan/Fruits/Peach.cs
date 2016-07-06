using System.Drawing;

namespace FarthorlPacMan.Fruits
{
    public class Peach : Fruit
    {
        private static Image peach = Image.FromFile(@"DataFiles\Images\Fruit\Peach.bmp");
        public Peach(int x, int y, Graphics graphicsFruit, Engine engine) : base(x, y, graphicsFruit, engine)
        {
        }

        public override void DrawFruit()
        {
            this.GraphicsFruit.DrawImage(peach, this.FruitPositionX * this.QuadrantDimension, this.FruitPositionY * this.QuadrantDimension);
        }
    }
}
