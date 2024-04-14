
using System.Text;

namespace nk;

public static class ListConstructor
{
    public static List<object> l(params object[] args)
    {
        return new List<object>(args);
    }
}

public static partial class ResultModule
{   
    public static string AsString(this IEnumerable<object> objects)
    {
        var sb = new StringBuilder().Append("(");

        foreach (var o in objects)
        {
            var str = o switch
            {
                Key k => $"_{k.Idx}",
                string s => $"\"{s}\"",
                List<object> l => l.AsString(),
                object[] a => a.AsString(),
                _ => o.ToString()
            };

            sb.Append(str);
            sb.Append(", ");
        }

        if (sb.Length > 2)
        {
            sb.Remove(sb.Length - 2, 2);
        }

        return sb.Append(")").ToString();
    }
}