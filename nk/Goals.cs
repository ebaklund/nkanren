
namespace nk;

public delegate IEnumerator<IStreamItem> Goal(Subst input);

public static class Goals
{
    public static Goal Succ() // p 154
    {
        IEnumerator<Subst> _Succ(Subst s)
        {
            yield return s;
        }

        return _Succ;
    }

    public static Goal Fail() // p 154
    {
        IEnumerator<Subst> _Fail(Subst s) 
        {
            yield break;
        }

        return _Fail;
    }

    public static Goal Eqo(object u, object v) // p 154
    {
        IEnumerator<IStreamItem> _Eqo(Subst s)
        {
            var res = s.TryUnify(out Subst s2, u, v);
            return res ? Succ()(s2) : Fail()(s2);
        }

        return _Eqo;
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
        IEnumerator<IStreamItem> _Disj2(Subst s) 
        {
            return g1(s).AppendInf(g2(s));
        }

        return _Disj2;
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

    public static Goal Fresh(Func<Key, Goal> f) // P 174
    {
        IEnumerator<IStreamItem> _Fresh(Subst s)
        {
            return f(s.Fresh())(s);
        };

        return _Fresh;
    }
}