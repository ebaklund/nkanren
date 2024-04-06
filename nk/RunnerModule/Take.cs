
namespace nk;

public static partial class RunnerModule
{
    public static bool TryTakeFirst<T>(this IEnumerator<T> s, out T? res) where T : class
    {
        res = null;

        while(s.MoveNext())
        {
            res = s.Current;
            return true;
        }

        return false;
    }

    public static bool TryTake<T>(this IEnumerator<T> s , int count, out List<T> res)
    {
        res = new List<T>();

        while(s.MoveNext() && (res.Count < count))
        {
            res.Add (s.Current);
        }

        return res.Count == count;
    }

    public static List<T> TakeAll<T>(this IEnumerator<T> stream)
    {
        var res = new List<T>();

        while(stream.MoveNext())
        {
            res.Add (stream.Current);
        }

        return res;
    }
}
