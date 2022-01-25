using KacenaRest.Classes.Attributes.HTTP;
using KacenaRest.Classes.Bases;
using KacenaRest.Classes.Entities.Returns;
using KacenaRest.Classes.Enums.ResponseCodes;
using KacenaRest.Classes.Handlers;
using KacenaRest.Classes.Interfaces.Entities;
using Newtonsoft.Json;

namespace KacenaRest.ExampleControllers
{
    internal class Weather : Controller
    {
        public Weather(API api, string service) : base(api, service)
        {
        }

        [HTTPGet]
        [Route("/forecast")]
        internal static HTTPArrayResult<IEntity> Forecast()
        {
            return new(HTTPResponseCode.RequestFulfilled, new ExampleForecast()
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
        [JsonProperty("id")]
        public ulong Id => 0;
    }
}
