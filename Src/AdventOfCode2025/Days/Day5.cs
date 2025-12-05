using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode2025.Days;

internal class Day5 : Day
{
    public override int DayDate => 5;

    public override string ExecuteFirst()
    {
        var lines = ReadLines();
        var ranges = lines.TakeWhile(l => !string.IsNullOrEmpty(l))
                          .Select(Range.Parse)
                          .ToArray();
        var ingredients = lines.Skip(ranges.Length + 1)
                               .Select(long.Parse)
                               .ToArray();

        List<long> freshIngredients = [];
        foreach (var ingredient in ingredients)
        {
            if (ranges.Any(r => r.Contains(ingredient)))
                freshIngredients.Add(ingredient);
        }

        return freshIngredients.Count.ToString();
    }

    public override string ExecuteSecond()
    {
        var ranges = ReadLines().TakeWhile(l => !string.IsNullOrEmpty(l))
                                .Select(Range.Parse)
                                .ToList();
        bool hasMerged;
        do
        {
            hasMerged = false;
            for (int i = 0; i < ranges.Count; i++)
            {
                var range = ranges[i];
                int j = i + 1;
                while(j < ranges.Count)
                {
                    if (!range.TryMerge(ranges[j], out var merged))
                        j++;
                    else
                    {
                        hasMerged = true;
                        range = merged;
                        ranges.RemoveAt(j);
                    }
                }

                ranges[i] = range;
            }
        } while (hasMerged);

        return ranges.Sum(r => r.Size).ToString();
    }

    private class Range(long low, long high)
    {
        public long Low => low;

        public long High => high;

        public long Size => high - low + 1;

        public bool Contains(long val) => low <= val && val <= high;


        public bool TryMerge(Range other, [MaybeNullWhen(false)] out Range merged)
        {
            if (Low <= other.High && other.Low <= High)
            {
                merged = new(Math.Min(Low, other.Low), Math.Max(High, other.High));
                return true;
            }

            merged = null;
            return false;
        }

        public static Range Parse(string line)
        {
            var split = line.Split('-');
            return new(long.Parse(split[0]), long.Parse(split[1]));
        }

        public override string ToString()
            => $"{Low}-{High}";
    }
}
