
using static nk.LoggerModule;
using nk.Utils;

namespace nk;


public delegate IEnumerator<Situation> Goal(Situation input);

public static partial class GoalsModule
{
    public static Goal Succ() // p 154
    {
        return (Situation s) =>
        {
            LogDebug($"Succ({s})");
            return Enumerable.Repeat(s, 1).GetEnumerator();
        };
    }

    public static Goal Fail() // p 154
    {
        return (Situation s) =>
        {
            LogDebug($"Fail({s})");
            return Enumerable.Repeat(s, 0).GetEnumerator();
        };
    }
    
    public static Goal Disj(params Goal[] gs) // p 177
    {
        return gs.Length switch
        {
            0 => Succ(),
            1 => gs[0],
            _ => (Situation s) =>
            {
                LogDebug($"Disj({s})");
                return gs[0](s.Clone()).AppendInf(gs[1..].Select(g => g(s.Clone())).ToArray());
            }
        };
    }

    public static Goal Conj(params Goal[] gs) // p 177
    {
        return (Situation s) =>
        {
            LogDebug($"Conj({s}, g*{gs.Length})");

            return gs.Length switch
            {
                0 => Succ()(s),
                1 => gs[0](s),
                _ => Conj(gs[1..])(s).MapInf(gs[0])
            };
        };
    }
}