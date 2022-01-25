﻿namespace kacena.Classes.Attributes.HTTP
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class Route : Attribute
    {
        private readonly string _url;
        public string relativeUri => this._url;
        public Route(string routeTo)
        {
            this._url =
                routeTo.StartsWith("/") && !routeTo.EndsWith("/") ? routeTo :
                !routeTo.StartsWith("/") && routeTo.EndsWith("/") ? $"/{routeTo[0..^1]}" :
                routeTo.StartsWith("/") && routeTo.EndsWith("/") ? $"{routeTo[0..^1]}" :
                                                                        $"/{routeTo}";
        }
    }
}
