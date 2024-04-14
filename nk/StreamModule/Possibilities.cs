
using static nk.GoalsModule;
using static nk.SubstModule;

namespace nk;


public static partial class StreamModule
{
    public static IEnumerator<Substitution> FlattenInf(this IEnumerator<Substitution> st) // p 163
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

    public static IEnumerator<Substitution> MapInf(this IEnumerator<Substitution> st1, Goal g)
    {
        var yielded = 0;

        while (st1.MoveNext())
        {
            var st2 = g(st1.Current);

            while (st2.MoveNext())
            {
                yield return st2.Current;
                ++yielded;
            }
        }
    }

    public static IEnumerator<Substitution> AppendInf(this IEnumerator<Substitution> st1, params IEnumerator<Substitution>[] sts)
    {
        while (st1.MoveNext())
        {
            yield return st1.Current;
        }

        foreach (var st in sts)
        {
            while (st.MoveNext())
            {
                yield return st.Current;
            }
        }
    }

    public static IEnumerator<Substitution> FlatMapInf(this IEnumerator<Substitution> st, Goal g) // p 163
    {
        return st.FlattenInf().MapInf(g);
    }

    public static IEnumerator<Substitution> Take(this IEnumerator<Substitution> st, int n) // p161
    {
        for (int i = 0; i < n && st.MoveNext(); ++i)
        {
            yield return st.Current;
        }
    }
}