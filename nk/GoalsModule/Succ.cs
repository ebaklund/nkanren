
using static nk.LoggerModule;

namespace nk;


public static partial class GoalsModule
{
    // PUBLIC
   
    public static Goal Succ() // p 154
    {
        return (Situation s) =>
        {
            LogDebug($"Succ({s})");
            return Enumerable.Repeat(s, 1).GetEnumerator();
        };
    }
}
