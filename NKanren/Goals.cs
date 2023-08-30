
namespace nkanren;

using Subst = List<object?>;
using Stream = List<object?>;
public delegate Stream Goal(Subst input);

public static class Goals
{
    public static Goal Succ() // p 154
    {
        return (Subst s) => new Stream() { s };
    }

    public static Goal Fail() // p 154
    {
        return (Subst s) => new Stream();
    }

    public static Goal Equal(object? u, object? v) // p 154
    {
        return (Subst s) => s.TryUnify(out Subst s2, u, v)
            ? Succ()(s2) 
            : Fail()(s2);
    }

    public static Goal Disj2(Goal g1, Goal g2) // p 156
    {
        return (Subst s) => g1(s).Append(g2(s));
    }

    public static Goal Disj(params Goal[] gs) // p 177
    {
        return (gs.Length) switch
        {
            0 => Succ(),
            1 => gs[0],
            _ => Disj2(gs[0], Disj(gs[1..]))
        };
    }

    public static Goal Conj2(Goal g1, Goal g2) // p 156
    {
        return (Subst s) => g1(s).AppendMap(g2);
    }

    public static Goal Conj(params Goal[] gs) // p 177
    {
        return (gs.Length) switch
        {
            0 => Succ(),
            1 => gs[0],
            _ => Conj2(gs[0], Conj(gs[1..]))
        };
    }

    public static Goal Nevero() // p 157
    {
        return (Subst s) => new Stream() { () => Nevero()(s) };
    }

    public static Goal Alwayso() // p 159
    {
        return (Subst s) => new Stream() { () => Disj2(Succ(), Alwayso())(s) };
    }

    public static Goal Reify(object? t1) // p 168, ISSUE: Seems to not to be a true Goal and only exist for demonstration
    {
        return (Subst s) =>
        {
            var t2 = s.WalkRec(t1); // Resolve variables in the tree
            var r = s.Reify(t2); // Collect and name variables in tree by occurance
            var t3 = r.WalkRec(t2); // Replace variables in tree with variavle names
            return new List<object?>() { t3 }; // Is this actually correct? Not clear if input and output are streams.
        };
    }

    public static Goal Ifte(Goal p, Goal t, Goal e) // 173
    {
        return (Subst s) =>
        {
            var st = p(s);
            
            return (st.Count > 0)
                ? st.AppendMap(t)
                : e(s);
        };
    }

    public static Goal Once(Goal g) // P 174
    {
        return (Subst s) =>
        {
            Stream st = g(s);

            return (st.Count == 0)
                ? st
                : new Stream() { st[0] };
        };
    }

    public static Func<Subst, Func<Stream>> defrel(params Goal[] gs) // p 177
    {
        return (Subst s) => () => Conj(gs)(s);
    }
}