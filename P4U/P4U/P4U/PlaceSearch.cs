using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace P4U
{
    public class PlaceSearch
    {
        public string Name { get; set; }
        public string Picture { get; set; }
        public string PlaceId { get; set; }
        public string Address { get; set; }
        public string PageToken { get; set; }
    }
}
