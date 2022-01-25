using Kacena.Classes.Handlers;

namespace Kacena.Classes.Interfaces.Bases
{
    public interface IController
    {
        string ServiceName { get; }
        API API { get; }
    }
}
