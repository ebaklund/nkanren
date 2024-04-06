
using nk;
using static nk.LoggerModule;
using static nk.GoalsModule;
using static Sudoku.BoardModule;

namespace Sudoku;

public static partial class GoalsModule
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

    private static uint[] CountNumbers(Situation s, uint dim, object[] siblings)
    {
        var counts = new uint[dim];

        for (var i = 0; i < siblings.Length; ++i)
        {
            var w = s.Walk(siblings[i]);

            if (w is int num)
            {
                counts[num] += 1;
            }
        }

        return counts;
    }
/*
    private static int[] CountNumbers2(Situation s, params object?[][] numberGroups)
    {
        var dim = numberGroups[0].Length;
        var counts = new int[dim];

        for (var j = 0; j < numberGroups.Length; ++j)
        {
            var group = numberGroups[j];

            for (var i = 0; i < dim; ++i)
            {
                var w = group[i];

                if (w is int num)
                {
                    counts[num] += 1;
                }
            }
        }

        return counts;
    }
*/

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

    public static Goal Once(Key k, uint dim, object[] siblings)
    {
        IEnumerator<Situation> _Once(Situation s)
        {
            LogDebug($"Once({s}, {k}) #{++_callCount}");

            if(s.IsDefined(k))
            {
                yield return s;
                yield break;
            }

            var counts = CountNumbers(s, dim, siblings);

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
