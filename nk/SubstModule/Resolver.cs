﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nk;

public static partial class SubstModule
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

    private static object[] GetResolved(Substitution s, Array a)
    {
        var a2 = new object[a.Length];

        for (int i = 0; i < a.Length; i++)
        {
            a2[i] = GetResolved(s, a.GetValue(i)!);
        }
    
        return a2;
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

    private static object GetResolved(Substitution s, object o1)
    {
        var w = s.Walk(o1);
        var t = w.GetType();

        return w switch
        {
            Key k          => k, // Return unresolved fresh keys as is.
            string str     => str,
            object v when t.IsValueType => v,
            List<object> l => l.Select(o2 => GetResolved(s, o2)).ToList(),
            object a when t.IsArray => GetResolved(s, (Array) a),
            IResolvable  r => GetResolved(s, r),
            //object[][] m   => GetResolved(s, m),
            //Key[] ks       => GetResolved(s, ks),
            //IResolvable  r => r,
            //object[] a     => GetResolved(s, a),
            object x       => throw new ApplicationException("Unknown observable.")
        };
    }
}
