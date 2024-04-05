
using static nk.LoggerModule;

namespace nk;


public static partial class GoalsModule
{
    // PRIVATE

    public static bool TryUnify(Situation s, object o1, object o2) // p 151
    {
        o1 = s.Walk(o1);
        o2 = s.Walk(o2);

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
            return s.TrySet(k1, o2);
        }
        
        if (o2 is Key k2)
        {
            return s.TrySet(k2, o1);
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
            for (; (i < l1.Count) && TryUnify(s, l1[i], l2[i]); ++i);

            return i == l1.Count;
        }

        if (type1.Name.StartsWith("ValueTuple"))
        {
            var fields1 = type1.GetFields();
            var fields2 = type2.GetFields();

            var i = 0;
            for (; (i < fields1.Length) && TryUnify(s, fields1[i].GetValue(o1)!, fields2[i].GetValue(o2)!); ++i);
    
            return i == fields1.Length;
        }

        return false;
    }

    // PUBLIC

    public static Goal Equal(object o1, object o2) // p 154
    {
        return (Situation s) =>
        {
            LogDebug($"Equal({s})");
            return TryUnify(s, o1, o2) ? Succ()(s) : Fail()(s);
        };
    }

}

