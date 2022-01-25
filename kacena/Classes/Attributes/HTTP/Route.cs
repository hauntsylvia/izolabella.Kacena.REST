namespace kacena.Classes.Attributes.HTTP
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class Route : Attribute
    {
        private readonly string relativeUrl;
        public string RelativeUrl => this.relativeUrl;
        public Route(string RouteTo)
        {
            this.relativeUrl =
                RouteTo.StartsWith("/") && !RouteTo.EndsWith("/") ? RouteTo :
                !RouteTo.StartsWith("/") && RouteTo.EndsWith("/") ? $"/{RouteTo[0..^1]}" :
                RouteTo.StartsWith("/") && RouteTo.EndsWith("/") ? $"{RouteTo[0..^1]}" :
                                                                        $"/{RouteTo}";
        }
    }
}
