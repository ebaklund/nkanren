
using KellermanSoftware.CompareNetObjects;
using System;
using System.Collections.Specialized;

namespace nkanren;

using Subst = List<object?>;

public static class SubstExt
{
    // PRIVATE

    private static object? Walk(this Subst s, object? o) // p 148
    {
        // Returns value, Key value or fresh Key

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
    
    public static Goal CallFresh(this Subst s, Func<Key, Goal> f) // p 165
    {
        return f(s.Fresh());
    }

    public static bool TryUnify(this Subst s1, out Subst s2, object? o1, object? o2) // p 151
    {
        s2 = s1.ToList();
        var isUnified = s2.Unify(o1, o2);

        if (!isUnified)
        {
            s2 = s1;
        }

        return isUnified;
    }

    public static object? ReifyTree(this Subst s, object? o) // 167
    {
        o = s.Walk(o);

        // Substitute all Keys in tree, which by protocol are fresh, with Key reified name
        if (o is Key k)
        {            
            // ISSUE: The original algorithm reifies by the squence fresh variables occurs in o.
            // Not sure that is what we want. 
            // Alternatively we could reify by fresh variables' creation index. i.e. (int)Key

            // r.Add((r.Count - 1).ToString());
            return ((int)k).ToString();
        }
        else if (o is List<object?> l )
        {
            List<object?> r = new ();

            foreach (object? i in l)
            {
                r.Add(ReifyTree(s, i));
            }

            return r;            
        }

        return o;
    }

    public static object? WalkRec(this Subst s, object? o) // p 166
    {
        // Returns copy of input tree with all variables resolved leaving only fresh variables unresolved.
        
        return s.Walk(o) switch
        {
            Key k => k, // Unresolved since it is fresh
            List<object> l => l.Select(x => s.WalkRec(x)), // Resolve recursively
            _ => o // Resolved
        };
    }

    public static Subst Reify(this Subst r, object? o)
    {
        // Creates a new substitution whith variable names.
        // Can be used in a Goal that generates "r", but is otherwise of questionable use.
        // A function that does direct substitution in a tre may be more effective.

        o = r.Walk(o);

        if (o is Key k)
        {
            r.Add("_" + (r.Count - 1).ToString());
        }
        else if (o is List<object?> l)
        {
            foreach(var i in l)
            {
                r.Reify(i);
            }
        }

        return r;
    }
}
