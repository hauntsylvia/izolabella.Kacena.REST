using kacena.Classes.Handlers;

namespace kacena.Classes.Interfaces.Bases
{
    public interface IController
    {
        string serviceName { get; }
        API api { get; }
    }
}
