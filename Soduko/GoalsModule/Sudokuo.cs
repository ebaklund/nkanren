
using nk;
using static nk.LoggerModule;
using static nk.GoalsModule;

namespace Sudoku;


public static partial class GoalsModule
{
    // PRIVATE

    private static int _sudokuCallCount = 0;

    // PUBLIC

    public static Goal Sudokuo(Key[] ks, object[][] sudokuBoard)
    {
        IEnumerator<Situation> _sudoku(Situation s)
        {
            LogDebug($"Sudokuo() #{++_sudokuCallCount}");

            yield return s;
        }

        return _sudoku;
    }
}