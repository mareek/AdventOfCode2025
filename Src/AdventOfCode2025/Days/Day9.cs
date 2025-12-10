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
        var rectanglesBySize = GetRectangles(tiles, true).OrderByDescending(r => r.Area).ToArray();

        foreach (var rectangle in rectanglesBySize)
        {
            if (IsValid(rectangle))
                return rectangle.Area.ToString();
        }

        throw new Exception("Here be dragons");

        bool IsValid(Rectangle rectangle)
        {
            if (tiles.Any(rectangle.Contains))
                return false;

            return true;
        }
    }

    private static IEnumerable<Rectangle> GetRectangles(Tile[] tiles, bool checkValidity = false)
    {
        for (int i = 0; i < tiles.Length; i++)
            for (int j = i + 1; j < tiles.Length; j++)
            {
                var corner1 = tiles[i];
                var corner2 = tiles[j];
                Rectangle rectangle = new(corner1, corner2);
                if (!checkValidity)
                {
                    yield return rectangle;
                    continue;
                }

                // C'est un peu n'importe quoi
                var corner1Positions = corner1.GetRelativePosition(corner2).ToArray();
                var corner1IsOk = corner1Positions.Contains(corner1.GetRelativePosition(GetPreviousTile(i)).Single())
                                  && corner1Positions.Contains(corner1.GetRelativePosition(GetNextTile(i)).Single());

                var corner2Positions = corner2.GetRelativePosition(corner1).ToArray();
                var corner2IsOk = corner2Positions.Contains(corner2.GetRelativePosition(GetPreviousTile(j)).Single())
                                  && corner2Positions.Contains(corner2.GetRelativePosition(GetNextTile(j)).Single());

                if (corner1IsOk && corner2IsOk)
                    yield return rectangle;
            }

        Tile GetPreviousTile(int tileIndex) => tiles[(tileIndex - 1 + tiles.Length) % tiles.Length];
        Tile GetNextTile(int tileIndex) => tiles[(tileIndex + 1) % tiles.Length];
    }

    private readonly struct Tile(long x, long y)
    {
        public long X { get; } = x;
        public long Y { get; } = y;

        public IEnumerable<Position> GetRelativePosition(Tile reference)
        {
            if (X < reference.X)
                yield return Position.Left;
            else if (reference.X < X)
                yield return Position.Right;

            if (Y < reference.Y)
                yield return Position.Below;
            else if (reference.Y < Y)
                yield return Position.Above;
        }

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

        public bool Contains(Tile tile)
            => Bottom < tile.Y && tile.Y < Top
            && Left < tile.X && tile.X < Right;
    }

    private enum Position
    {
        Same = 0,
        Above = 1,
        Below = 2,
        Left = 4,
        Right = 8
    }
}