
namespace nk;

public interface IResolvable
{
    object GetResolvable();
    IResolvable Wrap(object r);
}