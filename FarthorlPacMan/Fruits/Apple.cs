using System.Drawing;
using System.Runtime.CompilerServices;

namespace FarthorlPacMan.Fruits
{
    public class Apple : Fruit
    {
        public Apple(int x, int y, Graphics graphicsFruit) : base(x, y, graphicsFruit)
        {
            base.Image= Image.FromFile(@"DataFiles\Images\Fruit\Apple.bmp");
        }

        public override void ActivatePowerup()
        {
           
        }
    }
}