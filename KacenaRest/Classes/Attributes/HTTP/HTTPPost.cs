using KacenaRest.Classes.Interfaces.Attributes.HTTP;

namespace KacenaRest.Classes.Attributes.HTTP
{
    [AttributeUsage(AttributeTargets.Method)]
    public class HTTPPost : Attribute, IHTTPAttribute
    {
        public string Verb => "POST";
    }
}
