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

        public string TextSearchRequestsByLocation(string longitude,string latitude,int radius,string types,string pagetoken = "")
        {
            //return "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location="+latitude+","+longitude+"&radius="+ nearbySearchRaduis + "&types="+nearbySearchTypes+"&key="+key;
            return "https://maps.googleapis.com/maps/api/place/textsearch/json?query=" + types + "&pagetoken=" + pagetoken + "&location="+latitude+","+longitude+"&radius="+radius +"&types=" + types + "&key=" + key;
        }

        public string TextSearchRequestsBySearch(string types,string search)
        {
            return "https://maps.googleapis.com/maps/api/place/textsearch/json?query=" + types + "%20" + search + "&types=" + types + "&key=" + key;
        }

        public string PlaceDetailsResponses(string placeId)
        {
            return "https://maps.googleapis.com/maps/api/place/details/json?placeid="+placeId+"&key=" + key;
        }

        public string DistanceMatrixRequests(string destinationLatitude, string destinationLongitude,string mode)
        {
            return "https://maps.googleapis.com/maps/api/distancematrix/json?origins=" + latitude + "," + longitude + "&destinations=" + destinationLatitude + "," + destinationLongitude + "&mode=" + mode + "&language=fr-FR&key=" + key;
        }

        public async Task<List<PlaceSearch>> GetPlaceSearch(string query, int max_width, int max_height)
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
                    dynamic placePhoto = item["photos"]==null?item["icon"].Value: "https://maps.googleapis.com/maps/api/place/photo?maxwidth=" + max_width + "&maxheight=" + max_height + "&photoreference=" + item["photos"][0]["photo_reference"].Value + "&key=AIzaSyCRaT-68xS7CkjDlvVygW_hWdkBbs4mb-Y";

                    dynamic longitudeDestinations = item["geometry"]["location"]["lng"].Value;
                    dynamic latitudeDestinations = item["geometry"]["location"]["lat"].Value;

                    string str_latitudeDestinations = Convert.ToString(latitudeDestinations);
                    string str_longitudeDestinations =  Convert.ToString(longitudeDestinations);

                    string queryMatrix = DistanceMatrixRequests(str_latitudeDestinations, str_longitudeDestinations, "driving");

                    //string km = GetMatrix(queryMatrix);

                    lstPlaceSearch.Add(new PlaceSearch()
                    {
                        Name = item["name"].Value,
                        Picture = placePhoto ,
                        PlaceId = item["place_id"].Value,
                        Address = address,
                        PageToken = nextPageToken
                    });
                }
                return lstPlaceSearch;
            }
            else
                return null;
        }

        public async Task<List<PlaceDetails>> GetPlaceDetails(string query)
        {
            dynamic data_results = await DataService.GetDataFromService(query).ConfigureAwait(false);
            dynamic placeOverview = data_results["result"];

            if (data_results["status"].Value == "OK")
            {
                List<PlaceDetails> lstPlaceDetails = new List<PlaceDetails>();

                //dynamic openNow = placeOverview["opening_hours"] == null ? "" : placeOverview["opening_hours"]["open_now"].Value;
                //dynamic rating = placeOverview["rating"].Value == null ? "" : placeOverview["rating"].Value;
                //dynamic website = placeOverview["website"].Value == null ? "" : placeOverview["website"].Value;
                //dynamic weekdayText = placeOverview["opening_hours"]["weekday_text"] == null ? "" : placeOverview["opening_hours"]["weekday_text"].Value; 

                lstPlaceDetails.Add(new PlaceDetails()
                {
                    //FormattedAddress = placeOverview["formatted_address"].Value,
                    //InternationalPhoneNumber = placeOverview["international_phone_number"].Value,
                    Name = placeOverview["name"].Value,
                    //Rating = rating,
                    //Website = website,
                    //OpenNow = openNow,
                    //WeekdayText = weekdayText
                });

                return lstPlaceDetails;
            }
            else
                return null;
        }

        public string GetMatrix(string query)
        {
            dynamic data_results = DataService.GetDataFromService(query).Result;
            dynamic matrixOverview = data_results["rows"][0]["elements"];

            if (data_results["status"].Value == "OK")
            {
                dynamic km = matrixOverview["distance"]["text"].Value;
                string p = Convert.ToString(km);
                //var kma= (string)matrixOverview["distance"]["text"].Value;
                
                return p;
            }
            else
                return null;
        }
    }
}
