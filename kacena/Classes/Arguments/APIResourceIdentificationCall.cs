using kacena.Classes.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kacena.Classes.Arguments
{
    public class APIResourceIdentificationCall : APIBaseCall
    {
        private readonly ulong _inUrl;
        public ulong inUrl => _inUrl;


        public APIResourceIdentificationCall(object? caller, ulong inUrlIdentification) : base(caller)
        {
            this._inUrl = inUrlIdentification;
        }
    }
}
