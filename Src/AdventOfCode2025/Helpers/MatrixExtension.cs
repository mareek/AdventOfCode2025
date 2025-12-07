namespace AdventOfCode2025.Helpers;

internal static class MatrixExtension
{
    extension<T>(T[][] matrix) where T : struct
    {
        public int Height => matrix.Length;

        public int Width => matrix[0].Length;

        public T GetCell(int row, int col)
        {
            if (row < 0 || matrix.Height <= row || col < 0 || matrix.Width <= col)
                return default;

            return matrix[row][col];
        }

        public void SetCell(int row, int col, T val)
        {
            if (row < 0 || matrix.Height <= row || col < 0 || matrix.Width <= col)
                return;

            matrix[row][col] = val;
        }
    }
}
