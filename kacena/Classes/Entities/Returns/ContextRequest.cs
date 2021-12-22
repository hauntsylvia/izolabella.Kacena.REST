using kacena.Classes.Bases;
using kacena.Classes.Enums.ResponseCodes;
using kacena.Classes.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kacena.Classes.Entities.Returns
{
    internal class ContextRequest
    {

        public bool success => _result != null && _error == null;


        private readonly HTTPResult<IEntity>? _result;
        public HTTPResult<IEntity>? result => _result;


        private readonly HTTPResponseError? _error;
        public HTTPResponseError? error => _error;


        private bool _writeAsBytes = false;
        public bool writeAsBytes 
        { 
            get => _writeAsBytes;
            set
            {
                if (bytesToWrite != null && value)
                    _writeAsBytes = value;
                else
                    throw new ArgumentNullException(paramName: nameof(value), message: $"You must set {nameof(bytesToWrite)} to a value before setting {nameof(writeAsBytes)} to {value}.");
            }
        }


        private byte[]? _bytesToWrite;
        public byte[]? bytesToWrite { get => _bytesToWrite; set => _bytesToWrite = value; }


        public ContextRequest(HTTPResponseError? underlyingError, HTTPResult<IEntity>? result)
        {
            this._result = result;
            this._error = underlyingError;
        }
    }
}
