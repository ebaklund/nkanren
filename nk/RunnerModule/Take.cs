
namespace nk;

public static partial class RunnerModule
{
    public static List<T> TakeMax<T>(this IEnumerator<T> stream , int n)
    {
        var count = 0;
        var res = new List<T>();

        while(stream.MoveNext() && (count++ < n))
        {
            res.Add (stream.Current);
        }

        return res;
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
