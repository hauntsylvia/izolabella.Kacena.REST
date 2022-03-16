using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace izolabella.Kacena.REST.Classes.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class QueryPropertyAttribute : Attribute
    {
        public QueryPropertyAttribute(string Name)
        {
            this.Name = Name;
        }

        public string Name { get; }
    }
}
