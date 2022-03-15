using cum.Test_Classes;
using izolabella.Kacena.REST.Classes.Listeners;
namespace cum
{
    public class Program
    {
        public static void Main(string[] Args)
        {
            MainAsync().GetAwaiter().GetResult();
            Task.Delay(-1).GetAwaiter().GetResult();
        }
        public static async Task MainAsync()
        {
            Uri Prefix = new("https://mercury-bot.ml:443/");
            EndpointListener EndpointListener = new(Prefix);
            EndpointListener.AddEndpoint(new TestWeatherEndpoint());
            await EndpointListener.StartListeningAsync();
            using (HttpClient Client = new())
            {
                HttpResponseMessage? r = await Client.GetAsync("https://mercury-bot.ml:443/weather/ToCall");
                string a = await r.Content.ReadAsStringAsync();
                Console.WriteLine(a);
            }
            await Task.Delay(1000);
        }
    }

}