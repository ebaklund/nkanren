
using KellermanSoftware.CompareNetObjects;
using System;
using System.Collections.Specialized;

namespace nkanren;

public record Key(int Idx);

public class Subst
{
    // PRIVATE

    private List<object?> _slots = new();

    private static object? Walk(object? obj, List<object?> slots) // p 148
    {
        while (obj is Key k && slots[k.Idx] is not null)
        {
            obj = slots[k.Idx];
        }

        return obj;
    }

    private static bool Occurs(Key k, object? obj, List<object?> slots) // p 149
    {
        return Walk(obj, slots) switch
        {
            Key asKey => asKey == k,
            Tuple<object?, object?> t => Occurs(k, t.Item1, slots) || Occurs(k, t.Item2, slots),
            List<object?> asList => Occurs(k, asList.Car(), slots) || Occurs(k, asList.Cdr(), slots),
            _ => false
        };
    }

    private static bool Bind(Key k, object? v, List<object?> slots) // p 149
    {
        if (Occurs(k, v, slots))
        {
            return false;
        }

        slots[k.Idx] = v;

        return true;
    }

    private static bool Unify(object? u, object? v, List<object?> slots) // p 151
    {
        if (u == v)
        {
            return true;
        }

        if (u is Key ku)
        {
            Bind(ku, v, slots);
            return true;
        }

        if (v is Key kv)
        {
            Bind(kv, u, slots);
            return true;
        }

        if (v is List<object?> lv && u is List<object?> lu)
        {
            return Unify(lv.Car(), lu.Car(), slots) && Unify(lv.Cdr(), lu.Cdr(), slots);
        }

        if (v is ValueTuple<object?, object?> tv && u is ValueTuple<object?, object?> tu)
        {
            return Unify(tv.Item1, tu.Item1, slots) && Unify(tv.Item2, tu.Item2, slots);
        }

        return false;
    }

    // PUBLIC
    
    public Key Fresh() // p 145
    {
        Key k = new Key(_slots.Count);
        _slots.Add(null);

        return k;
    }

    public bool Unify(object? u, object? v) // p 151
    {
        var newSlots = _slots.ShallowCopy();

        if (!Unify(u, v, newSlots))
        {
            return false;
        }

        _slots = newSlots;

        return true;
    }
}