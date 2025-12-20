namespace AdventOfCode2025.Days;

internal abstract class Day
{
    protected Day(bool real)
    {
        Real = real;
        _lazyLines = new(() => ReadLines(real, DayDate));
    }

    public bool Real { get; }

    public bool Slow { get; init; } = false;


    private readonly Lazy<string[]> _lazyLines;
    public string[] Lines => _lazyLines.Value;

    public abstract int DayDate { get; }

    public abstract string ExecuteFirst();

    public abstract string ExecuteSecond();

    protected static string[] ReadLines(bool real, int day, int? subDay = null)
    {
        var inputDir = real ? "RealInput" : "TestInput";
        var filePath = subDay.HasValue ? $"{inputDir}\\{day}.{subDay}.txt" : $"{inputDir}\\{day}.txt";
        return File.ReadAllLines(filePath);
    }

    public char[][] GetCharMatrix()
        => Lines.Select(l => l.ToCharArray()).ToArray();
}