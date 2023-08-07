namespace nkanren;

using Stream = List<object>;
using Subst = List<object?>;

public static class StreamExt
{
    public static bool IsEmpty(this Stream self)
    {
        return self.Count == 0;
    }

    public static Stream AppendFunc(this Stream self, Func<Stream> func)
    {
        self.Add(func);

        return self;
    }

    public static Stream AppendList(this Stream st1, Stream st2)
    {
        var r = st1.GetRange(0, st1.Count);
        r.AddRange(st2);

        return r;
    }

    public static Stream Append(this Stream st1, Stream st2) // p 156
    {
        Stream result;

        if (st1.Last() is Func<Stream> f)
        {
            result = st1.GetRange(0, st1.Count - 1);
            result.Add(() => Append(st2, f()));
        }
        else
        {
            result = st1.ToList();
            result.AddRange(st2);
        }

        return result;        
    }

    public static Stream Take(this Stream st, int n) // p161
    {
        Stream result = new();

        for (int i = 0; i < n && i < st.Count; ++i)
        {
            if (st[i] is Func<Stream> f)
            {
                st = f();
                i -= i;
                n -= i;
            }

            result.Add(st[i]);
        }

        return result;
    }

    public static Stream AppendMap(this Stream st, Goal g) // p 163
    {
        Stream result = new();

        foreach (var obj in st)
        {
            if (obj is Subst sb)
            {
                result.AddRange(g(sb));
            }            
            else if (obj is Func<Stream> f)
            {
                result.AddRange(f().AppendMap(g));
            }
            else
            {
                throw new ApplicationException("Unknown stream item type");
            }
        }

        return result;
    }
}