namespace AdventOfCode2025.Days;

internal class Day9(bool real) : Day(real)
{
    public override int DayDate => 9;

    public override string ExecuteFirst()
    {
        Tile[] tiles = Lines.Select(Tile.Parse).ToArray();

        long maxArea = 0;
        for (int i = 0; i < tiles.Length; i++)
        {
            var firstTile = tiles[i];
            for (int j = i + 1; j < tiles.Length; j++)
            {
                var secondTile = tiles[j];
                long width = Math.Abs(firstTile.X - secondTile.X) + 1;
                long height = Math.Abs(firstTile.Y - secondTile.Y) + 1;
                long area = width * height;
                if (area > maxArea)
                {
                    maxArea = area;
                }
            }
        }

        return maxArea.ToString();
    }

    public override string ExecuteSecond()
    {
        Tile[] redTiles = Lines.Select(Tile.Parse).ToArray();
        long minX = redTiles.Select(t => t.X).Min();
        long maxX = redTiles.Select(t => t.X).Max();
        long minY = redTiles.Select(t => t.Y).Min();
        long maxY = redTiles.Select(t => t.Y).Max();

        List<Line> greenZone = [];
        for (int i = 0; i < redTiles.Length; i++)
        {
            var firstTile = redTiles[i];
            var secondTile = redTiles[(i + 1) % redTiles.Length];
            greenZone.Add(new(firstTile, secondTile));
        }
        // Faire des trucs ici

        return "";
    }

    private readonly struct Tile(long x, long y, char color)
    {
        public long X { get; } = x;
        public long Y { get; } = y;
        public char Color { get; } = color;

        public static Tile Parse(string line)
        {
            var split = line.Split(',');
            return new(long.Parse(split[0]), long.Parse(split[1]), 'R');
        }
    }

    private readonly struct Line(Tile from, Tile to)
    {
        public Tile From { get; } = from;
        public Tile To { get; } = to;
    }
}