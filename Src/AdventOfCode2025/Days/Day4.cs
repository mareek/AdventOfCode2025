namespace AdventOfCode2025.Days;

internal class Day4(bool real) : Day(real)
{
    public override int DayDate => 4;

    public override string ExecuteFirst()
    {
        var input = Lines.Select(s => s.ToCharArray()).ToArray();

        int count = 0;
        for (int y = 0; y < input.Length; y++)
        {
            var lineLength = input[y].Length;
            for (int x = 0; x < lineLength; x++)
            {
                if (IsAccessible(input, x, y))
                    count++;
            }
        }

        return count.ToString();
    }

    public override string ExecuteSecond()
    {
        var input = Lines.Select(s => s.ToCharArray()).ToArray();

        int totalCount = 0;
        int currentCount;

        do
        {
            currentCount = 0;
            for (int y = 0; y < input.Length; y++)
            {
                var lineLength = input[y].Length;
                for (int x = 0; x < lineLength; x++)
                {
                    if (IsAccessible(input, x, y))
                    {
                        input[y][x] = 'x';
                        currentCount++;
                    }
                }
            }

            totalCount += currentCount;
        } while (currentCount > 0);
        
        return totalCount.ToString();
    }

    private static bool IsEmpty(char[][] input, int x, int y)
    {
        if (y < 0 || input.Length <= y || x < 0 || input[y].Length <= x)
            return true;

        return input[y][x] != '@';
    }

    private static bool IsAccessible(char[][] input, int x, int y)
    {
        if (input[y][x] != '@')
            return false;

        var neighboorCount = 0;
        for (int xB = x - 1; xB <= x + 1; xB++)
        {
            for (int yB = y - 1; yB <= y + 1; yB++)
            {
                if (xB == x && yB == y)
                    continue;
                if (!IsEmpty(input, xB, yB))
                    neighboorCount++;
            }
        }

        return neighboorCount < 4;
    }
}
