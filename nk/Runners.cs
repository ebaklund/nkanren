
namespace nk;

public static class Runners
{
    // PRIVATE

    private static IEnumerator<object> RunGoal(Subst s, uint nt, Key q, Goal g)
    {
        var stream = g(s).FlattenInf();

        for(var i = 0; (i < nt) && stream.MoveNext();)
        {
            yield return stream.Current.WalkRec(q);
        }
    }

    // PUBLIC

    public static IEnumerator<object> Run(uint nt, Func<Key, Goal> f) // p 169
    {
        var s = new Subst();
        var q = s.Fresh();

        return RunGoal(s, nt, q, f(q));
    }

    public static IEnumerator<object> Run(uint nt, uint nk, Func<Key, Key[], Goal> f) // p 169
    {
        var s = new Subst();
        var q = s.Fresh();

        return RunGoal(s, nt, q, f(q, s.Fresh(nk)));
    }

    public static IEnumerator<object> RunAll(Func<Key, Goal> f) // p 177
    {
        return Run(int.MaxValue, f);
    }

    public static IEnumerator<object> RunAll(uint nk, Func<Key, Key[], Goal> f) // p 177
    {
        return Run(int.MaxValue, nk, f);
    }
}
