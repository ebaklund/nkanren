
using static nk.Logging.LoggerModule;
using static nk.GoalsModule;
using nk.Utils;

namespace nk.Goals;

public static class EqoModule
{

    public static Goal Eqo(object o1, object o2) // p 154
    {
        return (Situation s) =>
        {
            LogDebug($"Eqo({s})");
            return Consolidator.TryConsolidate(s, o1, o2) ? Succ()(s) : Fail()(s);
        };
    }

}

