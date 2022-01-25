using kacena.Classes.Handlers;

namespace kacena.Classes.Interfaces.Bases
{
    public interface IController
    {
        string ServiceName { get; }
        API API { get; }
    }
}
