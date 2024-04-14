
using System.Text;

namespace nk;

public static class IEnumeratorExt
{
    public static string AsString(this IEnumerable<object> st)
    {
        return st.ToList().AsString();
    }
}
