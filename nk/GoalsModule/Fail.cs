
using static nk.LoggerModule;
using static nk.SubstModule;

namespace nk;


public static partial class GoalsModule
{
    // PUBLIC
   
    public static Goal Fail() // p 154
    {
        return (Substitution s) =>
        {
            LogDebug($"Fail({s})");
            return Enumerable.Repeat(s, 0).GetEnumerator();
        };
    }
}
