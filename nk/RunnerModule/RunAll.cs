
using static nk.LoggerModule;
using static nk.GoalsModule;
using static nk.SubstModule;

namespace nk;

public static partial class RunnerModule
{
    #region PUBLIC

    public static IEnumerable<object> RunAll(Func<Key, Goal> f)
    {
        return RunN(int.MaxValue, f);
    }

    public static IEnumerable<object> RunAll(Func<Key, Goal[]> f)
    {
        return RunN(int.MaxValue, f);
    }

    public static IEnumerable<object> RunAll(Func<Key, Key, Goal> f)
    {
        return RunN(int.MaxValue, f);
    }

    public static IEnumerable<object> RunAll(Func<Key, Key, Goal[]> f)
    {
        return RunN(int.MaxValue, f);
    }

    public static IEnumerable<object> RunAll(Func<Key, Key, Key, Goal> f)
    {
        return RunN(int.MaxValue, f);
    }

    public static IEnumerable<object> RunAll(uint nk, Func<Key, Key[], Goal> f)
    {
        return RunN(int.MaxValue, nk, f);
    }

    public static IEnumerable<object> RunAll(uint nk, Func<Key, Key[], Goal[]> f)
    {
        return RunN(int.MaxValue, nk, f);
    }

    public static IEnumerable<T> RunAll<T>(uint nk, Func<Key, Key[], Goal> f) where T : class
    {
        return RunN<T>(int.MaxValue, nk, f);
    }

    public static IEnumerable<T> RunAll<T>(uint nk, Func<Key, Key[], Goal[]> f) where T : class
    {
        return RunN<T>(int.MaxValue, nk, f);
    }

    #endregion PUBLIC
}
