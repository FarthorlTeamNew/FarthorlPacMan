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
        private int fruitPositionX;
        private int fruitPositionY;
        private int QuadrantDimension = Global.QuadrantSize;
        private static Image apple = Image.FromFile(@"DataFiles\Images\Fruit\Apple.bmp");
        private static Image banana = Image.FromFile(@"DataFiles\Images\Fruit\Banana.bmp");
        private static Image brezel = Image.FromFile(@"DataFiles\Images\Fruit\Brezel.bmp");
        private static Image cherry = Image.FromFile(@"DataFiles\Images\Fruit\Cherry.bmp");
        private static Image peach = Image.FromFile(@"DataFiles\Images\Fruit\Peach.bmp");
        private static Image pear = Image.FromFile(@"DataFiles\Images\Fruit\Pear.bmp");
        private static Image strawberry = Image.FromFile(@"DataFiles\Images\Fruit\Strawberry.bmp");

        public Fruit(int X, int Y, Graphics graphicsFruit, Engine engine)
        {
            this.fruitPositionX = X;
            this.fruitPositionY = Y;
            this.engine = engine;
            this.graphicsFruit = graphicsFruit;
        }

        public void DrawApple()
        {
            graphicsFruit.DrawImage(apple, fruitPositionX * QuadrantDimension, fruitPositionY * QuadrantDimension);
        }
        public void DrawBanana()
        {
            graphicsFruit.DrawImage(banana, fruitPositionX * QuadrantDimension, fruitPositionY * QuadrantDimension);
        }

        public void DrawBrezel()
        {
            graphicsFruit.DrawImage(brezel, fruitPositionX * QuadrantDimension, fruitPositionY * QuadrantDimension);
        }

        public void DrawCherry()
        {
            graphicsFruit.DrawImage(cherry, fruitPositionX * QuadrantDimension, fruitPositionY * QuadrantDimension);
        }
        public void DrawPeach()
        {
            graphicsFruit.DrawImage(peach, fruitPositionX * QuadrantDimension, fruitPositionY * QuadrantDimension);
        }
        public void DrawPear()
        {
            graphicsFruit.DrawImage(pear, fruitPositionX * QuadrantDimension, fruitPositionY * QuadrantDimension);
        }
        public void DrawStrawberry()
        {
            graphicsFruit.DrawImage(strawberry, fruitPositionX * QuadrantDimension, fruitPositionY * QuadrantDimension);
        }
    }
}
