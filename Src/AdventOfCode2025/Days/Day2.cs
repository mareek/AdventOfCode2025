namespace AdventOfCode2025.Days;

internal class Day2(bool real) : Day(real)
{
    public override int DayDate => 2;

    public override string ExecuteFirst()
    {
        long response = 0;
        var input = Lines[0];
        var ranges = input.Split(',');
        foreach (var range in ranges)
        {
            var bornes = range.Split('-');
            var lowStr = bornes[0];
            var low = long.Parse(lowStr);
            var highStr = bornes[1];
            var high = long.Parse(highStr);
            for (int digitCount = lowStr.Length; digitCount <= highStr.Length; digitCount++)
            {
                if (digitCount % 2 == 0)
                {
                    var firstHalf = (digitCount == lowStr.Length)
                                            ? long.Parse(lowStr[..(digitCount / 2)])
                                            : (long)Math.Pow(10, digitCount / 2 - 1);
                    var factor = (long)Math.Pow(10, digitCount / 2);
                    for (long i = 0; i < (factor - firstHalf); i++)
                    {
                        var number = (firstHalf + i) * factor + firstHalf + i;
                        if (number < low)
                            continue;
                        if (high < number)
                            break;

                        response += number;
                    }
                }
            }
        }

        return response.ToString();
    }

    public override string ExecuteSecond()
    {

        var input = Lines[0];
        var ranges = input.Split(',');

        List<long> invalidIds = [];

        foreach (var range in ranges)
        {
            var bornes = range.Split('-');
            var lowStr = bornes[0];
            var low = long.Parse(lowStr);
            var highStr = bornes[1];
            var high = long.Parse(highStr);

            for (int digitCount = lowStr.Length; digitCount <= highStr.Length; digitCount++)
            {
                for (int sequenceLength = 1; sequenceLength < digitCount; sequenceLength++)
                {
                    if (digitCount % sequenceLength == 0)
                    {
                        int sequenceCount = digitCount / sequenceLength;
                        var firstPart = (digitCount == lowStr.Length)
                                                ? long.Parse(lowStr[..sequenceLength])
                                                : (long)Math.Pow(10, sequenceLength - 1);
                        var sequenceMax = (long)Math.Pow(10, sequenceLength);
                        for (long sequence = firstPart; sequence < sequenceMax; sequence++)
                        {
                            long number = 0;
                            for (int pow = 0; pow < sequenceCount; pow++)
                                number += sequence * (long)Math.Pow(10, pow * sequenceLength);

                            if (number < low)
                                continue;
                            if (high < number)
                                break;

                            invalidIds.Add(number);
                        }
                    }
                }
            }
        }

        return invalidIds.Distinct().Sum().ToString();
    }
}
