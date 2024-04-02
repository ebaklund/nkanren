
using nk;
using static nk.Logging.LoggerModule;

namespace Soduko.Utils;

internal static class Goals
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
                var w = s.Walk(group[j]);

                if (w is int num)
                {
                    counts[num] += 1;
                }
            }
        }

        return counts;
    }

    // PUBLIC

    public static Goal Onceo(Key k, params object[][] numberGroups)
    {
        IEnumerator<Situation> _Onceo(Situation s)
        {
            LogDebug($"Onceo({s}, {k}) #{++_callCount}");

            var dim = numberGroups[0].Length;
            var counts = CountNumbers(s, numberGroups);

            for (var i = 0; i < dim; ++i)
            {
                if (counts[i] == 0)
                {
                    yield return s.CloneWith(k, i);
                }
            }
        };

        return _Onceo;
    }
}
