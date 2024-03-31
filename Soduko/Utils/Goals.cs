
using nk;
using static nk.Utils.LoggerModule;

namespace Soduko.Utils;

internal static class Goals
{
    // PRIVATE

    private static int[] CountNumbers(Subst s, params object[][] numberGroups)
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
        IEnumerator<Subst> _Onceo(Subst s)
        {
            LogDebug($"Onceo({s}, {k})");

            if (s.Walk(k) is int)
            {
                yield return s;
            }

            var dim = numberGroups[0].Length;
            var counts = CountNumbers(s, numberGroups);
            Subst?[] ss = new Subst[dim];

            for (var num = 0; num < dim; ++num)
            {
                ss[num] = (counts[num] == 0) ? s.CloneWith(k, num) : null;
            }

            for (var num = 0; num < dim; ++num)
            {
                if (ss[num] is Subst rs)
                {
                    yield return rs;
                }
            }
        };

        return _Onceo;
    }
}
