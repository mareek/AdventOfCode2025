using System.Diagnostics;
using AdventOfCode2025.Days;

const bool Real = true;

Day[] days =
    [
    new Day1(Real),
    new Day2(Real),
    new Day3(Real),
    new Day4(Real),
    new Day5(Real),
    new Day6(Real),
    new Day7(Real),
    ];

foreach (var day in days)
{
    if (day.Slow)
        Console.WriteLine($"Day {day.DayDate} : Too slow");
    else
    {
        //Warmup
        _ = day.Lines;

        var chrono = Stopwatch.StartNew();
        var firstResponse = day.ExecuteFirst();
        chrono.Stop();
        var firstDuration = chrono.ElapsedMilliseconds;

        chrono = Stopwatch.StartNew();
        var secondResponse = day.ExecuteSecond();
        chrono.Stop();
        var secondDuration = chrono.ElapsedMilliseconds;

        Console.WriteLine($"Day {day.DayDate} : {firstResponse} ({firstDuration:#0} ms) | {secondResponse} ({secondDuration:#0} ms)");
    }
}
