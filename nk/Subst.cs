
using KellermanSoftware.CompareNetObjects;
using System;
using System.Collections.Specialized;

namespace nk;

public class Subst : IStreamItem
{
    // PRIVATE

    List<object?> _slots;

    private Subst Clone()
    {
        return new Subst(_slots.ToList());
    }

    private bool Occurs(Key k, object? o) // p 149
    {
        return Walk(o) switch
        {
            Key ok                     => ok == k,
            Tuple<object?, object?> ot => Occurs(k, ot.Item1) || Occurs(k, ot.Item2),
            List<object?> ol           => Occurs(k, ol.Car()) || Occurs(k, ol.Cdr()),
            _                          => false
        };
    }

    private bool Unify(object? o1, object? o2) // p 151
    {
        return 
            (o1 == o2) ? true
            : (o1 is Key k1) ? Set(k1, o2)
            : (o2 is Key k2) ? Set(k2, o1)
            : (o1 is List<object?> l1 && o2 is List<object?> l2) ? Unify(l1.Car(), l2.Car()) && Unify(l1.Cdr(), l2.Cdr())
            : (o1 is ValueTuple<object?, object?> t1 && o1 is ValueTuple<object?, object?> t2) 
                ? Unify(t1.Item1, t2.Item1) && Unify(t1.Item2, t2.Item2)
            : false;
    }

    // PUBLIC
    
    public Subst(List<object?> slots)
    {
        _slots = slots;
    }

    public Subst() : this (new List<object?>())
    {
    }

    public Key Fresh() // p 145
    {
        _slots.Add(null);

        return (Key) _slots.Count - 1;
    }

    private bool Set(Key k, object? o) // p 149
    {
        if (Occurs(k, o))
        {
            return false;
        }

        _slots[(int)k] = o;

        return true;
    }
    
    private object? Get(Key k) // p 149
    {
        return WalkRec(k);
    }

    
    public Goal CallFresh(Func<Key, Goal> f) // p 165
    {
        return f(Fresh());
    }

    public  bool TryUnify(out Subst s2, object? o1, object? o2) // p 151
    {
        s2 = this.Clone();
        return s2.Unify(o1, o2);
    }

    public object? ReifyTree(object? o) // 167
    {
        o = Walk(o);

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
                r.Add(ReifyTree(i));
            }

            return r;            
        }

        return o;
    }

    public object? Walk(object? o) // p 148
    {
        // Returns value, Key value or fresh Key

        while (o is Key k && _slots[(int)k] is not null)
        {
            o = _slots[(int)k];
        }

        return o;
    }

    public object? WalkRec(object? o) // p 166
    {
        // Returns copy of input tree with all variables resolved leaving only fresh variables unresolved.
        
        return Walk(o) switch
        {
            Key k => k, // Unresolved since it is fresh
            List<object> l => l.Select(x => WalkRec(x)).ToList(), // Resolve recursively
            _ => o // Resolved
        };
    }

    private static Subst Reify(Subst r, object? o)
    {
        o = r.Walk(o);

        if (o is Key k)
        {
            r._slots.Add("_" + (r._slots.Count - 1).ToString());
        }
        else if (o is List<object?> l)
        {
            foreach(var i in l)
            {
                Reify(r, i);
            }
        }

        return r;
    }

    public Subst Reify(object? o)
    {
        // Creates a new substitution whith variable names.
        // Can be used in a Goal that generates "r", but is otherwise of questionable use.
        // A function that does direct substitution in a tre may be more effective.

        return Reify(new Subst(), o);
    }
}
