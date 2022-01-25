using kacena.Classes.Interfaces.Attributes.HTTP;

namespace kacena.Classes.Attributes.HTTP
{
    [AttributeUsage(AttributeTargets.Method)]
    public class HTTPPost : Attribute, IHTTPAttribute
    {
        public string verb => "POST";
    }
}
