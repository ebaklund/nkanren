
using static nk.LoggerModule;
using static nk.SubstModule;

namespace nk;


public static partial class GoalsModule
{
    // PUBLIC
   
    public static Goal Succ() // p 154
    {
        return (Substitution s) =>
        {
            LogDebug($"Succ({s})");
            return Enumerable.Repeat(s, 1);
        };
    }
}
