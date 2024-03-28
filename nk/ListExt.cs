
using System.Text;

namespace nk;

public static class ListConstructor
{
    public static List<object> l(params object[] args)
    {
        return new List<object>(args);
    }
}

public static class ListExt
{
    public static List<object> ListFrom(params object[] items)
    {
        return new List<object>(items);
    }

    public static object? Car(this List<object> self)
    {
        return self.FirstOrDefault();
    }

    public static List<object> Cdr(this List<object> self)
    {
        return self.Count > 1 
            ? self.Skip(1).ToList() // ISSUE: Performance?
            : new List<object>();
    }
    
    public static string AsString(this List<object> list)
    {
        string _BuildString(string bra, List<object> list, string ket)
        {
            var sb = new StringBuilder().Append(bra);

            for (var i = 0; i < list.Count; ++i)
            {
                var str = list[i] switch
                {
                    Key k => $"_{k.Idx}",
                    string s => $"\"{s}\"",
                    List<object> l => l.AsString(),
                    object[] a => _BuildString("", a.ToList(), ""),
                    _ => list[i].ToString()
                };

                sb.Append(str);
            
                if ((i+1) < list.Count)
                {
                    sb.Append(", ");
                }
            }

            return sb.Append(ket).ToString();
        }

        return _BuildString("(", list, ")");
    }
}