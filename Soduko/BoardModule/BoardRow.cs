using nk;

namespace Sudoku;
using static Sudoku.RunnerModule;

public static partial class BoardModule
{
    public static class BoardRow
    {
        public static uint RowIdxOf(uint dim, uint cellIdx)
        {
            return cellIdx / dim;
        }

        public static IEnumerator<object> RowCells(object[] cells, uint dim, uint r)
        {
            var c0 = r * dim;

            for (uint c = c0; c < dim; c++)
            {
                yield return cells[c];
            }
        }
    }
}
