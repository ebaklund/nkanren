
namespace nk;

using static nk.GoalsModule;
using static nk.SubstModule;


public static class HooksModule
{
    public static Goal AssertHook(Func<Substitution, (bool, string)> assertHook)
    {
        IEnumerable<Substitution> _AssertHook(Substitution s)
        {
            var (ok, msg) = assertHook(s);

            if (!ok)
            {
                throw new ApplicationException(msg);
            }

            yield return s;
        }
        
        return _AssertHook;
    }

    public static Goal ActionHook(Action<Substitution> action)
    {
        IEnumerable<Substitution> _Action(Substitution s)
        {
            action(s);
            yield return s;
        }
        
        return _Action;
    }

    public static Goal FuncHook(Func<Substitution, Substitution> func)
    {
        IEnumerable<Substitution> _Func(Substitution s)
        {
            yield return func(s);
        }
        
        return _Func;
    }

    public static Goal FuncHook(Func<Substitution, IEnumerable<Substitution>> func)
    {
        return s =>
        {
           return func(s);
        };
    }
}

