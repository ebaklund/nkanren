
namespace nkanren;

using Subst = List<object?>;
using  Stream = List<object?>;

public static class Runners
{
    public static Stream RunGoal(Goal g) // p 169
    {
        return g(new Subst());
    }

    public static Stream RunGoal(int n, Goal g) // p 169
    {
        return RunGoal(g).Take(n);
    }
}