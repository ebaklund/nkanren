
namespace nkanren;

static class ListExt
{
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
        return self.Count > 1 
            ? self.Skip(1).ToList() // ISSUE: Performance?
            : new List<object?>();
    }
}