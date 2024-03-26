
namespace nk;


public static class StreamExt
{
    public static IEnumerator<Subst> FlattenInf(this IEnumerator<IStreamItem> st) // p 163
    {
        var streams = new Queue<IEnumerator<IStreamItem>>();
        streams.Enqueue(st);

        while(streams.Count > 0)
        {
            var st1 = streams.Dequeue();

            if(!st1.MoveNext())
            {
                continue;
            }

            streams.Enqueue(st1);

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
    }

    public static IEnumerator<IStreamItem> MapInf(this IEnumerator<Subst> st, Goal g)
    {
        while(st.MoveNext())
        {
            var st2 = g(st.Current);

            while(st2.MoveNext())
            {
                yield return st2.Current;
            }
        }
    }

    public static IEnumerator<IStreamItem> AppendInf(this IEnumerator<IStreamItem> st1, IEnumerator<IStreamItem> st2)
    {
        while(st1.MoveNext())
        {
            yield return st1.Current;
        }

        while(st2.MoveNext())
        {
            yield return st2.Current;
        }
    }

    public static IEnumerator<IStreamItem> FlatMapInf(this IEnumerator<IStreamItem> st, Goal g) // p 163
    {
        return MapInf(FlattenInf(st), g);
    }

    public static IEnumerator<IStreamItem> Take(this IEnumerator<IStreamItem> st, int n) // p161
    {
        for (int i = 0; i < n && st.MoveNext(); ++i)
        {
            yield return st.Current;
        }
    }
}