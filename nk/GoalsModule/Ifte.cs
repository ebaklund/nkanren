
using static nk.SubstModule;

namespace nk;


public static partial class GoalsModule
{
    // PUBLIC
   
    public static Goal Ifte(Goal g1, Goal g2, Goal g3) // 173
    {
        IEnumerable<Substitution> _Ifte(Substitution s)
        {
            bool succeeded = false;

            foreach (var s1 in g1(s))
            {
                foreach (var s2 in g2(s1))
                {
                    yield return s2;
                }

                succeeded = true;
            }

            if (succeeded == false)
            {
                foreach (var s3 in g3(s))
                {
                    yield return s3; 
                }
            } 
        };

        return _Ifte;
    }
}
