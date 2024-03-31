
namespace nk;


public static class StreamExt
{
    public static IEnumerator<Subst> FlattenInf(this IEnumerator<Subst> st) // p 163
    {
        return st;
        #if false
        var streams = new Queue<IEnumerator<Subst>>();
        streams.Enqueue(st);

        while(streams.Count > 0)
        {
            var st1 = streams.Dequeue();

            if(!st1.MoveNext())
            {
                continue;
            }

            streams.Enqueue(st1); // May have more after st1.Current

            if (st1.Current is Subst subst1)
            {
                yield return subst1;
                continue;
            }

            
            if (st1.Current is Suspension susp2)
            {
                streams.Enqueue(susp2.ToStream());
                continue;
            }
            

            throw new ApplicationException("Unknown IStreamTypetype.");
        }
        #endif
    }

    public static IEnumerator<Subst> MapInf(this IEnumerator<Subst> st1, Goal g)
    {
        while(st1.MoveNext())
        {
            var st2 = g(st1.Current);

            while(st2.MoveNext())
            {
                yield return st2.Current;
            }
        }
    }

    public static IEnumerator<Subst> AppendInf(this IEnumerator<Subst> st1, params IEnumerator<Subst>[] sts)
    {
        while(st1.MoveNext())
        {
            yield return st1.Current;
        }

        foreach (var st in sts)
        {
            while(st.MoveNext())
            {
                yield return st.Current;
            }
        }
    }

    public static IEnumerator<Subst> FlatMapInf(this IEnumerator<Subst> st, Goal g) // p 163
    {
        return MapInf(FlattenInf(st), g);
    }

    public static IEnumerator<Subst> Take(this IEnumerator<Subst> st, int n) // p161
    {
        for (int i = 0; i < n && st.MoveNext(); ++i)
        {
            yield return st.Current;
        }
    }
}