
using System.IO;
using static nk.SubstModule;

namespace nk;


public static partial class GoalsModule
{
    // PUBLIC
   
    public static Goal LoopUntil(Goal endGoal, Goal refinementGoal, Goal initialGoal) // 173
    {
        IEnumerable<Substitution> _LoopUntil(Substitution s)
        {
            Queue<IEnumerable<Substitution>> queue = new();
            var initialStream = initialGoal(s);
            queue.Enqueue(initialStream);

            IEnumerable<Substitution> stream;
            while (queue.TryDequeue(out stream))
            {
                foreach (var subst in stream)
                {
                    var endResults = endGoal(subst);

                    if (endResults.Any())
                    {
                        foreach(var res in endResults)
                        {
                            yield return res;
                        }
                    }
                    else
                    {
                        var refinementStream = refinementGoal(subst);
                        queue.Enqueue(refinementStream);
                    }
                }
            }
        };

        return _LoopUntil;
    }
}
