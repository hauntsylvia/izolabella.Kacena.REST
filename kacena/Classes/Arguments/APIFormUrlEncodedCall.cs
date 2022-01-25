namespace kacena.Classes.Arguments
{
    public class APIFormUrlEncodedCall : APIBaseCall
    {
        private readonly Dictionary<string, string> _query;
        public Dictionary<string, string> query => this._query;


        public APIFormUrlEncodedCall(object? caller, Dictionary<string, string> query) : base(caller)
        {
            this._query = query;
        }
    }
}
