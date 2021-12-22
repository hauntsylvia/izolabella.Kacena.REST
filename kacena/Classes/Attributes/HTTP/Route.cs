using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kacena.Classes.Attributes.HTTP
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class Route : Attribute
    {
        private readonly string _url;
        public string relativeUri => _url;
        public Route(string routeTo)
        {
            this._url = routeTo.StartsWith("/") ? routeTo : throw new ArgumentException($"{nameof(routeTo)} must start with a '/'");
        }
    }
}
