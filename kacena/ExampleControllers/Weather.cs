using kacena.Classes.Arguments;
using kacena.Classes.Attributes.HTTP;
using kacena.Classes.Bases;
using kacena.Classes.Entities.Returns;
using kacena.Classes.Enums.ResponseCodes;
using kacena.Classes.Handlers;
using kacena.Classes.Interfaces.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kacena.ExampleControllers
{
    internal class Weather : Controller
    {
        public Weather(API api, string service) : base(api, service)
        {
        }

        [HTTPGet]
        [Route("/forecast")]
        internal HTTPResult<IEntity> Forecast()
        {
            return new(HTTPResponseCode.requestFulfilled, new ExampleForecast()
            {
                date = DateTime.Now,
                woo = "this is a test endpoint"
            });
        }
    }
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    internal class ExampleForecast : IEntity
    {
        [JsonProperty("date")]
        internal DateTime date;
        [JsonProperty("w")]
        internal string? woo;
        public ulong id => 0;
        public void Delete()
        {
            throw new NotImplementedException();
        }
    }
}
