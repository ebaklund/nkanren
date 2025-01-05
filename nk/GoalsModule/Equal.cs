
namespace nk;
using static nk.LoggerModule;
using static nk.SubstModule;


public static partial class GoalsModule
{
    // PRIVATE

    public static bool TryUnify(Substitution s, object o1, object o2) // p 151
    {
        object w1 = s.Walk(o1);
        object w2 = s.Walk(o2);

        if (Object.Equals(w1, w2))
        {
            return true;
        }

        if (w1 is Key k1)
        {
            return s.TrySet(k1, w2);
        }
        
        if (w2 is Key k2)
        {
            return s.TrySet(k2, w1);
        }
                
        var type1 = w1.GetType();
        var type2 = w2.GetType();

        if (type1.MetadataToken != type2.MetadataToken)
        {
            return false;
        }

        if (w1 is List<object> l1 && w2 is List<object> l2)
        {
            if (l1.Count != l2.Count)
            {
                return false; 
            }
            
            var i = 0;
            for (; (i < l1.Count) && TryUnify(s, l1[i], l2[i]); ++i);

            return i == l1.Count;
        }

        if (type1.IsArray)
        {
            Array a1 = (Array) w1;
            Array a2 = (Array) w2;

            if (a1.Length != a2.Length)
            {
                return false; 
            }
            
            var i = 0;
            for (; (i < a1.Length) && TryUnify(s, (object) a1.GetValue(i)!, (object) a2.GetValue(i)!); ++i);

            if (i == a1.Length)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        if (type1.Name.StartsWith("ValueTuple"))
        {
            var fields1 = type1.GetFields();
            var fields2 = type2.GetFields();

            var i = 0;
            for (; (i < fields1.Length) && TryUnify(s, fields1[i].GetValue(w1)!, fields2[i].GetValue(w2)!); ++i);
    
            return i == fields1.Length;
        }

        return false;
    }

    // PUBLIC

    public static Goal Equal(object o1, object o2) // p 154
    {
        return (Substitution s) =>
        {
            LogDebug($"Equal({s})");
            return TryUnify(s, o1, o2) ? Succ()(s) : Fail()(s);
        };
    }

    public static Goal Equal(Func<object> getObj1, Func<object> getObj2) // p 154
    {
        return Equal(getObj1(), getObj2());
    }
}

