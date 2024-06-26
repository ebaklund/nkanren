﻿
using static nk.GoalsModule;

namespace nk;

public static partial class RunnerModule
{
    public static Goal Fresh(uint n, Func<Key[], Goal> f)
    {
        return (s) => f(s.Fresh(n))(s);
    }

    public static Goal Fresh(uint n, Func<Key[], Goal[]> f)
    {
        return (s) => Conj(f(s.Fresh(n)))(s);
    }

    public static Goal Fresh(Func<Key, Goal> f)
    {
        return (s) => f(s.Fresh())(s);
    }

    public static Goal Fresh(Func<Key, Goal[]> f)
    {
        return (s) => Conj(f(s.Fresh()))(s);
    }

    public static Goal Fresh(Func<Key, Key, Goal> f)
    {
        return (s) => f(s.Fresh(), s.Fresh())(s);
    }

    public static Goal Fresh(Func<Key, Key, Goal[]> f)
    {
        return (s) => Conj(f(s.Fresh(), s.Fresh()))(s);
    }

    public static Goal Fresh(Func<Key, Key, Key, Goal> f)
    {
        return (s) => f(s.Fresh(), s.Fresh(), s.Fresh())(s);
    }

    public static Goal Fresh(Func<Key, Key, Key, Goal[]> f)
    {
        return (s) => Conj(f(s.Fresh(), s.Fresh(), s.Fresh()))(s);
    }
}
