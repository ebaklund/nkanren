
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
        var sb = new StringBuilder();
        sb.Append("(");

        foreach (var item in list)
        {
            var str = item switch
            {
                Key key => $"_{key}",
                string val => $"\"{val}\"",
                _ => item.ToString()
            };

            sb.Append(str); 
        }

        sb.Append(")");

        return sb.ToString();
    }
}