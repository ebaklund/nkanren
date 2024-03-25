namespace nkanren;

using System.Text;

public interface IStreamItem
{
    public object? Unwrap()
    {
        return null;
    }
}

public class Stream
{
    // PRIVATE

    private List<IStreamItem> _results;

    // PUBLIC

    public Stream(List<IStreamItem> results)
    {
        _results = results;
    }

    public Stream() : this (new List<IStreamItem>())
    {
    }

    public Stream(params IStreamItem[] results) : this (results.ToList())
    {
    } 

    public Stream(Func<Stream> f) : this (new Susp(f))
    {
    }    

    public IStreamItem this[int i]
    {
        get => _results[i];
    }

    public bool IsEmpty
    {
        get => _results.Count == 0;
    }

    public Stream Append(Stream st2)
    {
        if (_results.LastOrDefault()?.Unwrap() is Susp susp)
        {
            _results.RemoveAt(_results.Count - 1); // f
            _results.AddRange(st2._results);
            _results.Add(susp);
        }
        else
        {
            _results.AddRange(st2._results);
        }

        return this;        
    }

    public Stream Take(int n) // p161
    {
        Stream st = new();

        for (int i = 0; i < n && i < _results.Count; ++i)
        {
            if (_results[i] is Susp susp)
            {
                st._results.AddRange(susp.ToStream()._results.GetRange(0, n - i));
            }
            else
            {
                st._results.Add(_results[i]);
            }
        }

        return st;
    }

    public Stream AppendMap(Goal g) // p 163
    {
        foreach (var obj in _results)
        {
            if (obj is Subst sb)
            {
                _results.AddRange(g(sb)._results);
            }            
            else if (obj is Susp susp)
            {
                _results.AddRange(susp.ToStream().AppendMap(g)._results);
            }
            else
            {
                throw new ApplicationException("Unknown stream item type");
            }
        }

        return this;
    }

    public override string ToString()
    {
        StringBuilder sb = new("(");

        foreach (var item in _results)
        {            
            sb.Append(
                item switch
                {
                    Stream st => st.ToString(),
                    null => "null",
                    _ => item.ToString()
                }
            );
        }

        return sb.Append(")").ToString();
    }
}