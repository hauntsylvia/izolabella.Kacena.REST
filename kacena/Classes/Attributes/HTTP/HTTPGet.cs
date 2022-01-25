using Kacena.Classes.Interfaces.Attributes.HTTP;

namespace Kacena.Classes.Attributes.HTTP
{
    [AttributeUsage(AttributeTargets.Method)]
    public class HTTPGet : Attribute, IHTTPAttribute
    {
        public string Verb => "GET";
    }
}
