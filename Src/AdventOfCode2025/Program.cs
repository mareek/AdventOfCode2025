using AdventOfCode2025.Days;

const bool Real = true;

Day[] days =
    [
    new Day1() { Real = Real },
    ];

foreach (var day in days)
{
    Console.WriteLine($"Day {day.DayDate} : {day.ExecuteFirst()} | {day.ExecuteSecond()}");
}
