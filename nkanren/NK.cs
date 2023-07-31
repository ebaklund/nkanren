
using KellermanSoftware.CompareNetObjects;
using System;

namespace nkanren;

/*
public enum Key
{
}

static class KeyExt
{
    public static string ToString(this Key k)
    {
        return $"_{(int)k}";
    }
}

static class ObjectExt
{
    // PRIVATE

    private static readonly CompareLogic _cmp = new();

    // PUBLIC

    public static bool Eqv(this Object? self, Object? x)
    {
        return _cmp.Compare(self, x).AreEqual;
    }

    public static bool ContainsKey(this Object? self, Key k, Dict d)
    {
        return d.Trace(self) switch
        {
            Key asKey => asKey == k,
            List<object?> asList => asList.Car().ContainsKey(k, d) || asList.Cdr().ContainsKey(k, d),
            _ => false
        };
    }
}

static class ListExt
{
    // PRIVATE

    private static readonly List<object?> _emptyList = new();

    // PUBLIC

    public static object? Car(this List<object?> self)
    {
        return self.FirstOrDefault();
    }

    public static List<object?> Cdr(this List<object?> self)
    {
        return self.Count > 1 ? self.Skip(1).ToList() : _emptyList; // ISSUE: Performance?
    }
}

public class Dict
{
    // PRIVATE

    private readonly List<object?> _slots = new();

    // PUBLIC

    public object? Trace(object? x)
    {
        return x switch
        {
            Key k => Trace(_slots[(int)k]),
            _ => x
        };
    }

    public Key FreshKey()
    {
        Key k = (Key)_slots.Count;
        _slots.Add(null);
        return k;
    }

    public bool TrySet(Key? nk, object? v)
    {
        if (nk is not Key k || v.ContainsKey(k, this))
        {
            return false;
        }

        _slots[(int)k] = v;
        return true;
    }

    public bool Unify(object? u, object? v)
    {
        u = Trace(u);
        v = Trace(v);

        return u.Eqv(v) || // 
            (u is Key ku && TrySet(ku, v)) ||
            (v is Key kv) && TrySet(kv, u) ||
            (v is List<object?> lv) && (u is List<object?> lu) && Unify(lv.Car(), lu.Car()) && Unify(lv.Cdr(), lu.Cdr());
    }
}


public static class NK
{
    // PRIVATE

    private static readonly List<object?> _emptyList = new();
    private static readonly List<Dict> _emptyStream = new();

    // PUBLIC

    public static List<object?> E
    {
        get => _emptyList;
    }

    public static Func<Dict, List<Dict>> Succ()
    {
        return (d) => new List<Dict>() { d };
    }

    public static Func<Dict, List<Dict>> Fail()
    {
        return (d) => _emptyStream;
    }

    public static Func<Dict, List<Dict>> Assoc(object? u, object? v)
    {
        return (d) => d.Unify(u, v) 
            ? new List<Dict>() { d } 
            : _emptyStream;
    }

    public static List<Dict> Run(Func<Key, Func<Dict, List<Dict>>> gg)
    {
        Dict d = new ();
        Key k = d.FreshKey();

        return gg(k)(d); // ISSUE: Performance?
    }

    static void deleteme()
    {
        Run((q) => Fail());
    }

    ///


}
*/

public class Key { };

public record Assoc(Key Key, object? Val);

static class ListExt
{
    // PRIVATE

    private static readonly List<object?> _emptyList = new();

    private static (object?, Exception) Cond(int i, params (Func<bool>, Func<(object?, Exception)>)[] work)
    {
        return
            (i >= work.Length) ? (null, new ApplicationException("Case(): Case list fall through.")) :
            (work[i].Item1()) ? work[i].Item2() :
            Cond(i + 1, work);
    }

    // PUBLIC

    public static object? Car(this List<object?> self)
    {
        return self.FirstOrDefault();
    }

    public static List<object?> Cdr(this List<object?> self)
    {
        return self.Count > 1 ? self.Skip(1).ToList() : _emptyList; // ISSUE: Performance?
    }

    public static (object?, Exception) Cond(params (Func<bool>, Func<(object?, Exception)>)[] work)
    {
        return Cond(0, work);
    }
}

public class Subst
{
    // PRIVATE

    private Dictionary<Key, object?> _dict = new();

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
        while (obj is Key k && _dict.ContainsKey(k))
        {
            obj = _dict[k];
        }

        return obj;
    }

    public bool Occurs(Key k, object? obj)
    {
        return Walk(obj).Item1 switch
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