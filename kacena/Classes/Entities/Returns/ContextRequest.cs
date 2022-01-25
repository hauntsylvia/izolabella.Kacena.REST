using Kacena.Classes.Bases;
using Kacena.Classes.Interfaces.Entities;

namespace Kacena.Classes.Entities.Returns
{
    internal class ContextRequest
    {

        public bool Success => (this.arrayResult != null || this.singleResult != null) && this.error == null;


        private readonly HTTPArrayResult<IEntity>? arrayResult;
        public HTTPArrayResult<IEntity>? ArrayResult => this.arrayResult;


        private readonly HTTPSingleResult<IEntity>? singleResult;
        public HTTPSingleResult<IEntity>? SingleResult => this.singleResult;


        private readonly HTTPResponseError? error;
        public HTTPResponseError? Error => this.error;


        private bool writeAsBytes = false;
        public bool WriteAsBytes
        {
            get => this.writeAsBytes;
            set => this.writeAsBytes = value;
        }


        private byte[]? bytesToWrite;
        public byte[]? BytesToWrite { get => this.bytesToWrite; set => this.bytesToWrite = value; }


        public ContextRequest(HTTPResponseError? UnderlyingError, HTTPArrayResult<IEntity>? ArrayResult)
        {
            this.arrayResult = ArrayResult;
            this.error = UnderlyingError;
        }


        public ContextRequest(HTTPResponseError? UnderlyingError, HTTPSingleResult<IEntity>? SingleResult)
        {
            this.singleResult = SingleResult;
            this.error = UnderlyingError;
        }
    }
}
