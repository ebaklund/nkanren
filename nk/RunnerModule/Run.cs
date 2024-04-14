
using System.Linq;

using static nk.GoalsModule;
using static nk.LoggerModule;
using static nk.SubstModule;

namespace nk;


public static partial class RunnerModule
{
    // PRIVATE

    private static IEnumerable<object> RunGoal(Substitution s, int nt, Key q, Goal g)
    {
        return g(s).Take(nt).Select(s2 => s2.GetResolved(q));
    }

    private static IEnumerable<T> As<T>(this IEnumerable<object> objects)
    {
        foreach (var o in objects)
        {
            if(o is T obj)
            {
                yield return obj;
            } else
            {
                LogError($"Removed from stream: Failed to convert from \"{o.GetType().Name}\" to \"{typeof(T).Name}\".");
            }
        }
    }

    // PUBLIC

    public static IEnumerable<object> Run(int nt, Func<Key, Goal> f) // p 169
    {
        var s = new Substitution();
        var q = s.Fresh();

        return RunGoal(s, nt, q, f(q));
    }

    public static IEnumerable<object> Run(int nt, uint nk, Func<Key, Key[], Goal> f) // p 169
    {
        var s = new Substitution();
        var q = s.Fresh();

        return RunGoal(s, nt, q, f(q, s.Fresh(nk)));
    }

    public static IEnumerable<object> RunAll(Func<Key, Goal> f) // p 177
    {
        return Run(int.MaxValue, f);
    }

    public static IEnumerable<object> RunAll(uint nk, Func<Key, Key[], Goal> f) // p 177
    {
        return Run(int.MaxValue, nk, f);
    }

    
    public static IEnumerable<T> RunAll<T>(uint nk, Func<Key, Key[], Goal> f) where T : class
    {
        return Run(int.MaxValue, nk, f).As<T>();
    }
}
