using AdventOfCode2025.Helpers;

namespace AdventOfCode2025.Days;

internal class Day7(bool real) : Day(real)
{
    public override int DayDate => 7;

    public override string ExecuteFirst()
    {
        var matrix = GetCharMatrix();

        // Draw the beam
        int splitCount = 0;
        for (int row = 0; row < matrix.Height; row++)
        {
            for (int col = 0; col < matrix.Width; col++)
            {
                var currentCell = matrix.GetCell(row, col);
                var upperCell = matrix.GetCell(row - 1, col);
                if (currentCell == '.' && (upperCell == '|' || upperCell == 'S'))
                    matrix.SetCell(row, col, '|');
                else if (currentCell == '^' && upperCell == '|')
                {
                    matrix.SetCell(row, col - 1, '|');
                    matrix.SetCell(row, col + 1, '|');
                    splitCount += 1;
                }
            }
        }

        return splitCount.ToString();
    }

    public override string ExecuteSecond()
    {
        var charMatrix = GetCharMatrix();
        long[][] valMatrix = Enumerable.Range(0, charMatrix.Height)
                                       .Select(_ => new long[charMatrix.Width])
                                       .ToArray();

        // Draw the beam
        for (int row = 0; row < charMatrix.Height; row++)
        {
            for (int col = 0; col < charMatrix.Width; col++)
            {
                var upperValCell = valMatrix.GetCell(row - 1, col);
                var currentCharCell = charMatrix.GetCell(row, col);

                if (currentCharCell == 'S')
                    valMatrix.SetCell(row, col, 1);
                else if (currentCharCell == '.')
                {
                    var currentValCell = valMatrix.GetCell(row, col);
                    valMatrix.SetCell(row, col, currentValCell + upperValCell);
                }
                else if (currentCharCell == '^')
                {
                    int leftCol = col - 1;
                    var leftValCell = valMatrix.GetCell(row, leftCol);
                    valMatrix.SetCell(row, leftCol, leftValCell + upperValCell);

                    int rightCol = col + 1;
                    var rightValCell = valMatrix.GetCell(row, rightCol);
                    valMatrix.SetCell(row, rightCol, rightValCell + upperValCell);
                }
            }
        }

        return valMatrix.Last().Sum().ToString();
    }
}
