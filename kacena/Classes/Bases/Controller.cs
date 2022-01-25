using kacena.Classes.Handlers;
using kacena.Classes.Interfaces.Bases;

namespace kacena.Classes.Bases
{
    public class Controller : IController
    {
        private readonly string _serviceName;
        public string ServiceName => this._serviceName;


        private readonly API _api;
        public API API => this._api;
        public Controller(API api, string service)
        {
            this._serviceName = service;
            this._api = api;
        }
    }
}
