
using static nk.GoalsModule;
using static nk.SubstModule;
using static nk.LoggerModule;

namespace nk;

public static partial class GoalsModule
{
    public static Goal Conde(params Goal[][] gsa)
    {
        return (Substitution s) =>
        {
            LogDebug($"Conde({s})");
            return Disj(gsa.Select(gs => Conj(gs)).ToArray())(s);
        };
    }
}
