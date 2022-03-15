using izolabella.Kacena.REST.Classes.Interfaces.Attributes.HTTP;

namespace izolabella.Kacena.REST.Classes.Attributes.HTTP
{
    [AttributeUsage(AttributeTargets.Method)]
    public class HTTPPost : Attribute, IHTTPAttribute
    {
        public string Verb => "POST";
    }
}
