
using nk;
using static nk.GoalsModule;
using static nk.SubstModule;

namespace nk.Examples;


internal class ExampleGoals
{
    #if false
    public static Goal Nevero() // p 157
    {
        IEnumeraable<IStreamItem> _Nevero(Subst s) 
        {
            yield return new Suspension(() => Nevero()(s));
        }

        return _Nevero;
    }

    public static Goal Alwayso() // p 159
    {
        IEnumerable<IStreamItem> _Alwayso(Subst s)
        {
            yield return new Suspension(() => Disj(Succ(), Alwayso())(s));
        }

        return _Alwayso;
    }
    #endif

    public static Goal Once(Goal g) // P 174
    {
        return (Substitution s) => g(s).Take(1);
    }
}
