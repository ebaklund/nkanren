
namespace nk;

public static class Freshes
{
    public static Goal Fresh(uint n, Func<Key[], Goal> f)
    {
        return (Subst s) => f(s.Fresh(n))(s);
    }

    public static Goal Fresh(Func<Key, Goal> f)
    {
        return (Subst s) => f(s.Fresh())(s);
    }

    public static Goal Fresh(Func<Key, Key, Goal> f)
    {
        return (Subst s) => f(s.Fresh(), s.Fresh())(s);
    }

    public static Goal Fresh(Func<Key, Key, Key, Goal> f)
    {
        return (Subst s) => f(s.Fresh(), s.Fresh(), s.Fresh())(s);
    }
}
