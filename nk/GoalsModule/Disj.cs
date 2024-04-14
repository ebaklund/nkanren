
using static nk.LoggerModule;
using static nk.SubstModule;

namespace nk;


public static partial class GoalsModule
{
    // PRIVATE

    private static IEnumerable<Substitution> Multiplex(IEnumerable<IEnumerable<Substitution>> streams)
    {
        Queue<IEnumerator<Substitution>> queue = new(streams.Select(st => st.GetEnumerator()));

        while (queue.Count > 0)
        {
            var st = queue.Dequeue();

            if (!st.MoveNext())
            {
                continue;
            }

            yield return st.Current;

            queue.Enqueue(st);
        }
    }

    // PUBLIC
   
    public static Goal Disj(params Goal[] gs) // p 177
    {
        return gs.Length switch
        {
            0 => Succ(),
            1 => gs[0],
            _ => (Substitution s) =>
            {
                LogDebug($"Disj({s})");
                return Multiplex(gs.Select(g => g(s.Clone())));
            }
        };
    }
}
