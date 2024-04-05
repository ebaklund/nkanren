
using nk;
using static nk.Logging.LoggerModule;
using static nk.GoalsModule;
using static Sudoku.Utils.BoardExt;

namespace Sudoku;

internal static partial class GoalsModule
{
    // PRIVATE

    private static int _callCount = 0;

    private static int[] CountNumbers(Situation s, params object[][] numberGroups)
    {
        var dim = numberGroups[0].Length;
        var counts = new int[dim];

        for (var j = 0; j < numberGroups.Length; ++j)
        {
            var group = numberGroups[j];

            for (var i = 0; i < dim; ++i)
            {
                var w = s.Walk(group[i]);

                if (w is int num)
                {
                    counts[num] += 1;
                }
            }
        }

        return counts;
    }

    // PUBLIC

    public static Goal Once(Key k, params object[][] numberGroups)
    {
        IEnumerator<Situation> _Once(Situation s)
        {
            LogDebug($"Once({s}, {k}) #{++_callCount}");

            if(s.IsDefined(k))
            {
                yield return s;
                yield break;
            }

            var dim = numberGroups[0].Length;
            var counts = CountNumbers(s, numberGroups);

            for (var i = 0; i < dim; ++i)
            {
                if (counts[i] > 0)
                {
                    continue;
                }

                Situation res;
                if (!s.TryCloneWith(k, i, out res))
                {
                    continue;
                }

                yield return res;
            }
        };

        return _Once;
    }
}
