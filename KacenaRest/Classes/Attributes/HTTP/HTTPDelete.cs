using KacenaRest.Classes.Interfaces.Attributes.HTTP;

namespace KacenaRest.Classes.Attributes.HTTP
{
    [AttributeUsage(AttributeTargets.Method)]
    public class HTTPDelete : Attribute, IHTTPAttribute
    {
        public string Verb => "DELETE";
    }
}
