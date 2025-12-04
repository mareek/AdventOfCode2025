using System.Diagnostics;
using AdventOfCode2025.Days;

const bool Real = true;

Day[] days =
    [
    new Day1() { Real = Real },
    new Day2() { Real = Real },
    new Day3() { Real = Real },
    new Day4() { Real = Real },
    ];

foreach (var day in days)
{
    if (day.Slow)
        Console.WriteLine($"Day {day.DayDate} : Too slow");
    else
    {
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
