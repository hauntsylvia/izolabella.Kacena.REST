using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Collections.Specialized;
using izolabella.Kacena.REST.Objects.Attributes;

namespace izolabella.Kacena.REST.Objects.Util
{
    public class QueryStringConverter<T>
    {
        internal QueryStringConverter(NameValueCollection RawQueryString)
        {
            this.RawQueryString = RawQueryString;
        }

        public NameValueCollection RawQueryString { get; }

#pragma warning disable CS8601 // Possible null reference assignment.
        public bool TryConvert(out T Result)
        {
            Type ToInstantiate = typeof(T);
            object? RawResult = Activator.CreateInstance(ToInstantiate);
            if (RawResult != null)
            {
                foreach (string Key in this.RawQueryString.Keys)
                {
                    foreach (PropertyInfo Property in ToInstantiate.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                    {
                        try
                        {
                            QueryPropertyAttribute? QueryProperty = Property.GetCustomAttribute<QueryPropertyAttribute>();
                            string PropertyName = QueryProperty != null ? QueryProperty.Name : Property.Name;
                            string? Value = this.RawQueryString[Key];
                            if (PropertyName == Key && Value != null && Property.SetMethod != null)
                            {
                                Type ObjectRequiresThisType = Property.PropertyType;
                                if (ObjectRequiresThisType == typeof(string))
                                {
                                    Property.SetValue(RawResult, Value);
                                }
                                else if (ObjectRequiresThisType == typeof(bool))
                                {
                                    Property.SetValue(RawResult, Value.ToLower() == "true");
                                }
                                else if (ObjectRequiresThisType == typeof(int) && int.TryParse(Value, out int ProvidedInteger))
                                {
                                    Property.SetValue(RawResult, ProvidedInteger);
                                }
                            }
                        }
                        catch (Exception Ex)
                        {
                            Console.WriteLine(Ex);
                        }
                    }
                }
                Result = (T)RawResult;
                return true;
            }
            Result = default;
            return false;
        }
#pragma warning restore CS8601 // Possible null reference assignment.
    }
}
