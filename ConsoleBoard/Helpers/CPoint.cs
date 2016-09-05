namespace ConsoleBoard.Helpers
{
    public class CPoint
    {
        public int X { get; set; }
        public int Y { get; set; }
        
        public CPoint(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static CPoint operator +(CPoint cPoint, CPoint offset) => new CPoint(cPoint.X + offset.X, cPoint.Y + offset.Y);
        public static CPoint operator -(CPoint cPoint, CPoint offset) => new CPoint(cPoint.X - offset.X, cPoint.Y - offset.Y);
        public static CPoint operator *(CPoint cPoint, int x) => new CPoint(cPoint.X * x, cPoint.Y * x);

        public override string ToString()
        {
            return $"({X}:{Y})";
        }
    }
}