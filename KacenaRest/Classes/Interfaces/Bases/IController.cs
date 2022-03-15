using izolabella.Kacena.REST.Classes.Handlers;

namespace izolabella.Kacena.REST.Classes.Interfaces.Bases
{
    public interface IController
    {
        string ServiceName { get; }
        API API { get; }
    }
}
