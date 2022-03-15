namespace izolabella.Kacena.REST.Classes.Arguments
{
    public class APIBaseCall
    {
        public U? GetCaller<U>() where U : class
        {
            return this.Caller as U;
        }


        private readonly object? caller;
        public object? Caller => this.caller;


        public APIBaseCall(object? caller)
        {
            this.caller = caller;
        }
    }
}
