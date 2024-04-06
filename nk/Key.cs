
namespace nk;

public record Key(uint Idx)
{
    public override string ToString()
    {
        return $"_{Idx}";
    }
}
