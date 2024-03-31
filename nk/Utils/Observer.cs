

namespace nk.Utils;

/// <summary>
/// Observes a situation by returning a deep copy where variables are resolved.
/// Implementation is inspired by "The Reasoned Schemer, 2nd ed. p166" 'walk*'.
/// Used to produce runner output.
/// </summary>
/// 
internal static class Observer
{
    // PRIVATE

    private static object[][] Observe(Situation s, object[][] m1)
    {
        var m2 = new object[m1.Length][];

        for (int j = 0; j < m1.Length; j++)
        {
            m2[j] = new object[m1[j].Length];
    
            for (int i = 0; i < m1.Length; i++)
            {
                m2[j][i] = Observe(s, m1[j][i]);
            }
        }

        return m2;
    }

    // PUBLIC

    public static object Observe(Situation s, object o)
    {
        return s.Walk(o) switch
        {
            Key k          => k, // Return unresolved fresh keys as is.
            string str     => str,
            object v when v.GetType().IsValueType => v,
            object[][] m   => Observe(s, m),
            List<object> l => l.Select(o => Observe(s, o)).ToList(),
            object x       => throw new ApplicationException("Unknown observable.")
        };
    }
}
