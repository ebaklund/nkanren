
using KellermanSoftware.CompareNetObjects;
using System;
using System.Collections.Specialized;

namespace nkanren;

using Subst = List<object?>;

public enum Key { }

public static class SubstExt
{
    // PRIVATE

    private static object? Walk(this Subst s, object? o) // p 148
    {
        while (o is Key k && s[(int)k] is not null)
        {
            o = s[(int)k];
        }

        return o;
    }

    private static bool Occurs(this Subst s, Key k, object? o) // p 149
    {
        return s.Walk(o) switch
        {
            Key ok                     => ok == k,
            Tuple<object?, object?> ot => s.Occurs(k, ot.Item1) || s.Occurs(k, ot.Item2),
            List<object?> ol           => s.Occurs(k, ol.Car()) || s.Occurs(k, ol.Cdr()),
            _                          => false
        };
    }

    private static bool Bind(this Subst s, Key k, object? o) // p 149
    {
        if (s.Occurs(k, o))
        {
            return false;
        }

        s[(int)k] = o;

        return true;
    }

    private static bool Unify(this Subst s, object? o1, object? o2) // p 151
    {
        return 
            (o1 == o2) ? true
            : (o1 is Key k1) ? s.Bind(k1, o2)
            : (o2 is Key k2) ? s.Bind(k2, o1)
            : (o1 is List<object?> l1 && o2 is List<object?> l2) ? s.Unify(l1.Car(), l2.Car()) && s.Unify(l1.Cdr(), l2.Cdr())
            : (o1 is ValueTuple<object?, object?> t1 && o1 is ValueTuple<object?, object?> t2) 
                ? s.Unify(t1.Item1, t2.Item1) && s.Unify(t1.Item2, t2.Item2)
            : false;
    }

    // PUBLIC
    
    public static Key Fresh(this Subst s) // p 145
    {
        s.Add(null);

        return (Key) s.Count - 1;
    }

    public static bool TryUnify(this Subst s1, out Subst s2, object? o1, object? o2) // p 151
    {
        s2 = s1.ShallowCopy();
        var isUnified = s2.Unify(o1, o2);

        if (!isUnified)
        {
            s2 = s1;
        }

        return isUnified;
    }
}