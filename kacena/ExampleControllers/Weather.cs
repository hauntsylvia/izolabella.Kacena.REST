using kacena.Classes.Attributes.HTTP;
using kacena.Classes.Bases;
using kacena.Classes.Entities.Returns;
using kacena.Classes.Enums.ResponseCodes;
using kacena.Classes.Handlers;
using kacena.Classes.Interfaces.Entities;
using Newtonsoft.Json;

namespace kacena.ExampleControllers
{
    internal class Weather : Controller
    {
        public Weather(API api, string service) : base(api, service)
        {
        }

        [HTTPGet]
        [Route("/forecast")]
        internal static HTTPResult<IEntity> Forecast()
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
    }
}
