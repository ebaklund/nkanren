
namespace nk;

public class Freshs
{
    public static Goal Fresh(Func<Key, Goal> f) // P 174
    {
        return s => f(s.Fresh())(s);
    }

    public static Goal Fresh(Func<Key, Key, Goal> f) // P 174
    {
        return s => f(s.Fresh(), s.Fresh())(s);
    }

    public static Goal Fresh(Func<Key, Key, Key, Goal> f) // P 174
    {
        return s => f(s.Fresh(), s.Fresh(), s.Fresh())(s);
    }
}
