
namespace nk;

public static class Runners
{
    // PRIVATE

    // PUBLIC

    public static IEnumerator<object> Run(uint n, Func<Key, Goal> f) // p 169
    {
        var s = new Subst();
        var k = s.Fresh();
        var g = f(k);
        var stream = g(s).FlattenInf();

        for(var i = 0; (i < n) && stream.MoveNext();)
        {
            yield return stream.Current.WalkRec(k);
        }
    }

    public static IEnumerator<object[]> Run(uint n, Func<Key, Key, Goal> f) // p 169
    {
        var s = new Subst();
        var ks = s.Fresh(2);
        var g = f(ks[0], ks[1]);
        var stream = g(s).FlattenInf();

        for(var i = 0; (i < n) && stream.MoveNext();)
        {
            yield return new object[]
            {
                stream.Current.WalkRec(ks[0]),
                stream.Current.WalkRec(ks[1])
            };
        }
    }

    public static IEnumerator<object[]> Run(uint n, Func<Key, Key, Key, Goal> f) // p 169
    {
        var s = new Subst();
        var ks = s.Fresh(3);
        var g = f(ks[0], ks[1], ks[2]);
        var stream = g(s).FlattenInf();

        for(var i = 0; (i < n) && stream.MoveNext();)
        {
            yield return new object[]
            {
                stream.Current.WalkRec(ks[0]),
                stream.Current.WalkRec(ks[1]),
                stream.Current.WalkRec(ks[2])
            };
        }
    }

    public static IEnumerator<object> RunAll(Func<Key, Goal> f) // p 177
    {
        return Run(int.MaxValue, f);
    }

    public static IEnumerator<object[]> RunAll(Func<Key, Key, Goal> f) // p 177
    {
        return Run(int.MaxValue, f);
    }

    public static IEnumerator<object[]> RunAll(Func<Key, Key, Key, Goal> f) // p 177
    {
        return Run(int.MaxValue, f);
    }
}