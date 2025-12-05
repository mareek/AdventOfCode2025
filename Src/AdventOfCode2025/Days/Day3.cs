namespace AdventOfCode2025.Days;

internal class Day3(bool real) : Day(real)
{
    public override int DayDate => 3;

    public override string ExecuteFirst()
    {
        int response = 0;
        foreach (var line in Lines)
        {
            int firstDigitPos = -1;
            int firstDigit = 0;
            for (int i = 0; i < line.Length - 1; i++)
            {
                var digit = int.Parse([line[i]]);
                if (firstDigit < digit)
                {
                    firstDigit = digit;
                    firstDigitPos = i;
                }

                if (firstDigit == 9)
                    break;
            }

            var secondDigit = 0;
            for (int i = firstDigitPos + 1; i < line.Length; i++)
            {
                var digit = int.Parse([line[i]]);
                if (secondDigit < digit)
                    secondDigit = digit;
            }

            response += firstDigit * 10 + secondDigit;
        }

        return response.ToString();
    }

    public override string ExecuteSecond()
    {
        long response = 0;
        foreach (var line in Lines)
        {
            int[] digits = new int[12];

            int digitPos = -1;
            for (int digitNum = 0; digitNum < digits.Length; digitNum++)
            {
                int digitVal = 0;
                int upperBound = line.Length - digits.Length + digitNum;
                for (int i = digitPos + 1; i <= upperBound; i++)
                {
                    var digit = int.Parse([line[i]]);
                    if (digitVal < digit)
                    {
                        digitVal = digit;
                        digitPos = i;
                    }

                    if (digitVal == 9)
                        break;
                }

                digits[digitNum] = digitVal;
            }
            var joltageStr = string.Join("", digits);
            var joltage = long.Parse(joltageStr);

            response += joltage;
        }

        return response.ToString();
    }
}
