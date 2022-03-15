using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Collections.Specialized;

namespace izolabella.Kacena.REST.Classes.Util
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
                foreach(string Key in this.RawQueryString.Keys)
                {
                    foreach (PropertyInfo Property in ToInstantiate.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                    {
                        try
                        {
                            string PropertyName = Property.Name;
                            string? Value = this.RawQueryString[Key];
                            if(PropertyName == Key && Value != null)
                            {
                                Type ObjectRequiresThisType = Property.PropertyType;
                                if(ObjectRequiresThisType == typeof(string))
                                {
                                    Property.SetValue(RawResult, Value);
                                }
                                else if(ObjectRequiresThisType == typeof(bool))
                                {
                                    Property.SetValue(RawResult, Value.ToLower() == "true");
                                }
                                else if(ObjectRequiresThisType == typeof(int) && int.TryParse(Value, out int ProvidedInteger))
                                {
                                    Property.SetValue(RawResult, ProvidedInteger);
                                }
                                if(Property.GetValue(RawResult) != null)
                                {
                                    Result = (T)RawResult;
                                    return true;
                                }
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }
            Result = default;
            return false;
        }
#pragma warning restore CS8601 // Possible null reference assignment.
    }
}
