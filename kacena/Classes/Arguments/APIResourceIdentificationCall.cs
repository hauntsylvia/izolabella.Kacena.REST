namespace Kacena.Classes.Arguments
{
    public class APIResourceIdentificationCall : APIBaseCall
    {
        private readonly ulong resourceIdentifier;
        public ulong ResourceIdentifier => this.resourceIdentifier;


        public APIResourceIdentificationCall(object? Caller, ulong ResourceIdentifier) : base(Caller)
        {
            this.resourceIdentifier = ResourceIdentifier;
        }
    }
}
