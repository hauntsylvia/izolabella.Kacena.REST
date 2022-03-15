using izolabella.Kacena.REST.Classes.Attributes.HTTP;
using izolabella.Kacena.REST.Classes.Bases;
using izolabella.Kacena.REST.Classes.Entities.Returns;
using izolabella.Kacena.REST.Classes.Enums.ResponseCodes;
using izolabella.Kacena.REST.Classes.Handlers;
using izolabella.Kacena.REST.Classes.Interfaces.Entities;
using Newtonsoft.Json;

namespace izolabella.Kacena.REST.ExampleControllers
{
    public class Weather : Controller
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
