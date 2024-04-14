
using static nk.SubstModule;

namespace nk;


public static partial class GoalsModule
{
    // PUBLIC
   
    public delegate IEnumerator<Substitution> Goal(Substitution input);
}
