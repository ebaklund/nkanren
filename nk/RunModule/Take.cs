
namespace nk;

public static partial class RunModule
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
}
