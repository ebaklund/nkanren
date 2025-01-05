
using nk;
using static nk.GoalsModule;
using static nk.SubstModule;

namespace TowerOfHanoi;

public static class GoalsModule
{
    public static Goal TowerOfHanoiMove(Key[] ks)
    {
        bool CanMove(int[] a, int[] b)
        {
            if (a.Length == 0)
            {
                return false;
            }

            if (b.Length == 0)
            {
                return true;
            }

            if (a.Last() < b.Last())
            {
                return true;
            }

            return false;
        }

        (int[], int[]) Move(int[] a, int[] b)
        {
            int[] aRes = a.Take(a.Length - 1).ToArray();
            int[] bRes = b.Take(b.Length).Append(a.Last()).ToArray();

            return (aRes, bRes);
        }

        IEnumerable<Substitution> _TowerOfHanoiMove(Substitution s)
        {
            var stacks = ks.Select(k =>
            {
                if (s.Get(k) is not int[] stack)
                {
                    throw new ArgumentNullException($"Stack for key {k} is not defined.");
                }

                return stack;
            }).ToArray();

            for (int j = 0; j < 3; ++j)
            {
                for (int i = 0; i < 3; ++i)
                {
                    if (i == j)
                    {
                        continue;
                    }

                    int k = 3 - j - i;

                    if (CanMove(stacks[i], stacks[j]))
                    {
                        var moved = Move(stacks[i], stacks[j]);
                        var res = new int[3][];
                        res[i] = moved.Item1;
                        res[j] = moved.Item2;
                        res[k] = stacks[k];

                        Substitution s2;
                        
                        if (s.TryCloneWith(ks, res, out s2))
                        {
                            yield return s2;
                        }
                    }
                }
            }
        };

        return _TowerOfHanoiMove;
    }
}
