﻿
using KellermanSoftware.CompareNetObjects;
using System;
using System.Collections.Specialized;

namespace nkanren;

public class Key {};

public class Subst
{
    // PRIVATE

    private OrderedDictionary _dict = new();

    // PUBLIC

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

    public object? Walk(object? obj)
    {
        while (obj is Key k && _dict[k] is not null)
        {
            obj = _dict[k];
        }

        return obj;
    }

    public bool Occurs(Key k, object? obj)
    {
        return Walk(obj) switch
        {
            Key asKey => asKey == k,
            Tuple<object?, object?> t => Occurs(k, t.Item1) || Occurs(k, t.Item2),
            List<object?> asList => Occurs(k, asList.Car()) || Occurs(k, asList.Cdr()),
            _ => false
        };
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