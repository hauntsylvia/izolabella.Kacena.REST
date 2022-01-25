using KacenaRest.Classes.Interfaces.Attributes.HTTP;

namespace KacenaRest.Classes.Attributes.HTTP
{
    [AttributeUsage(AttributeTargets.Method)]
    public class HTTPPatch : Attribute, IHTTPAttribute
    {
        public string Verb => "PATCH";
    }
}
