using izolabella.Kacena.REST.Classes.Listeners;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Tests.izolabella.Kacena.REST.Test_Classes;

namespace Tests.izolabella.Kacena.REST
{
    [TestClass]
    public class TestEndpoint
    {
        [TestMethod]
        public async Task TestWithWeatherEndpointAsync()
        {
            Uri Prefix = new("https://mercury-bot.ml:443/");
            EndpointListener EndpointListener = new(Prefix);
            EndpointListener.AddEndpoint(new TestWeatherEndpoint());
            using(HttpClient Client = new())
            {
                HttpResponseMessage? r = await Client.GetAsync("https://mercury-bot.ml:443/weather/ToCall");
                string a = await r.Content.ReadAsStringAsync();
                Console.WriteLine(a);
                bool Contains = a.Contains("called!");
                Assert.IsTrue(Contains);
            }
        }
    }
}