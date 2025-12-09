namespace AdventOfCode2025.Days;

internal class Day8(bool real) : Day(real)
{
    public override int DayDate => 8;

    public override string ExecuteFirst()
    {
        int sampleSize = Real ? 1000 : 10;
        var points = Lines.Select(Point3D.Parse).ToArray();

        List<Link> links = [];
        for (int i = 0; i < points.Length; i++)
            for (int j = i + 1; j < points.Length; j++)
                links.Add(new(points[i], points[j]));

        var circuits = points.Select(p => new Circuit(p)).ToList();

        int count = 0;
        foreach (var link in links.OrderBy(l => l.Distance))
        {
            for (int i = 0; i < circuits.Count; i++)
            {
                if (circuits[i].TryAdd(link))
                {
                    for (int j = i + 1; j < circuits.Count; j++)
                    {
                        if (circuits[j].IsInCircuit(link))
                        {
                            circuits[i].Absorb(circuits[j]);
                            circuits.RemoveAt(j);
                            j--;
                        }
                    }

                    break;
                }
            }

            count++;
            if (count == sampleSize)
                break;
        }

        var largestCircuits = circuits.OrderByDescending(c => c.Size)
                                      .Take(3)
                                      .Select(c => c.Size)
                                      .ToArray();
        var result = largestCircuits.Aggregate((a, b) => a * b);

        return result.ToString();
    }

    public override string ExecuteSecond()
    {
        var points = Lines.Select(Point3D.Parse).ToArray();

        List<Link> links = [];
        for (int i = 0; i < points.Length; i++)
            for (int j = i + 1; j < points.Length; j++)
                links.Add(new(points[i], points[j]));

        var circuits = points.Select(p => new Circuit(p)).ToList();

        foreach (var link in links.OrderBy(l => l.Distance))
        {
            for (int i = 0; i < circuits.Count; i++)
            {
                if (circuits[i].TryAdd(link))
                {
                    for (int j = i + 1; j < circuits.Count; j++)
                    {
                        if (circuits[j].IsInCircuit(link))
                        {
                            circuits[i].Absorb(circuits[j]);
                            circuits.RemoveAt(j);
                            j--;
                        }
                    }

                    break;
                }
            }

            if (circuits.Count == 1)
            {
                long result = ((long)link.From.X) * ((long)link.To.X);
                return result.ToString();
            }
        }

        throw new Exception("Should not happen");
    }

    private readonly struct Point3D(int x, int y, int z)
    {
        public int X { get; } = x;
        public int Y { get; } = y;
        public int Z { get; } = z;

        public string Key => $"{X},{Y},{Z}";

        public double ComputeDistance(Point3D point)
        {
            var xDiff = X - point.X;
            var yDiff = Y - point.Y;
            var zDiff = Z - point.Z;

            var distance = Math.Sqrt(Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2) + Math.Pow(zDiff, 2));
            return distance;
        }

        public static Point3D Parse(string line)
        {
            var split = line.Split(',');
            return new(int.Parse(split[0]), int.Parse(split[1]), int.Parse(split[2]));
        }
    }

    private readonly struct Link(Point3D from, Point3D to)
    {
        public Point3D From { get; } = from;
        public Point3D To { get; } = to;
        public double Distance { get; } = from.ComputeDistance(to);
    }

    private class Circuit(Point3D box)
    {
        private readonly HashSet<Point3D> _boxes = [box];

        public int Size => _boxes.Count;

        public bool TryAdd(Link newLink)
        {
            if (!IsInCircuit(newLink))
                return false;

            _boxes.Add(newLink.To);
            _boxes.Add(newLink.From);
            return true;
        }

        public bool IsInCircuit(Link link)
            => _boxes.Contains(link.From) || _boxes.Contains(link.To);

        internal void Absorb(Circuit circuit)
        {
            foreach (var box in circuit._boxes)
                _boxes.Add(box);
        }
    }
}
