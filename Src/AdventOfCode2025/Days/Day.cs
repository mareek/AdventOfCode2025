namespace AdventOfCode2025.Days;

internal abstract class Day
{
    public required bool Real { get; init; }

    public bool Slow { get; init; } = false;

    public abstract int DayDate { get; }

    public abstract string ExecuteFirst();

    public abstract string ExecuteSecond();

    protected string[] ReadLines()
    {
        var inputDir = Real ? "RealInput" : "TestInput";
        var filePath = $"{inputDir}\\{DayDate}.txt";
        return File.ReadAllLines(filePath);
    }
}