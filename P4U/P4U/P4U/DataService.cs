using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;

namespace P4U
{
    public class DataService
    {
        public static async Task<dynamic> GetDataFromService(string queryString)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(queryString);
            var response = await request.GetResponseAsync().ConfigureAwait(false);

            Stream streamReceive = response.GetResponseStream();
            StreamReader streamRead = new StreamReader(streamReceive);
            return JsonConvert.DeserializeObject(streamRead.ReadToEnd());
        }
    }
}
