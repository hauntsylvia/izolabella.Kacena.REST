using kacena.Classes.Interfaces.Attributes.HTTP;

namespace kacena.Classes.Attributes.HTTP
{
    [AttributeUsage(AttributeTargets.Method)]
    public class HTTPGet : Attribute, IHTTPAttribute
    {
        public string Verb => "GET";
    }
}
