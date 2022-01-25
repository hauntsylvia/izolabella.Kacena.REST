using KacenaRest.Classes.Handlers;
using KacenaRest.Classes.Interfaces.Bases;

namespace KacenaRest.Classes.Bases
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
