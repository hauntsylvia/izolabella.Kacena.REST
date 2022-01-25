using Kacena.Classes.Interfaces.Attributes.HTTP;

namespace Kacena.Classes.Attributes.HTTP
{
    [AttributeUsage(AttributeTargets.Method)]
    public class HTTPDelete : Attribute, IHTTPAttribute
    {
        public string Verb => "DELETE";
    }
}
