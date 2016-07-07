using System.Drawing;

namespace FarthorlPacMan.Fruits
{
    public class Pear : Fruit
    {
        public Pear(int x, int y, Graphics graphicsFruit) : base(x, y, graphicsFruit)
        {
            base.Image= Image.FromFile(@"DataFiles\Images\Fruit\Pear.bmp");
        }
        public override void ActivatePowerup()
        {

        }
    }
}