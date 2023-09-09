
namespace nkanren;

public class Susp : IStreamItem
{
    // PRIVATE

    Func<Stream> _func;

    // PUBLIC

    public Susp (Func<Stream> func)
    {
        _func = func;
    }

    public Stream ToStream()
    {
        return _func();
    }

    public Susp Unwrapx()
    {
        return this;
    }
}