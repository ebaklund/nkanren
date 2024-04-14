
using System.Linq;

using static nk.GoalsModule;
using static nk.LoggerModule;
using static nk.SubstModule;

namespace nk;


public static partial class RunnerModule
{
    // PRIVATE

    private static object[] GetResolved(Substitution s, Key[] ks)
    {
        var a = new object[ks.Length];

        for (int i = 0; i< ks.Length; ++i)
        {
            a[i] = GetResolved(s, ks[i]);
        }

        return a;
    }

    private static object[] GetResolved(Substitution s, object[] a)
    {
        var a2 = new object[a.Length];

        for (int i = 0; i < a.Length; i++)
        {
            a2[i] = GetResolved(s, a[i]);
        }
    
        return a2;
    }

    private static object[][] GetResolved(Substitution s, object[][] m1)
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

    private static IResolvable GetResolved(Substitution s, IResolvable r)
    {
        var resolved = GetResolved(s, r.GetResolvable());
        var w = r.Wrap(resolved);

        return w;
    }

    private static object GetResolved(Substitution s, object o)
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

    private static IEnumerable<object> RunGoal(Substitution s, int nt, Key q, Goal g)
    {
        return g(s).Take(nt).Select(s2 => GetResolved(s2, q));
    }

    private static IEnumerable<T> As<T>(this IEnumerable<object> objects)
    {
        foreach (var o in objects)
        {
            if(o is T obj)
            {
                yield return obj;
            } else
            {
                LogError($"Removed from stream: Failed to convert from \"{o.GetType().Name}\" to \"{typeof(T).Name}\".");
            }
        }
    }

    // PUBLIC

    public static IEnumerable<object> Run(int nt, Func<Key, Goal> f) // p 169
    {
        var s = new Substitution();
        var q = s.Fresh();

        return RunGoal(s, nt, q, f(q));
    }

    public static IEnumerable<object> Run(int nt, uint nk, Func<Key, Key[], Goal> f) // p 169
    {
        var s = new Substitution();
        var q = s.Fresh();

        return RunGoal(s, nt, q, f(q, s.Fresh(nk)));
    }

    public static IEnumerable<object> RunAll(Func<Key, Goal> f) // p 177
    {
        return Run(int.MaxValue, f);
    }

    public static IEnumerable<object> RunAll(uint nk, Func<Key, Key[], Goal> f) // p 177
    {
        return Run(int.MaxValue, nk, f);
    }

    
    public static IEnumerable<T> RunAll<T>(uint nk, Func<Key, Key[], Goal> f) where T : class
    {
        return Run(int.MaxValue, nk, f).As<T>();
    }
}
