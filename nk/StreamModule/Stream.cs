
using static nk.GoalsModule;
using static nk.SubstModule;

namespace nk;


public static partial class StreamModule
{
    public static IEnumerable<Substitution> MapInf(this IEnumerable<Substitution> st1, Goal g)
    {
        return st1.Select(s1 => g(s1)).SelectMany(st2 => st2);
    }

    public static IEnumerable<Substitution> AppendInf(this IEnumerable<Substitution> st1, params IEnumerable<Substitution>[] sts)
    {
        List<IEnumerable<Substitution>> streams = [st1, .. sts];

        return streams.SelectMany(st => st);
    }
}