
using System.Linq;

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

    private static IEnumerable<List<object>> RunGoal(Substitution s, int nt, Key x, Key y, Goal g)
    {
        return g(s)
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

    // PUBLIC

    public static IEnumerable<object> RunN(int nt, uint nk, Func<Key, Key[], Goal> f) // p 169
    {
        var s = new Substitution();
        var q = s.Fresh();

        return RunGoal(s, nt, q, f(q, s.Fresh(nk)));
    }

    public static IEnumerable<object> RunN(int nt, Func<Key, Goal> f) // p 169
    {
        var s = new Substitution();
        var q = s.Fresh();

        return RunGoal(s, nt, q, f(q));
    }
}
