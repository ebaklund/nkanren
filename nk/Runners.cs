
namespace nk;

public static class Runners
{
    // PRIVATE

    private static IEnumerator<object> RunGoal(uint n, Func<Subst, Key, Goal> f) // p 169
    {
        var s = new Subst();
        var q = s.Fresh();
        var g = f(s, q);
        var stream = g(s).FlattenInf();

        for(var i = 0; (i < n) && stream.MoveNext();)
        {
            yield return stream.Current.Walk(q);
        }
    }

    // PUBLIC

    public static IEnumerator<object> Run(uint n, Func<Key, Goal> f) // p 169
    {
        return RunGoal(n, (s, q) => f(q));
    }

    public static IEnumerator<object> Run(uint n, Func<Key, Key, Goal> f) // p 169
    {
        return RunGoal(n, (s, q) => f(q, s.Fresh()));
    }

    public static IEnumerator<object> Run(uint n, Func<Key, Key, Key, Goal> f) // p 169
    {
        return RunGoal(n, (s, q) => f(q, s.Fresh(), s.Fresh()));
    }

    public static IEnumerator<object> RunAll(Func<Key, Goal> f) // p 177
    {
        return Run(int.MaxValue, f);
    }

    public static IEnumerator<object> RunAll(Func<Key, Key, Goal> f) // p 177
    {
        return Run(int.MaxValue, f);
    }

    public static IEnumerator<object> RunAll(Func<Key, Key, Key, Goal> f) // p 177
    {
        return Run(int.MaxValue, f);
    }
}