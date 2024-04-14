
using nk;
using static nk.LoggerModule;
using static nk.GoalsModule;
using static nk.RunnerModule;

namespace Sudoku;
using static Sudoku.BoardModule;
using static Sudoku.GoalsModule;

public static partial class RunnerModule
{
    // PUBLIC

    public static IEnumerable<Board> RunSudoku(uint cellCount, Func<Key[], Board> f)
    {
        return RunAll<Board>(cellCount, (q, ks) => {
            var board = f(ks);
            var CellIdx = (Key k) => (uint)(k.Idx - ks[0].Idx); // Calculate cell index from cell key.

            List<Goal> constaints = 
                ks.ToList()
                .Select(cellKey => Conj(
                    SudokuConstraint(cellKey, board.BoardDim, board.PeersOfCellAt(CellIdx(cellKey)).ToArray()),
                    Equal(cellKey, board.GetCellValue(CellIdx(cellKey)))
                ))
                .ToList();

            constaints.Add(Equal(q, board));

            return Conj(constaints.ToArray());
        });
    }
}