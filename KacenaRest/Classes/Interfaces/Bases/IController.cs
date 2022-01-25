using KacenaRest.Classes.Handlers;

namespace KacenaRest.Classes.Interfaces.Bases
{
    public interface IController
    {
        string ServiceName { get; }
        API API { get; }
    }
}
