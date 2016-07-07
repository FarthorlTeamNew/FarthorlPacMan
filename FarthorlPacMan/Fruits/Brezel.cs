using System.Drawing;

namespace FarthorlPacMan.Fruits
{
    public class Brezel : Fruit
    {
        public Brezel(int x, int y, Graphics graphicsFruit) : base(x, y, graphicsFruit)
        {
            base.Image= Image.FromFile(@"DataFiles\Images\Fruit\Brezel.bmp");
        }
        public override void ActivatePowerup()
        {

        }
    }
}