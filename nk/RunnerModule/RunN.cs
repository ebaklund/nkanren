
using System.Linq;

using static nk.GoalsModule;
using static nk.SubstModule;
using static nk.LoggerModule;

namespace nk;


public static partial class RunnerModule
{
    #region PRIVATE

    private static IEnumerable<T> As<T>(this IEnumerable<object> objects)
    {
        foreach (var o in objects)
        {
            if(o is T obj)
            {
                yield return obj;
            } else
            {
                LogError($"Object removed from stream: Failed to convert from \"{o.GetType().Name}\" to \"{typeof(T).Name}\".");
            }
        }
    }

    #endregion PRIVATE
    #region PUBLIC

    public static IEnumerable<object> RunN(int nt, Func<Key, Goal> f)
    {
        var s = new Substitution();
        var q = s.Fresh();

        return RunGoal(s, nt, q, f(q));
    }

    public static IEnumerable<object> RunN(int nt, Func<Key, Goal[]> f)
    {
        var s = new Substitution();
        var q = s.Fresh();

        return RunGoal(s, nt, q, f(q));
    }
  
    public static IEnumerable<object> RunN(int nt, Func<Key, Key, Goal> f)
    {
        var s = new Substitution();
        var xs = s.Fresh(2);

        return RunGoal(s, nt, xs, f(xs[0], xs[1]));
    }

    public static IEnumerable<object> RunN(int nt, Func<Key, Key, Goal[]> f)
    {
        var s = new Substitution();
        var xs = s.Fresh(2);

        return RunGoal(s, nt, xs, f(xs[0], xs[1]));
    }

    public static IEnumerable<object> RunN(int nt, Func<Key, Key, Key, Goal> f)
    {
        var s = new Substitution();
        var xs = s.Fresh(3);

        return RunGoal(s, nt, xs, f(xs[0], xs[1], xs[2]));
    }

    public static IEnumerable<object> RunN(int nt, Func<Key, Key, Key, Goal[]> f)
    {
        var s = new Substitution();
        var xs = s.Fresh(3);

        return RunGoal(s, nt, xs, f(xs[0], xs[1], xs[2]));
    }

    public static IEnumerable<object> RunN(int nt, uint nk, Func<Key, Key[], Goal> f)
    {
        var s = new Substitution();
        var q = s.Fresh();

        return RunGoal(s, nt, q, f(q, s.Fresh(nk)));
    }

    public static IEnumerable<object> RunN(int nt, uint nk, Func<Key, Key[], Goal[]> f)
    {
        var s = new Substitution();
        var q = s.Fresh();

        return RunGoal(s, nt, q, f(q, s.Fresh(nk)));
    }
 
    public static IEnumerable<T> RunN<T>(int nt, uint nk, Func<Key, Key[], Goal> f) where T : class
    {
        var s = new Substitution();
        var q = s.Fresh();

        return RunGoal(s, nt, q, f(q, s.Fresh(nk))).As<T>();
    }

    public static IEnumerable<T> RunN<T>(int nt, uint nk, Func<Key, Key[], Goal[]> f) where T : class
    {
        var s = new Substitution();
        var q = s.Fresh();

        return RunGoal(s, nt, q, f(q, s.Fresh(nk))).As<T>();
    }

    #endregion PUBLIC
}
