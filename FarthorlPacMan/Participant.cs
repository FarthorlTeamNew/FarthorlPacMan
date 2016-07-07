namespace FarthorlPacMan
{
    public class Participant
    {
        public Participant()
        {
        }

        public Participant(int positionXQaundarnt, int positionYQuadrant)
        {
            this.PositionQuadrantX = positionXQaundarnt;
            this.PositionQuadrantY = positionYQuadrant;
        }

        public int PositionQuadrantX { get; protected set; }
        public int PositionQuadrantY { get; protected set; }
        public int DrawingCoordinatesX { get; set; }
        public int DrawingCoordinatesY { get; set; }
        protected string MovedDirection { get; set; }
        protected string PreviousDirection { get; set; }
    }
}