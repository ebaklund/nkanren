
using System.Linq;

using static nk.GoalsModule;
using static nk.SubstModule;

namespace nk;


public static partial class RunnerModule
{
    // PUBLIC

    public static IEnumerable<object> RunN(int nt, uint nk, Func<Key, Key[], Goal> f) // p 169
    {
        var s = new Substitution();
        var q = s.Fresh();

        return RunGoal(s, nt, q, f(q, s.Fresh(nk)));
    }

    public static IEnumerable<object> RunN(int nt, uint nk, Func<Key, Key[], Goal[]> f) // p 169
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

    public static IEnumerable<object> RunN(int nt, Func<Key, Goal[]> f) // p 169
    {
        var s = new Substitution();
        var q = s.Fresh();

        return RunGoal(s, nt, q, f(q));
    }
}
