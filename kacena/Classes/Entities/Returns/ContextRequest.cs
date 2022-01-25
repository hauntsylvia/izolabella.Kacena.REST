using kacena.Classes.Bases;
using kacena.Classes.Interfaces.Entities;

namespace kacena.Classes.Entities.Returns
{
    internal class ContextRequest
    {

        public bool success => this._result != null && this._error == null;


        private readonly HTTPResult<IEntity>? _result;
        public HTTPResult<IEntity>? result => this._result;


        private readonly HTTPResponseError? _error;
        public HTTPResponseError? error => this._error;


        private bool _writeAsBytes = false;
        public bool writeAsBytes
        {
            get => this._writeAsBytes;
            set => this._writeAsBytes = value;
        }


        private byte[]? _bytesToWrite;
        public byte[]? bytesToWrite { get => this._bytesToWrite; set => this._bytesToWrite = value; }


        public ContextRequest(HTTPResponseError? underlyingError, HTTPResult<IEntity>? result)
        {
            this._result = result;
            this._error = underlyingError;
        }
    }
}
