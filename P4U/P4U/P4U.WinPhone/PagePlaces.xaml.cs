using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace P4U.WinPhone
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PagePlaces : Page
    {

        private string SELECT_TYPE;
        private const int MAX_WIDTH = 160;
        private const int MAX_HEIGHT = 160;
        private string LONGITUDE;
        private string LATITUDE;
        private int RADIUS = 10000;
        private List<PlaceSearch> RESULT_PLACE_SEARCH;

        public PagePlaces()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;

            this.GetLocation();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.
            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
            ListView list = e.Parameter as ListView;
            SELECT_TYPE = list.Tag.ToString();

            this.GetLocation();
        }



        private async void GetLocation()
        {
            Geolocator geolocator = new Geolocator();
            geolocator.DesiredAccuracyInMeters = 50;

            try
            {
                Geoposition geoposition = await geolocator.GetGeopositionAsync(
                     maximumAge: TimeSpan.FromMinutes(5),
                     timeout: TimeSpan.FromSeconds(10)
                    );
                LONGITUDE = geoposition.Coordinate.Longitude.ToString("0.00");
                LATITUDE = geoposition.Coordinate.Latitude.ToString("0.00");
                
                //SELECT_TYPE = "Parking";
                Core myCore = new Core();
                myCore.latitude = LATITUDE;
                myCore.longitude = LONGITUDE;
                string query = myCore.TextSearchRequestsByLocation(RADIUS, SELECT_TYPE, "drive", "");
                RESULT_PLACE_SEARCH = await myCore.GetPlaceSearch(query, MAX_WIDTH, MAX_HEIGHT,"drive", "200x200");

                if (RESULT_PLACE_SEARCH != null)
                {
                    ListBoxPlaces.DataContext = RESULT_PLACE_SEARCH;
                }
            }
            //If an error is catch 2 are the main causes: the first is that you forgot to include ID_CAP_LOCATION in your app manifest. 
            //The second is that the user doesn't turned on the Location Services
            catch (Exception ex)
            {
                //exception
            }
        }
    }
}
