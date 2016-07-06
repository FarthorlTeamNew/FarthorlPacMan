using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarthorlPacMan
{
    public class Fruit
    {
        private Engine engine;
        private Graphics graphicsFruit;
        private int fruitPositionX = 5;
        private int fruitPositionY = 6;
        private int quadrantDimension = Global.QuadrantSize;
        private static Image apple = Image.FromFile(@"DataFiles\Images\Fruit\Apple.bmp");
        private static Image banana = Image.FromFile(@"DataFiles\Images\Fruit\Banana.bmp");
        private static Image brezel = Image.FromFile(@"DataFiles\Images\Fruit\Brezel.bmp");
        private static Image cherry = Image.FromFile(@"DataFiles\Images\Fruit\Cherry.bmp");
        private static Image peach = Image.FromFile(@"DataFiles\Images\Fruit\Peach.bmp");
        private static Image pear = Image.FromFile(@"DataFiles\Images\Fruit\Pear.bmp");
        private static Image strawberry = Image.FromFile(@"DataFiles\Images\Fruit\Strawberry.bmp");

        public Fruit(int x, int y, Graphics graphicsFruit, Engine engine)
        {
            this.fruitPositionX = x;
            this.fruitPositionY = y;
            this.engine = engine;
            this.graphicsFruit = graphicsFruit;
        }

        public void Draw()
        {
            this.graphicsFruit.DrawImage(apple, this.fruitPositionX * this.quadrantDimension, this.fruitPositionY * this.quadrantDimension);
        }
    }
}
