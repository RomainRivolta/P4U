using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4U
{
    public class Core
    {
        private const string key = "AIzaSyCRaT-68xS7CkjDlvVygW_hWdkBbs4mb-Y";
        public string latitude { get; set; }
        public string longitude { get; set; }


        //public string TextSearchRequestsByLocation(int radius,string query,string types="",string pagetoken = "")
        //{
        //    return "https://maps.googleapis.com/maps/api/place/textsearch/json?query=" + query + "&pagetoken=" + pagetoken + "&location="+latitude+","+longitude+"&radius="+radius +"&types=" + types + "&key=" + key;
        //}

        public string NearBySearchRequestsByLocation(int radius, string types = "", string pagetoken = "")
        {
            return "https://maps.googleapis.com/maps/api/place/nearbysearch/json?pagetoken=" + pagetoken + "&location=" + latitude + "," + longitude + "&radius=" + radius + "&types=" + types + "&key=" + key;
        }

        public string TextSearchRequestsBySearch(string search)
        {
            return "https://maps.googleapis.com/maps/api/place/textsearch/json?query=" + search + "&key=" + key;
        }

        public string PlaceDetailsResponses(string placeId)
        {
            return "https://maps.googleapis.com/maps/api/place/details/json?placeid="+placeId+"&key=" + key;
        }

        public string DistanceMatrixRequests(string destinationLatitude, string destinationLongitude,string mode)
        {
            return "https://maps.googleapis.com/maps/api/distancematrix/json?origins=" + latitude + "," + longitude + "&destinations=" + destinationLatitude + "," + destinationLongitude + "&mode=" + mode + "&language=fr-FR&key=" + key;
        }

        public string StreetView(string lt,string lng,string size)
        {
            return "https://maps.googleapis.com/maps/api/streetview?location=" + lt + "," + lng + "&size="+size+"&key="+key;
        }

        public async Task<List<PlaceSearch>> GetPlaceSearch(string query, int max_width, int max_height,string mode,string size)
        {    
            dynamic data_results = await DataService.GetDataFromService(query).ConfigureAwait(false);
            dynamic placeOverview = data_results["results"];
            dynamic nextPageToken = data_results["next_page_token"] != null ? data_results["next_page_token"].Value : "";

            if (data_results["status"].Value == "OK")
            {
                List<PlaceSearch> lstPlaceSearch = new List<PlaceSearch>();

                foreach (var item in placeOverview)
                {
                    dynamic address = query.Contains("textsearch") ? item["formatted_address"].Value : item["vicinity"].Value;
                    dynamic longitudeDestinations = item["geometry"]["location"]["lng"].Value;
                    dynamic latitudeDestinations = item["geometry"]["location"]["lat"].Value;

                    string str_latitudeDestinations = Convert.ToString(latitudeDestinations);
                    string str_longitudeDestinations =  Convert.ToString(longitudeDestinations);

                    string queryMatrix = DistanceMatrixRequests(str_latitudeDestinations.Replace(",","."), str_longitudeDestinations.Replace(",", "."), mode);
                    string streetViewQuery = StreetView(str_latitudeDestinations.Replace(",", "."), str_longitudeDestinations.Replace(",", "."), size);
                    string km = GetMatrix(queryMatrix);

                    lstPlaceSearch.Add(new PlaceSearch()
                    {
                        Name = item["name"].Value,
                        Picture = streetViewQuery,
                        PlaceId = item["place_id"].Value,
                        Address = address,
                        PageToken = nextPageToken,
                        Distance = km,
                    });
                }
                return lstPlaceSearch;
            }
            else
                return null;
        }

        public async Task<PlaceDetails> GetPlaceDetails(string query)
        {
            dynamic data_results = await DataService.GetDataFromService(query).ConfigureAwait(false);
            dynamic placeOverview = data_results["result"];

            if (data_results["status"].Value == "OK")
            {
                dynamic openNow = placeOverview["opening_hours"] == null ? "Inconnu" : placeOverview["opening_hours"]["open_now"].Value;
                //dynamic rating = placeOverview["rating"].Value == null ? "" : placeOverview["rating"].Value;
                dynamic website = placeOverview["website"].Value == null ? "Inconnu" : placeOverview["website"].Value;
                //dynamic weekdayText = placeOverview["opening_hours"]["weekday_text"] == null ? "Inconnu" : placeOverview["opening_hours"]["weekday_text"][0].Value;

                string rating = Convert.ToString(placeOverview["rating"].Value);

                PlaceDetails placeDetails = new PlaceDetails()
                {
                    Name = placeOverview["name"].Value,
                    FormattedAddress = placeOverview["formatted_address"].Value,
                    InternationalPhoneNumber = placeOverview["international_phone_number"].Value,
                    Rating = rating,
                    Website = website,
                    //WeekdayText = weekdayText,
                    OpenNow =openNow
                };

                return placeDetails;
            }
            else if(data_results["status"].Value == "ZERO_RESULT")
            {
                return null;
            }
            else
                return null;
        }

        public string GetMatrix(string query)
        {
            dynamic data_results = DataService.GetDataFromService(query).Result;
            dynamic matrixOverview = data_results["rows"][0]["elements"][0];

            if (data_results["status"].Value == "OK")
            {
                dynamic km = matrixOverview["distance"]["text"].Value;
                string str_km = Convert.ToString(km);
                return str_km;
            }
            else
                return null;
        }
    }
}
