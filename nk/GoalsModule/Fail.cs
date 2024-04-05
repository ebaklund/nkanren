
using static nk.LoggerModule;

namespace nk;


public static partial class GoalsModule
{
    // PUBLIC
   
    public static Goal Fail() // p 154
    {
        return (Situation s) =>
        {
            LogDebug($"Fail({s})");
            return Enumerable.Repeat(s, 0).GetEnumerator();
        };
    }
}
