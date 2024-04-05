namespace nk;

public static partial class RunModule
{
    public static Goal Fresh(object[] init, Func<Key[], Goal> f)
    {
        return (s) => f(s.Fresh(init))(s);
    }

    public static Goal Fresh(uint n, Func<Key[], Goal> f)
    {
        return (s) => f(s.Fresh(n))(s);
    }

    public static Goal Fresh(Func<Key, Goal> f)
    {
        return (s) => f(s.Fresh())(s);
    }

    public static Goal Fresh(Func<Key, Key, Goal> f)
    {
        return (s) => f(s.Fresh(), s.Fresh())(s);
    }

    public static Goal Fresh(Func<Key, Key, Key, Goal> f)
    {
        return (s) => f(s.Fresh(), s.Fresh(), s.Fresh())(s);
    }
}
