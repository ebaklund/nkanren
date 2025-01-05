

using static nk.SubstModule;
using static nk.GoalsModule;
using static nk.RunnerModule;
using static nk.HooksModule;

namespace TowerOfHanoi;

using nk;
using static TowerOfHanoi.GoalsModule;

public static class RunnerModule
{
    // PRIVATE

    private static Func<Substitution, (bool, string)> VerifyStack(Key k)
    {
        return s =>
        {
            if (s.Walk(k) is not int[] stack)
            {
                return (false, $"Key {k} does not reference a Hanoi stack.");
            }

            for (int i = 0; i < stack.Length - 1; ++i)
            {
                if (stack[i] < stack[i + 1]) // Disks are supposed to be appended by diminishing size.
                {
                    return (false, $"Key {k} is referencing an unordered Hanoi stack.");
                }
            }

            return (true, "ok");
        };
    }

    private static Func<Substitution, (bool, string)> VerifyStacks(Key a, Key b, Key c)
    {
        (bool, string) _VerifyStacks(Substitution s)
        {
            (bool, string)[] res = [VerifyStack(a)(s), VerifyStack(b)(s), VerifyStack(c)(s)];
            bool flag = res.Select(x => x.Item1).All(x => x);
            string msg = res.Select(x => x.Item2).MaxBy(s => s.Length);

            return (flag, msg);
        };

        return _VerifyStacks;
    }

    // PUBLIC

    public static IEnumerable<int[][]> RunTowerOfHanoi(int n)
    {
        return RunN<int[][]>(1, (a, b, c) =>
        {
            int[] emptyStack = (int[])[];
            int[] fullStack = Enumerable.Range(0, n).Reverse().ToArray();

            var initialStacks = (object[])[fullStack, emptyStack, emptyStack];
            var endStacks = (object[])[emptyStack, emptyStack, fullStack];

            Goal initialGoal = Equal((object[]) [a, b, c], initialStacks);
            Goal endGoal = Equal((object[])[a, b, c], endStacks);

            Goal assertedMove = Conj(
                AssertHook(VerifyStacks(a, b, c)),
                TowerOfHanoiMove([a, b, c])
            );

            return LoopUntil(endGoal, assertedMove, initialGoal);
        });
    }
}

