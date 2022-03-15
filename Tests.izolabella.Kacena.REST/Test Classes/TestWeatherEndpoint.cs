using izolabella.Kacena.REST.Classes.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.izolabella.Kacena.REST.Test_Classes
{
    public class TestWeatherEndpoint : IEndpoint
    {
        public string Route => "weather";
        public static string ToCall()
        {
            return "called!";
        }
    }
}
