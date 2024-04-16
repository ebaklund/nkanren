
using static nk.SubstModule;

namespace nk;


public static partial class GoalsModule
{
    // PUBLIC
   
    public static Goal Ifte(Goal g1, Goal g2, Goal g3) // 173
    {
        return (Substitution s) =>
        {
            var st1 = Conj(g2, g1)(s);
            return st1.Any() ? st1 : g3(s);
        };
    }
}
