using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4U
{
    public class Core
    {
        public static async Task<List<PlaceSearch>> GetPlaceTextSearch()
        {
            string query = "https://maps.googleapis.com/maps/api/place/textsearch/json?query=parking+in+Paris&key=AIzaSyCRaT-68xS7CkjDlvVygW_hWdkBbs4mb-Y";
            dynamic data_results = await DataService.GetDataFromService(query).ConfigureAwait(false);
            dynamic placeOverview = data_results["results"];

            if (data_results["status"].Value == "OK")
            {
                List<PlaceSearch> lstPlaceSearch = new List<PlaceSearch>();
                
                foreach (var item in placeOverview)
                {
                    lstPlaceSearch.Add(new PlaceSearch { Name = item["name"].Value });
                }
                return lstPlaceSearch;
            }
            else
                return null;
        }
    }
}
