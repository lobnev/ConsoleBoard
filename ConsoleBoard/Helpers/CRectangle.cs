namespace ConsoleBoard.NewFolder1
{
    public class CRectangle
    {
        public CPoint Position { get; set; } = new CPoint(0,0);
        public int Width { get; set; } = 0;
        public int Height { get; set; } = 0;

        public CRectangle()
        {
        }

        public CRectangle(CPoint position, int width, int height)
        {
            Position = position;
            Width = width;
            Height = height;
        }

        public CRectangle(int x, int y, int width, int height)
        {
            Position = new CPoint(x,y);
            Width = width;
            Height = height;
        }

        public CPoint RightBottom => Position + new CPoint(Width, Height);

        public override string ToString()
        {
            return $"Pos({Position.X}:{Position.Y}),Width({Width}),Height({Height})";
        }
    }
}