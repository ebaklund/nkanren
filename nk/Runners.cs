
namespace nk;

using static Goals;

public static class Runners
{
    public static IEnumerator<IStreamItem> RunGoal(Goal g) // p 169
    {
        return g(new Subst());
    }

    public static IEnumerator<object> Run(uint n, Func<Key, Goal> f) // p 169
    {
        var s = new Subst();
        var q = s.Fresh();
        var g = f(q);
        var r = g(s).FlattenInf();

        for(var i = 0; (i < n) && r.MoveNext();)
        {
            if (r.Current.Walk(q) is not object obj)
            {
                continue;
            }

            yield return obj;
        }
    }

    public static IEnumerator<object> RunAll(Func<Key, Goal> f) // p 177
    {
        return Run(UInt32.MaxValue, f);
    }
}