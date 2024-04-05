﻿
using static nk.LoggerModule;
using static nk.GoalsModule;

namespace nk;


public class Situation
{
    // PRIVATE

    private static int _maxDefinedCount = 0;
    private static int _idCount = 0;
    private int _id = 0;
    private List<object?> _slots;

    private void  UpdateMaxDefinedCount()
    {
        int _definedKeysCount = 0;

        for (int i = 0; i < _slots.Count; ++i)
        {
            if (_slots[i] != null)
            {
                ++_definedKeysCount;
            }
        }

        if (_maxDefinedCount < _definedKeysCount)
        {
            ++_maxDefinedCount;
            LogInformation($"{_maxDefinedCount}/{_slots.Count} defined");
        }
    }

    private bool Occurs(Key k1, object o2) // p 149
    {
        o2 = Walk(o2);

        if (o2 is Key k2)
        {
            return k1 == k2;
        }

        if (o2 is List<object> l2 )
        {
            var i = 0;
            for(; (i < l2.Count) && !Occurs(k1, l2[i]); ++i);

            return i != l2.Count;
        }

        return false;
    }

    // PUBLIC
    
    public Situation(List<object?> slots)
    {
        _id = _idCount++;
        _slots = slots;

        LogDebug($"Subst(s{_id})");
    }

    public Situation() : this (new List<object?>())
    {
    }

    public override string ToString()
    {
        return $"s{_id}";
    }

    public Situation Clone()
    {
        return new Situation(_slots.ToList());
    }

    public Key Fresh() // p 145
    {
        _slots.Add(null);

        return new Key(_slots.Count - 1);
    }

    public Key[] Fresh(uint n) // p 145
    {
        var ks = new Key[n];

        for (int i = 0; i < n; ++i)
        {
            ks[i] = Fresh();
        }

        return ks;
    }

    public bool TrySet(Key k, object o) // p 149
    {
        if (Occurs(k, o))
        {
            return false;
        }

        _slots[k.Idx] = o;
        UpdateMaxDefinedCount();

        return true;
    }
    
    public bool TryCloneWith(Key k, object o, out Situation s)
    {
        s = new Situation(_slots.ToList());
        return s.TrySet(k, o);
    }

    public object? Get(Key k)
    {
        return _slots[k.Idx];
    }

    public bool IsDefined(Key k)
    {
        return _slots[k.Idx] is not null;
    }
    
    public Goal CallFresh(Func<Key, Goal> f) // p 165
    {
        return f(Fresh());
    }

    public object Walk(object o) // p 148
    {
        for (;o is Key k && _slots[k.Idx] is not null; o = _slots[k.Idx]!)
        {}

        return o;
    }
}
