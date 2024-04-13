
namespace nk.Examples;
using static nk.GoalsModule;


internal class ExampleGoals
{
    #if false
    public static Goal Nevero() // p 157
    {
        IEnumerator<IStreamItem> _Nevero(Subst s) 
        {
            yield return new Suspension(() => Nevero()(s));
        }

        return _Nevero;
    }

    public static Goal Alwayso() // p 159
    {
        IEnumerator<IStreamItem> _Alwayso(Subst s)
        {
            yield return new Suspension(() => Disj(Succ(), Alwayso())(s));
        }

        return _Alwayso;
    }
    #endif

    public static Goal Ifte(Goal g1, Goal g2, Goal g3) // 173
    {
        IEnumerator<Situation> _Ifte(Situation s)
        {
            var st1 = g1(s);

            if(!st1.MoveNext())
            {
                return g3(s);
            }
            
            return st1.FlatMapInf(g2); 
        };

        return _Ifte;
    }

    public static Goal Once(Goal g) // P 174
    {
        IEnumerator<Situation> _Once(Situation s)
        {
            var st = g(s);

            if (st.MoveNext())
            {
                yield return st.Current;
            }
        };

        return _Once;
    }
}
