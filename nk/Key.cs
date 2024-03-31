
namespace nk;

public record Key(int Idx)
{
    public override string ToString()
    {
        return $"_{Idx}";
    }
}
