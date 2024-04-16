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
    // PRIVATE

    private static IEnumerable<object> RunGoal(Substitution s, int nt, Key q, Goal g)
    {
        return g(s)
            .Take(nt)
            .Select(
                s2 => s2.GetResolved(q)
            );
    }

    private static IEnumerable<object> RunGoal(Substitution s, int nt, Key q, Goal[] gs)
    {
        return Conj(gs)(s)
            .Take(nt)
            .Select(
                s2 => s2.GetResolved(q)
            );
    }

    private static IEnumerable<List<object>> RunGoal(Substitution s, int nt, Key x, Key y, Goal g)
    {
        return g(s)
            .Take(nt)
            .Select<Substitution, List<object>>(
                s2 => [s2.GetResolved(x), s2.GetResolved(y)]
             );
    }

    private static IEnumerable<List<object>> RunGoal(Substitution s, int nt, Key x, Key y, Goal[] gs)
    {
        return Conj(gs)(s)
            .Take(nt)
            .Select<Substitution, List<object>>(
                s2 => [s2.GetResolved(x), s2.GetResolved(y)]
             );
    }

    private static IEnumerable<List<object>> RunGoal(Substitution s, int nt, Key x, Key y, Key z, Goal g)
    {
        return g(s)
            .Take(nt)
            .Select<Substitution, List<object>>(
                s2 => [s2.GetResolved(x), s2.GetResolved(y), s2.GetResolved(z)]
             );
    }
}