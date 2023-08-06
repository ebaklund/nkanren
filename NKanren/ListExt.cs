
namespace nkanren;

static class ListExt
{
    // PRIVATE

    private static readonly List<object?> _emptyList = new();

    // PUBLIC

    public static List<object?> ListFrom(params object?[] items)
    {
        return new List<object?>(items);
    }

    public static object? Car(this List<object?> self)
    {
        return self.FirstOrDefault();
    }

    public static List<object?> Cdr(this List<object?> self)
    {
        return self.Count > 1 ? self.Skip(1).ToList() : _emptyList; // ISSUE: Performance?
    }

    public static (object?, Exception) Cond(params (Func<bool>, Func<(object?, Exception)>)[] cases)
    {
        foreach (var entry in cases)
        {
            if (entry.Item1())
            {
                return entry.Item2();
            }
        }

        return (null, new ApplicationException("Cond() fall trough."));
    }

    public static List<object?> ShallowCopy(this List<object?> list)
    {
        return list.ToList();
    }
}