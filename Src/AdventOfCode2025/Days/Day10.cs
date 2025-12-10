using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2025.Days;

internal class Day10(bool real) : Day(real)
{
    public override int DayDate => 10;

    public override string ExecuteFirst()
    {
        var machines = Lines.Select(Machine.Parse).ToArray();
        throw new NotImplementedException();
    }

    public override string ExecuteSecond()
    {
        return "";
    }

    public class Machine(string indicatorLights, int[][] buttons, int[] joltageRequirements)
    {
        public bool[] IndicatorLights { get; } = indicatorLights.Select(c => c == '#').ToArray();
        public int[][] Buttons { get; } = buttons;
        public int[] JoltageRequirements { get; } = joltageRequirements;

        public static Machine Parse(string line)
        {
            // [.##.] (3) (1,3) (2) (2,3) (0,2) (0,1) {3,5,4,7}
            var closingBracketPos = line.IndexOf(']');
            var indicatorLights = line[1..closingBracketPos];

            var openingCurlyBracePos = line.IndexOf('{');
            var buttons = line[(closingBracketPos + 1)..openingCurlyBracePos].Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                                                             .Select(g => g[1..^1].Split(',').Select(int.Parse).ToArray())
                                                                             .ToArray();

            var closingCurlyBracePos = line.IndexOf('}');
            var joltageRequirements = line[(openingCurlyBracePos + 1)..closingCurlyBracePos].Split(',')
                                                                                            .Select(int.Parse)
                                                                                            .ToArray();

            return new(indicatorLights, buttons, joltageRequirements);
        }
    }
}
