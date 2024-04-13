
using nk;
using static nk.LoggerModule;
using static nk.GoalsModule;
using static Sudoku.BoardModule;
using System;

namespace Sudoku;

public static partial class GoalsModule
{
    // PRIVATE

    private static int _callCount = 0;

    private static uint[] CountNumbers(Situation s, uint dim, object[] peers)
    {
        var counts = new uint[dim];

        for (var i = 0; i < peers.Length; ++i)
        {
            var cell = peers[i];
            var content = s.Walk(cell);

            if (content is SudokuNumber num)
            {
                ++counts[num.Value - 1];
            }
        }

        return counts;
    }

    // PUBLIC

    public static Goal SudokuConstraint(Key k, uint dim, object[] peers)
    {
        IEnumerator<Situation> _goal(Situation s)
        {
            LogDebug($"Once({s}, {k}) #{++_callCount}");

            if(s.IsDefined(k))
            {
                yield return s;
                yield break;
            }

            var counts = CountNumbers(s, dim, peers);

            for (uint i = 0; i < dim; ++i)
            {
                if (counts[i] > 0)
                {
                    continue;
                }

                Situation res;
                if (!s.TryCloneWith(k, SudokuNumber.From(i + 1), out res))
                {
                    continue;
                }

                yield return res;
            }
        };

        return _goal;
    }
}
