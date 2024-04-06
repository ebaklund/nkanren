
using nk;
using static nk.LoggerModule;
using static nk.GoalsModule;

namespace Sudoku;
using static Sudoku.BoardModule;


public static partial class RunnerModule
{
    // PRIVATE



    // PUBLIC

    public static IEnumerator<Board> RunSudoku(uint dim, Func<Key[], Board> f)
    {
        var s = new Situation();
        var q = s.Fresh();
        var board = f(s.Fresh(dim*dim));

        yield return board;
    }
}