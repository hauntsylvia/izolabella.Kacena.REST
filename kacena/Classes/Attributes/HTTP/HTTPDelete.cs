using kacena.Classes.Interfaces.Attributes.HTTP;

namespace kacena.Classes.Attributes.HTTP
{
    [AttributeUsage(AttributeTargets.Method)]
    public class HTTPDelete : Attribute, IHTTPAttribute
    {
        public string Verb => "DELETE";
    }
}
