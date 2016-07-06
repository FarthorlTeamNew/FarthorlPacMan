using System.Drawing;

namespace FarthorlPacMan.Fruits
{
    public class Strawberry : Fruit
    {
        public Strawberry(int x, int y, Graphics graphicsFruit) : base(x, y, graphicsFruit)
        {
            base.Image= Image.FromFile(@"DataFiles\Images\Fruit\Strawberry.bmp");
        }

    }
}
