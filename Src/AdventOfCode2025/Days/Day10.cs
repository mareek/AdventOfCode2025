using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace AdventOfCode2025.Days;

internal class Day10(bool real) : Day(real)
{
    public override int DayDate => 10;

    public override string ExecuteFirst()
    {
        var machines = Lines.Select(Machine.Parse).ToArray();
        int answer = 0;
        Parallel.ForEach(machines, machine =>
        {
            var optimalCombination = machine.GetOptimalButtonsCombinationForLights();
            Interlocked.Add(ref answer, optimalCombination.Length);
        });

        return answer.ToString();
    }

    public override string ExecuteSecond()
    {
        var machines = Lines.Select(Machine.Parse).ToArray();
        int answer = 0;

        foreach (var chunk in machines.OrderBy(m => m.JoltageRequirements.Max() * m.Buttons.Length).Chunk(4))
        {
            Parallel.ForEach(chunk, machine =>
            {
                var chrono = Stopwatch.StartNew();
                var optimalCombinationLength = machine.GetOptimalButtonsCombinationLengthForJoltage();
                chrono.Stop();
                //Console.WriteLine($"{machine.GetLights()}\t => {optimalCombinationLength} in {chrono.Elapsed.TotalSeconds:#0.0}s");
                Interlocked.Add(ref answer, optimalCombinationLength);
            });
        }

        return answer.ToString();
    }

    public class Machine(string indicatorLights, byte[][] buttons, short[] joltageRequirements)
    {
        public bool[] IndicatorLights { get; } = indicatorLights.Select(c => c == '#').ToArray();
        public byte[][] Buttons { get; } = buttons;
        public short[] JoltageRequirements { get; } = joltageRequirements;

        public string GetLights() => indicatorLights;

        public int[] GetOptimalButtonsCombinationForLights()
        {
            for (int combinationLength = 1; combinationLength <= (Buttons.Length * 2); combinationLength++)
            {
                var combination = new int[combinationLength];
                var dividers = Enumerable.Range(0, combinationLength)
                                         .Select(j => (int)Math.Pow(Buttons.Length, j))
                                         .ToArray();
                int iterationCount = (int)Math.Pow(Buttons.Length, combinationLength);
                for (int i = 0; i < iterationCount; i++)
                {
                    // fill combination
                    for (int j = 0; j < combinationLength; j++)
                    {
                        int button = (int)((i / dividers[j]) % Buttons.Length);
                        combination[j] = button;
                    }

                    if (IsValidLightCombination(combination))
                        return combination;

                }
            }

            throw new Exception("Here be dragons");
        }

        public int GetOptimalButtonsCombinationLengthForJoltage()
        {
            Span<short> initialJoltageState = stackalloc short[JoltageRequirements.Length];
            var maxJoltageRequirement = JoltageRequirements.Max();
            for (int searchDepth = maxJoltageRequirement; searchDepth < 1025; searchDepth += 2)
            {
                int result = Recurse(1, initialJoltageState, searchDepth);
                if (result <= searchDepth)
                    return result;
            }
            throw new Exception("Here be dragons");

            int Recurse(int combinationLength, Span<short> joltageState, int maxLength)
            {
                if (maxLength <= combinationLength)
                    return int.MaxValue;

                Span<short> newJoltageState = stackalloc short[JoltageRequirements.Length];
                int finalResult = int.MaxValue;
                foreach (var button in Buttons)
                {
                    joltageState.CopyTo(newJoltageState);

                    foreach (var lightIndex in button)
                        newJoltageState[lightIndex] += 1;

                    var result = IsValidState(newJoltageState) switch
                    {
                        JoltageLevel.TooHigh => int.MaxValue,
                        JoltageLevel.Equal => combinationLength,
                        JoltageLevel.TooLow => Recurse(combinationLength + 1, newJoltageState, maxLength),
                        _ => throw new Exception()
                    };

                    if (result < finalResult)
                        finalResult = result;

                    if (result < maxLength)
                        maxLength = result;
                }

                return finalResult;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private JoltageLevel IsValidState(Span<short> joltageState)
        {
            var response = JoltageLevel.Equal;
            for (int i = 0; i < JoltageRequirements.Length; i++)
            {
                if (JoltageRequirements[i] < joltageState[i])
                    return JoltageLevel.TooHigh;
                if (joltageState[i] < JoltageRequirements[i])
                    response = JoltageLevel.TooLow;
            }

            return response;
        }

        private bool IsValidLightCombination(int[] buttonsCombination)
        {
            var lightsState = new bool[indicatorLights.Length];
            foreach (var buttonIndex in buttonsCombination)
                foreach (var lightIndex in Buttons[buttonIndex])
                    lightsState[lightIndex] = !lightsState[lightIndex];

            return lightsState.SequenceEqual(IndicatorLights);
        }

        private enum JoltageLevel
        {
            TooLow,
            Equal,
            TooHigh
        }

        public static Machine Parse(string line)
        {
            // [.##.] (3) (1,3) (2) (2,3) (0,2) (0,1) {3,5,4,7}
            var closingBracketPos = line.IndexOf(']');
            var indicatorLights = line[1..closingBracketPos];

            var openingCurlyBracePos = line.IndexOf('{');
            var buttons = line[(closingBracketPos + 1)..openingCurlyBracePos].Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                                                             .Select(g => g[1..^1].Split(',').Select(byte.Parse).ToArray())
                                                                             .ToArray();

            var closingCurlyBracePos = line.IndexOf('}');
            var joltageRequirements = line[(openingCurlyBracePos + 1)..closingCurlyBracePos].Split(',')
                                                                                            .Select(short.Parse)
                                                                                            .ToArray();

            return new(indicatorLights, buttons, joltageRequirements);
        }
    }
}
