using nk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soduko;

internal static class AreaGoals
{
    // PRIVATE
/*
    private static bool IsAbsent(this object[] a, Subst s, int val)
    {
        for (int i = 0; i < a.Length; i++)
        {
            if (s.Walk(a[i]) is int v && v == val)
            {
                return false;
            }
        }

        return true;
    }

    // PUBLIC
    
    public static Goal Exclusivo2(Key k, object[] row, object[] col, object[] lot) // p 154
    {
        return (Subst s) => 
        {
            var n = row.Length;
            var w = s.Walk(k);

            for (var i = 0; i < n; ++i)
            {
                if (w is Key kFresh)
                {
                    if (row.IsAbsent(s, i) || col.IsAbsent(s, i) || lot.IsAbsent(s, i))
                    {
                        yield return i;
                    }

                }
            }
        };
    }

 
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
