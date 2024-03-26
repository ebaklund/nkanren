
using System.Collections.Generic;

namespace nk;

public class Suspension : IStreamItem
{
    // PRIVATE

    Func<IEnumerator<IStreamItem>> _func;

    // PUBLIC

    public Suspension (Func<IEnumerator<IStreamItem>> func)
    {
        _func = func;
    }

    public IEnumerator<IStreamItem> ToStream()
    {
        return _func();
    }
}