using Kacena.Classes.Interfaces.Attributes.HTTP;

namespace Kacena.Classes.Attributes.HTTP
{
    [AttributeUsage(AttributeTargets.Method)]
    public class HTTPPost : Attribute, IHTTPAttribute
    {
        public string Verb => "POST";
    }
}
