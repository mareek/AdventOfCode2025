namespace AdventOfCode2025.Days;

internal class Day1 : Day
{
    public override int DayDate => 1;

    public override string ExecuteFirst()
    {
        var lines = ReadLines();
        int position = 50;
        int zeroCOunt = 0;
        foreach (var line in lines.Where(l => !string.IsNullOrWhiteSpace(l)))
        {
            var sign = line[0] == 'L' ? -1 : 1;
            var clicks = int.Parse(line[1..]);
            var move = sign * clicks;
            position = (position + move + 100) % 100;
            if (position == 0)
                zeroCOunt++;
        }

        return $"{zeroCOunt}";
    }

    public override string ExecuteSecond()
    {
        var lines = ReadLines();
        int position = 50;
        int zeroCount = 0;
        foreach (var line in lines.Where(l => !string.IsNullOrWhiteSpace(l)))
        {
            var sign = line[0] == 'L' ? -1 : 1;
            var clicks = int.Parse(line[1..]);
            if (clicks >= 100)
                zeroCount += (clicks / 100);

            var move = sign * (clicks % 100);
            var fakePosition = position + move;
            var newPosition = (position + move + 100) % 100;
            if (newPosition == 0)
                zeroCount++;
            else if (position != 0 && ((fakePosition < 0) || (100 < fakePosition)))
                zeroCount++;
            position = newPosition;
        }

        return $"{zeroCount}";
    }
}
