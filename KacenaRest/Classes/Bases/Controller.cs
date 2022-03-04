using KacenaRest.Classes.Handlers;
using KacenaRest.Classes.Interfaces.Bases;

namespace KacenaRest.Classes.Bases
{
    public class Controller : IController
    {
        private readonly string serviceName;
        public string ServiceName => this.serviceName;


        private readonly API api;
        public API API => this.api;
        public Controller(API api, string service)
        {
            this.serviceName = service;
            this.api = api;
        }
    }
}
