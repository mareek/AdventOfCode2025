namespace AdventOfCode2025.Days;

internal abstract class Day
{
    protected Day(bool real)
    {
        Real = real;
        _lazyLines = new(() => ReadLines(real, DayDate));
    }

    private readonly Lazy<string[]> _lazyLines;

    public string[] Lines => _lazyLines.Value;

    public bool Real { get; }

    public bool Slow { get; init; } = false;

    public abstract int DayDate { get; }

    public abstract string ExecuteFirst();

    public abstract string ExecuteSecond();

    private static string[] ReadLines(bool real, int day)
    {
        var inputDir = real ? "RealInput" : "TestInput";
        var filePath = $"{inputDir}\\{day}.txt";
        return File.ReadAllLines(filePath);
    }
}