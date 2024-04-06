
using nk;
using static nk.LoggerModule;
using static nk.GoalsModule;
using static nk.RunnerModule;

namespace Sudoku;
using static Sudoku.BoardModule;
using static Sudoku.GoalsModule;

public static partial class RunnerModule
{
    // PRIVATE



    // PUBLIC

    public static IEnumerator<Board> RunSudoku(uint dim, Func<Key[], Board> f)
    {
        return RunAll<Board>(4, (q, ks) => {
            var board = f(ks);
            var CellIdx = (Key k) => (uint)(k.Idx - ks[0].Idx); // Calculate cell index from cell key.

            List<Goal> constaints = 
                ks.ToList()
                .Select(k => CellConstraint(k, board.Dim, board.PeersOfCellAt(CellIdx(k)).AsArray()))
                .ToList();

            constaints.Add(Equal(q, board));

            return Conj(constaints.ToArray());
        });
    }
}