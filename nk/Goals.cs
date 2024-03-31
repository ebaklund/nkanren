
using static nk.Utils.LoggerModule;

namespace nk;


public delegate IEnumerator<Subst> Goal(Subst input);

public static class Goals
{
    public static Goal Succ() // p 154
    {
        return (Subst s) =>
        {
            LogDebug($"Succ({s})");
            return Enumerable.Repeat(s, 1).GetEnumerator();
        };
    }

    public static Goal Fail() // p 154
    {
        return (Subst s) =>
        {
            LogDebug($"Fail({s})");
            return Enumerable.Repeat(s, 0).GetEnumerator();
        };
    }

    public static Goal Eqo(object o1, object o2) // p 154
    {
        return (Subst s1) =>
        {
            LogDebug($"Eqo({s1})");
            var s2 = s1.Clone();
            return s2.Unify(o1, o2) ? Succ()(s2 ) : Fail()(s2 );
        };
    }
    
    public static Goal Disj(params Goal[] gs) // p 177
    {
        return gs.Length switch
        {
            0 => Succ(),
            1 => gs[0],
            _ => (Subst s) =>
            {
                LogDebug($"Disj({s})");
                return gs[0](s.Clone()).AppendInf(gs[1..].Select(g => g(s.Clone())).ToArray());
            }
        };
    }

    public static Goal Conj(params Goal[] gs) // p 177
    {
        return (Subst s) =>
        {
            LogDebug($"Conj({s}, *{gs.Length})");

            return gs.Length switch
            {
                0 => Succ()(s),
                1 => gs[0](s),
                _ => Conj(gs[1..])(s).MapInf(gs[0])
            };
        };
    }
}