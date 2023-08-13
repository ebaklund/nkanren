
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

    public static Goal Conj2(Goal g1, Goal g2) // p 156
    {
        return (Subst s) => g1(s).AppendMap(g2);
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
}