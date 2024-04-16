
using static nk.LoggerModule;
using static nk.GoalsModule;
using static nk.SubstModule;

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

    public static IEnumerable<object> RunAll(Func<Key, Goal> f) // p 177
    {
        return RunN(int.MaxValue, f);
    }

    public static IEnumerable<object> RunAll(Func<Key, Goal[]> f) // p 177
    {
        return RunN(int.MaxValue, f);
    }

    public static IEnumerable<object> RunAll(Func<Key, Key, Goal> f) // p 177
    {
        var s = new Substitution();
        var x = s.Fresh(2);

        return RunGoal(s, int.MaxValue, x[0], x[1], f(x[0], x[1]));
    }

    public static IEnumerable<object> RunAll(Func<Key, Key, Goal[]> f) // p 177
    {
        var s = new Substitution();
        var x = s.Fresh(2);

        return RunGoal(s, int.MaxValue, x[0], x[1], f(x[0], x[1]));
    }

    public static IEnumerable<object> RunAll(Func<Key, Key, Key, Goal> f) // p 177
    {
        var s = new Substitution();
        var x = s.Fresh(3);

        return RunGoal(s, int.MaxValue, x[0], x[1], x[2], f(x[0], x[1], x[2]));
    }

    public static IEnumerable<object> RunAll(uint nk, Func<Key, Key[], Goal> f) // p 177
    {
        return RunN(int.MaxValue, nk, f);
    }

    public static IEnumerable<T> RunAll<T>(uint nk, Func<Key, Key[], Goal> f) where T : class
    {
        return RunN(int.MaxValue, nk, f).As<T>();
    }

    #endregion PUBLIC
}
