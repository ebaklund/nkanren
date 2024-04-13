using nk;

namespace Sudoku;

public class SudokuNumber : IResolvable
{
    // PRIVATE
    uint _value;

    private SudokuNumber(uint value)
    {
        _value = value;
    }

    // PUBLIC

    public uint Value
    {
        get => _value;
    }
    
    object IResolvable.GetResolvable()
    {
        return _value;
    }

    IResolvable IResolvable.Wrap(object r)
    {
        if (r is not uint value)
        {
            throw new ArgumentOutOfRangeException($"Unexpected SudokuNumber value: \"{r.GetType().Name}\".");
        }

        return SudokuNumber.From(value);
    }

    public static SudokuNumber From(uint value)
    {
        if (value < 1 || value > 9)
        {
            throw new ArgumentOutOfRangeException($"Number value ({value}) is not in range [1..9].");
        }

        return new SudokuNumber(value);
    }
}
