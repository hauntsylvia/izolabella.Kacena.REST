using izolabella.Kacena.REST.Classes.Handlers;
using izolabella.Kacena.REST.Classes.Interfaces.Bases;

namespace izolabella.Kacena.REST.Classes.Bases
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
