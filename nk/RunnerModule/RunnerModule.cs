using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static nk.GoalsModule;
using static nk.SubstModule;

namespace nk;

public static partial class RunnerModule
{
    #region PRIVATE

    private static IEnumerable<object> RunGoal(Substitution s, int nt, Key q, params Goal[] gs)
    {
        return Conj(gs)(s)
            .Take(nt)
            .Select(
                s2 => s2.GetResolved(q)
            );
    }

    private static IEnumerable<List<object>> RunGoal(Substitution s, int nt, Key[] ks, params Goal[] gs)
    {
        return Conj(gs)(s)
            .Take(nt)
            .Select(
                s2 => ks.Select(k => s2.GetResolved(k)).ToList()
             );
    }

    #endregion PRIVATE
}