
using static nk.SubstModule;

namespace nk;


public static partial class GoalsModule
{
    // PUBLIC
   
    public delegate IEnumerable<Substitution> Goal(Substitution input);
}
