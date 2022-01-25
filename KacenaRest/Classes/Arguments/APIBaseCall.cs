namespace KacenaRest.Classes.Arguments
{
    public class APIBaseCall
    {
        public U? GetCaller<U>() where U : class
        {
            return this.Caller as U;
        }


        private readonly object? _caller;
        public object? Caller => this._caller;


        public APIBaseCall(object? caller)
        {
            this._caller = caller;
        }
    }
}
