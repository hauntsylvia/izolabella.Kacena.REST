using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kacena.Classes.Interfaces.Entities
{
    public interface IEntity
    {
        ulong id { get; }
        void Delete();
    }
}
