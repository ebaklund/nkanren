
namespace nk;

public delegate IEnumerator<IStreamItem> Goal(Subst input);

public static class Goals
{
    public static Goal Succ() // p 154
    {
        return (Subst s) => Enumerable.Repeat(s, 1).GetEnumerator();;
    }

    public static Goal Fail() // p 154
    {
        return (Subst s) => Enumerable.Repeat(s, 0).GetEnumerator();;
    }

    public static Goal Eqo(object o1, object o2) // p 154
    {
        return (Subst s1) =>
        {
            var s2 = s1.Clone();
            return s2.Unify(o1, o2) ? Succ()(s2) : Fail()(s2);
        };
    }
    
    public static Goal Disj(params Goal[] gs) // p 177
    {
        return (gs.Length) switch
        {
            0 => Succ(),
            1 => gs[0],
            _ => Disj2(gs[0], Disj(gs[1..]))
        };
    }

    public static Goal Disj2(Goal g1, Goal g2) // p 156
    {
        return (Subst s) => g1(s).AppendInf(g2(s));
    }

    public static Goal Conj(params Goal[] gs) // p 177
    {
        return (gs.Length) switch
        {
            0 => Succ(),
            1 => gs[0],
            _ => Conj2(gs[0], Conj(gs[1..]))
        };
    }

    public static Goal Conj2(Goal g1, Goal g2) // p 163
    {
        IEnumerator<IStreamItem> _Conj2(Subst s) 
        {
            return g2(s).FlatMapInf(g1);
        }

        return _Conj2;
    }

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
            yield return new Suspension(() => Disj2(Succ(), Alwayso())(s));
        }

        return _Alwayso;
    }

    public static Goal Ifte(Goal g1, Goal g2, Goal g3) // 173
    {
        IEnumerator<IStreamItem> _Ifte(Subst s)
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
        IEnumerator<IStreamItem> _Once(Subst s)
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