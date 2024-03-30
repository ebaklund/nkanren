using nk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soduko;

internal static class AreaGoals
{
    /*
    // PRIVATE

    IEnumerator<int[]> Permute(int i, int[] vals, bool[] fresh)
    {
        if (i >= vals.Length)
        {
            yield return vals;
        }

        if (fresh[i])
        {
            return Permute(++i, vals, fresh);
        }

        for (var e = Permute(++i, vals, fresh); e.MoveNext();)
        {
            yield return e.Current;
        }
    }

    // PUBLIC

    public static Goal Area(object[] cells)
    {
        IEnumerator<IStreamItem> _Area(Subst s)
        {
            cells = cells.Select(c => s.Walk(c)).ToArray();
        }

        return _Area;
    }
*/
}
