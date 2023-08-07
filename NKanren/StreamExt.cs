namespace nkanren;

using Stream = List<object>;

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

    public static Stream AppendList(this Stream self, List<object> t)
    {
        var r = self.GetRange(0, self.Count);
        r.AddRange(t);

        return r;
    }

    public static List<object> Append(this Stream s, Stream t) // p 156
    {
        return 
            (s.IsEmpty()) ? t
            : (s.Last() is Func<List<object>> f) ? s.GetRange(0, s.Count - 1).AppendFunc(() => Append(t, f()))
            : s.ToList().AppendList(t); 
    }
}