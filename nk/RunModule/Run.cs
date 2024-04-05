using nk.Utils;

namespace nk;

public static partial class RunModule
{
    // PRIVATE

    private static IEnumerator<object> RunGoal(Situation s, uint nt, Key q, Goal g)
    {
        //var st = g(s).FlattenInf();
        var st = g(s);

        for (var i = 0; i < nt && st.MoveNext();)
        {
            var o1 = st.Current.Get(q);
            var o2 = Observer.Observe(st.Current, q);
            yield return o2;
        }
    }

    // PUBLIC

    public static IEnumerator<object> Run(uint nt, Func<Key, Goal> f) // p 169
    {
        var s = new Situation();
        var q = s.Fresh();

        return RunGoal(s, nt, q, f(q));
    }

    public static IEnumerator<object> Run(uint nt, uint nk, Func<Key, Key[], Goal> f) // p 169
    {
        var s = new Situation();
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
