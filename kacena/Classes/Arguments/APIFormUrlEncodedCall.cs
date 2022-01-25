namespace Kacena.Classes.Arguments
{
    public class APIFormUrlEncodedCall : APIBaseCall
    {
        private readonly Dictionary<string, string> query;
        public Dictionary<string, string> Query => this.query;


        public APIFormUrlEncodedCall(object? caller, Dictionary<string, string> query) : base(caller)
        {
            this.query = query;
        }
    }
}
