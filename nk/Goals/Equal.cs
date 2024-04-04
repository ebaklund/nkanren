
using static nk.Logging.LoggerModule;
using static nk.GoalsModule;
using nk.Utils;

namespace nk.Goals;

public static class EqoModule
{

    public static Goal Equal(object o1, object o2) // p 154
    {
        return (Situation s) =>
        {
            LogDebug($"Equal({s})");
            return Consolidator.TryConsolidate(s, o1, o2) ? Succ()(s) : Fail()(s);
        };
    }

}

