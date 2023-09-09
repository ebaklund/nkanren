
namespace nkanren;

using static Goals;

public static class Runners
{
    public static Stream RunGoal(Goal g) // p 169
    {
        return g(new Subst());
    }

    public static Stream Run(int n, Goal g) // p 169
    {
        return RunGoal(g).Take(n);
    }

    public static Stream Run(Func<Key, Goal> f) // p 169
    {
        Subst subst = new();
        var g = f(subst.Fresh());
        return g(subst);
    }
} 