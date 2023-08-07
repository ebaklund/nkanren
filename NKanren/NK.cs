
namespace nkanren;

public delegate List<object> Goal(Subst input);

public static class NK
{
    public static Goal Succ() // p 154
    {
        return (Subst s) => new List<object>() { s };
    }

    public static Goal Fail() // p 154
    {
        return (Subst s) => new List<object>();
    }

    public static Goal Equal(object? u, object? v) // p 154
    {
        return (Subst s) => s.Unify(u, v)
            ? Succ()(s) 
            : Fail()(s);
    }

    public static Goal Disj2(Goal g1, Goal g2) // p 156
    {
        return (Subst s) => g1(s).Append(g2(s));
    }
}