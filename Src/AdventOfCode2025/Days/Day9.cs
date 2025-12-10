namespace AdventOfCode2025.Days;

internal class Day9(bool real) : Day(real)
{
    public override int DayDate => 9;

    public override string ExecuteFirst()
    {
        Tile[] tiles = Lines.Select(Tile.Parse).ToArray();

        long maxArea = GetRectangles(tiles).Select(r => r.Area).Max();
        return maxArea.ToString();
    }

    public override string ExecuteSecond()
    {
        Tile[] tiles = Lines.Select(Tile.Parse).ToArray();
        List<Line> lines = [];
        for (int i = 0; i < tiles.Length; i++)
        {
            var tile = tiles[i];
            var nextTile = tiles[(i + 1) % tiles.Length];
            lines.Add(new(tile, nextTile));
        }

        var biggestRectangle = GetRectangles(tiles).OrderByDescending(r => r.Area)
                                                   .First(IsValid);
        return biggestRectangle.Area.ToString();

        bool IsValid(Rectangle rectangle)
        {
            if (lines.Any(rectangle.Intersect))
                return false;

            return true;
        }
    }

    private static IEnumerable<Rectangle> GetRectangles(Tile[] tiles)
    {
        for (int i = 0; i < tiles.Length; i++)
            for (int j = i + 1; j < tiles.Length; j++)
                yield return new Rectangle(tiles[i], tiles[j]);
    }

    private readonly struct Tile(long x, long y)
    {
        public long X { get; } = x;
        public long Y { get; } = y;

        public static Tile Parse(string line)
        {
            var split = line.Split(',');
            return new(long.Parse(split[0]), long.Parse(split[1]));
        }
    }

    private readonly struct Rectangle(Tile firstCorner, Tile secondCorner)
    {
        public long Top { get; } = Math.Max(firstCorner.Y, secondCorner.Y);
        public long Bottom { get; } = Math.Min(firstCorner.Y, secondCorner.Y);
        public long Right { get; } = Math.Max(firstCorner.X, secondCorner.X);
        public long Left { get; } = Math.Min(firstCorner.X, secondCorner.X);

        public long Area => (Top - Bottom + 1) * (Right - Left + 1);

        internal bool Intersect(Line line) 
            => Bottom < line.Top
               && line.Bottom < Top
               && Left < line.Right
               && line.Left < Right;
    }

    private class Line(Tile from, Tile to)
    {
        public Tile From { get; } = from;
        public Tile To { get; } = to;
        public long Top { get; } = Math.Max(from.Y, to.Y);
        public long Bottom { get; } = Math.Min(from.Y, to.Y);
        public long Right { get; } = Math.Max(from.X, to.X);
        public long Left { get; } = Math.Min(from.X, to.X);
    }
}