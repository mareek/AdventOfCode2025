using System.Runtime.InteropServices;
using System.Text;

namespace AdventOfCode2025.Days;

internal class Day6(bool real) : Day(real)
{
    public override int DayDate => 6;

    public override string ExecuteFirst()
    {
        var matrix = Lines.Select(l => l.Split(' ', StringSplitOptions.RemoveEmptyEntries))
                          .ToArray();

        long totalResult = 0;
        var operandCount = matrix.Length - 1;
        var operatorsLine = matrix.Last();
        for (int i = 0; i < operatorsLine.Length; i++)
        {
            var op = operatorsLine[i];
            long columnResult = long.Parse(matrix[0][i]);

            for (int j = 1; j < operandCount; j++)
            {
                var operand = long.Parse(matrix[j][i]);
                switch (op)
                {
                    case "+":
                        columnResult += operand;
                        break;
                    case "*":
                        columnResult *= operand;
                        break;
                    default:
                        throw new ArgumentException($"Unknown Operator {op}");
                }
            }

            totalResult += columnResult;
        }

        return totalResult.ToString();
    }

    public override string ExecuteSecond()
    {
        long totalResult = 0;
        var operatorsLine = Lines.Last();
        int columnCount = Lines[0].Length;
        List<long> operands = [];
        char op = default;
        for (int col = 0; col <= columnCount; col++)
        {
            if (TryReadCol(col, out var operand))
            {
                if (operands.Count == 0)
                    op = operatorsLine[col];

                operands.Add(operand);
            }
            else
            {
                long columnResult = operands[0];

                for (int row = 1; row < operands.Count; row++)
                {
                    operand = operands[row];
                    switch (op)
                    {
                        case '+':
                            columnResult += operand;
                            break;
                        case '*':
                            columnResult *= operand;
                            break;
                        default:
                            throw new ArgumentException($"Unknown Operator {op}");
                    }
                }

                totalResult += columnResult;

                operands = [];
            }
        }

        return totalResult.ToString();

        bool TryReadCol(int col, out long value)
        {
            if (Lines[0].Length <= col)
            {
                value = 0;
                return false;
            }

            List<char> digits = [];
            for (int row = 0; row < Lines.Length - 1; row++)
            {
                var digit = Lines[row][col];
                if (char.IsDigit(digit))
                    digits.Add(digit);
            }

            if (digits.Count > 0)
            {
                value = long.Parse(CollectionsMarshal.AsSpan(digits));
                return true;
            }

            value = 0;
            return false;
        }
    }
}
