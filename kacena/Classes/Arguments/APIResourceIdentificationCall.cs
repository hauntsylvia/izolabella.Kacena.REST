namespace kacena.Classes.Arguments
{
    public class APIResourceIdentificationCall : APIBaseCall
    {
        private readonly ulong _inUrl;
        public ulong inUrl => this._inUrl;


        public APIResourceIdentificationCall(object? caller, ulong inUrlIdentification) : base(caller)
        {
            this._inUrl = inUrlIdentification;
        }
    }
}
