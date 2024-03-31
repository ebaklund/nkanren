
namespace nk;

using static nk.Utils.LoggerModule;

public class Situation // : IStreamItem
{
    // PRIVATE

    private static int _idCount = 0;
    private int _id = 0;
    private List<object?> _slots;

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

    public Situation CloneWith(Key k, object o)
    {
        var s = new Situation(_slots.ToList());
        s._slots[k.Idx] = o;

        return s;
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

    public bool Set(Key k, object o) // p 149
    {
        if (Occurs(k, o))
        {
            return false;
        }

        _slots[k.Idx] = o;

        return true;
    }
    
    public object? Get(Key k)
    {
        return _slots[k.Idx];
    }
    
    public Goal CallFresh(Func<Key, Goal> f) // p 165
    {
        return f(Fresh());
    }

    public  bool TryUnify(out Situation s2, object o1, object o2) // p 151
    {
        s2 = this.Clone();
        var res = s2.Unify(o1, o2);

        return res;
    }

    public object Walk(object o) // p 148
    {
        for (;o is Key k && _slots[k.Idx] is not null; o = _slots[k.Idx]!)
        {}

        return o;
    }

    public object WalkRec(object o) // p 166
    {
        // Returns copy of input tree with all variables resolved leaving only fresh variables unresolved.

        o = Walk(o);
        
        return o switch
        {
            Key k          => k, // Unresolved since it is fresh
            Key[] ks       => WalkRec(((object[])ks).ToList()),
            object[][] m   => WalkMatrixRec(m),
            List<object> l => l.Select(x => WalkRec(x)).ToList(), // Resolve recursively
            _              => o // Resolved
        };
    }

    public object[][] WalkMatrixRec(object[][] m1)
    {
        var m2 = new object[m1.Length][];

        for (int j = 0; j < m1.Length; j++)
        {
            m2[j] = new object[m1[j].Length];
    
            for (int i = 0; i < m1.Length; i++)
            {
                m2[j][i] = WalkRec(m1[j][i]);
            }
        }

        return m2;
    }

    public bool Unify(object o1, object o2) // p 151
    {
        o1 = Walk(o1);
        o2 = Walk(o2);

        if (o1 == o2)
        {
            return true;
        }
/*
        if (o1 is Key && o2 is Key)
        {
            // Walked keys are fresh so no need to compare values
            return true;
        }
*/
        if (o1 is Key k1)
        {
            return Set(k1, o2);
        }
        
        if (o2 is Key k2)
        {
            return Set(k2, o1);
        }
                
        var type1 = o1.GetType();
        var type2 = o2.GetType();

        if (type1.MetadataToken != type2.MetadataToken)
        {
            return false;
        }

        if (o1 is List<object> l1 && o2 is List<object> l2)
        {
            if (l1.Count != l2.Count)
            {
                return false; 
            }
            
            var i = 0;
            for (; (i < l1.Count) && Unify(l1[i], l2[i]); ++i);

            return i == l1.Count;
        }

        if (type1.Name.StartsWith("ValueTuple"))
        {
            var fields1 = type1.GetFields();
            var fields2 = type2.GetFields();

            var i = 0;
            for (; (i < fields1.Length) && Unify(fields1[i].GetValue(o1)!, fields2[i].GetValue(o2)!); ++i);
    
            return i == fields1.Length;
        }

        return false;
    }
}
