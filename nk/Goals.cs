
namespace nk;

public delegate IEnumerator<IStreamItem> Goal(Subst input);

public static class Goals
{
    public static Goal Succ() // p 154
    {
        return (Subst s) => Enumerable.Repeat(s, 1).GetEnumerator();;
    }

    public static Goal Fail() // p 154
    {
        return (Subst s) => Enumerable.Repeat(s, 0).GetEnumerator();;
    }

    public static Goal Eqo(object o1, object o2) // p 154
    {
        return (Subst s) => s.Unify(o1, o2) ? Succ()(s) : Fail()(s);
    }
    
    public static Goal Disj(params Goal[] gs) // p 177
    {
        return gs.Length switch
        {
            0 => Succ(),
            1 => gs[0],
            _ => (Subst s) => gs[0](s.Clone()).AppendInf(gs[1..].Select(g => g(s.Clone())).ToArray())
        };
    }

    public static Goal Conj(params Goal[] gs) // p 177
    {
        return gs.Length switch
        {
            0 => Succ(),
            1 => gs[0],
            _ => (Subst s) => Conj(gs[1..])(s).FlatMapInf(gs[0])
        };
    }
}