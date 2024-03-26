
using System.Text;

namespace nk;

public static class IEnumeratorExt
{
    public static List<T> AsList<T>(this IEnumerator<T> st)
    {
        var list = new List<T>();

        while (st.MoveNext())
        {
            list.Add(st.Current);
        }

        return list;
    }

    public static string AsString(this IEnumerator<object> st)
    {
        return st.AsList().AsString();
    }
}
