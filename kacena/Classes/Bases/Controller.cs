using kacena.Classes.Handlers;
using kacena.Classes.Interfaces.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kacena.Classes.Bases
{
    public class Controller : IController
    {
        private readonly string _serviceName;
        public string serviceName => _serviceName;


        private readonly API _api;
        public API api => _api;
        public Controller(API api, string service)
        {
            this._serviceName = service;
            this._api = api;
        }
    }
}
