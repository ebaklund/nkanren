
namespace nk;
using static nk.LoggerModule;


public static partial class GoalsModule
{
    // PUBLIC
   
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
}
