using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace P4U
{
   [DataContract]
    public class PlaceSearch
    {
        [DataMember(Name ="name")]
        public string Name { get; set; }
    }
}
