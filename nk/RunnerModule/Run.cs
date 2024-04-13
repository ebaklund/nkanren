
using static nk.GoalsModule;
using static nk.LoggerModule;

namespace nk;


public static partial class RunnerModule
{
    // PRIVATE

    private static object[] GetResolved(Situation s, Key[] ks)
    {
        var a = new object[ks.Length];

        for (int i = 0; i< ks.Length; ++i)
        {
            a[i] = GetResolved(s, ks[i]);
        }

        return a;
    }

    private static object[] GetResolved(Situation s, object[] a)
    {
        var a2 = new object[a.Length];

        for (int i = 0; i < a.Length; i++)
        {
            a2[i] = GetResolved(s, a[i]);
        }
    
        return a2;
    }

    private static object[][] GetResolved(Situation s, object[][] m1)
    {
        var m2 = new object[m1.Length][];

        for (int j = 0; j < m1.Length; j++)
        {
            m2[j] = new object[m1[j].Length];
    
            for (int i = 0; i < m1.Length; i++)
            {
                m2[j][i] = GetResolved(s, m1[j][i]);
            }
        }

        return m2;
    }

    private static IResolvable GetResolved(Situation s, IResolvable r)
    {
        var resolved = GetResolved(s, r.GetResolvable());
        var w = r.Wrap(resolved);

        return w;
    }

    private static object GetResolved(Situation s, object o)
    {
        var w = s.Walk(o);

        return w switch
        {
            Key k          => k, // Return unresolved fresh keys as is.
            string str     => str,
            object v when v.GetType().IsValueType => v,
            object[][] m   => GetResolved(s, m),
            Key[] ks       => GetResolved(s, ks),
            List<object> l => l.Select(o => GetResolved(s, o)).ToList(),
            IResolvable  r => GetResolved(s, r),
            //IResolvable  r => r,
            object[] a     => GetResolved(s, a),
            object x       => throw new ApplicationException("Unknown observable.")
        };
    }

    private static IEnumerator<object> RunGoal(Situation s, uint nt, Key q, Goal g)
    {
        //var st = g(s).FlattenInf();
        var st = g(s);

        for (var i = 0; i < nt && st.MoveNext();)
        {
            var o1 = st.Current.Get(q);
            var o2 = GetResolved(st.Current, q);
            yield return o2;
        }
    }

    private static IEnumerator<T> As<T>(this IEnumerator<object> objects)
    {
        while (objects.MoveNext())
        {
            if(objects.Current is T obj)
            {
                yield return obj;
            } else
            {
                LogError($"Removed from stream: Failed to convert from \"{objects.Current.GetType().Name}\" to \"{typeof(T).Name}\".");
            }
        }
    }

    // PUBLIC

    public static IEnumerator<object> Run(uint nt, Func<Key, Goal> f) // p 169
    {
        var s = new Situation();
        var q = s.Fresh();

        return RunGoal(s, nt, q, f(q));
    }

    public static IEnumerator<object> Run(uint nt, uint nk, Func<Key, Key[], Goal> f) // p 169
    {
        var s = new Situation();
        var q = s.Fresh();

        return RunGoal(s, nt, q, f(q, s.Fresh(nk)));
    }

    public static IEnumerator<object> RunAll(Func<Key, Goal> f) // p 177
    {
        return Run(int.MaxValue, f);
    }

    public static IEnumerator<object> RunAll(uint nk, Func<Key, Key[], Goal> f) // p 177
    {
        return Run(int.MaxValue, nk, f);
    }

    
    public static IEnumerator<T> RunAll<T>(uint nk, Func<Key, Key[], Goal> f) where T : class
    {
        return Run(int.MaxValue, nk, f).As<T>();
    }
}
