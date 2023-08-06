
using KellermanSoftware.CompareNetObjects;
using System;
using System.Collections.Specialized;

namespace nkanren;

public record Key(int Idx);

public class Subst
{
    // PRIVATE

    private List<object?> _slots = new();

    // PUBLIC

    public Key Fresh() // p 145
    {
        Key k = new Key(_slots.Count);
        _slots.Add(null);

        return k;
    }

    public object? Walk(object? obj) // p 148
    {
        while (obj is Key k && _slots[k.Idx] is not null)
        {
            obj = _slots[k.Idx];
        }

        return obj;
    }

    public bool Occurs(Key k, object? obj) // p 149
    {
        return Walk(obj) switch
        {
            Key asKey => asKey == k,
            Tuple<object?, object?> t => Occurs(k, t.Item1) || Occurs(k, t.Item2),
            List<object?> asList => Occurs(k, asList.Car()) || Occurs(k, asList.Cdr()),
            _ => false
        };
    }

    public bool Bind(Key k, object? v) // p 149
    {
        if (Occurs(k, v))
        {
            return false;
        }

        _slots[k.Idx] = v;

        return true;
    }

    public static (bool, Exception?) IsValidSubstitution((Key, object?)[] args)
    {
        for (int i = 0; i < args.Length; ++i)
        {
            Key ki = args[i].Item1;

            for (int j = 0; j < args.Length; ++j)
            {
                if (i == j && args[i].Item2 is Key vi && ki == vi)
                {
                    return (false, new ArgumentException($"Invalid substitution. Key is self referencing: [{i}], [{j}]."));
                }

                if (i != j && args[i].Item1 is Key k && ki == k)
                {
                    return (false, new ArgumentException($"Invalid substitution. Key reoccurs as key: [{i}], [{j}]."));
                }
            }
        }

        return (true, null);
    }



    public (bool, Exception) Extend((Key, object?)[] args, int i, int j)
    {
        var ki = args[i].Item1;
        var vi = Walk(args[i].Item2);

        return Extend(args, 0, 0);
    }

    public (bool, Exception) Extend((Key, object?)[] args)
    {
        return Extend(args, 0, 0);
    }
}