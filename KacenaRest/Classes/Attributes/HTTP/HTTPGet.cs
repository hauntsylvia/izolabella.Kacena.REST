using izolabella.Kacena.REST.Classes.Interfaces.Attributes.HTTP;

namespace izolabella.Kacena.REST.Classes.Attributes.HTTP
{
    [AttributeUsage(AttributeTargets.Method)]
    public class HTTPGet : Attribute, IHTTPAttribute
    {
        public string Verb => "GET";
    }
}
