using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using Android.Locations;
using Android.Util;
using System.Collections;

namespace P4U.Droid
{
	[Activity (Label = "@string/app_name")]
	public class MainActivity : Activity, ILocationListener
    {
        LocationManager locMgr;
        string locationProvider;
        Location currentLocation;
        const int MAX_WIDTH = 160;
        const int MAX_HEIGHT = 160;

        protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

            // Get a reference to the locationManager
            locMgr = GetSystemService(Context.LocationService) as LocationManager;

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            // Grid view home
            var gridView = FindViewById<GridView>(Resource.Id.gridViewHome);
            var lstGrid = GridViewHome.getGridViewHome();
            gridView.Adapter = new Base_Adapter.GridViewHomeAdapter(this,lstGrid);
            gridView.ItemClick += GridViewHome_Click;
		}

        protected override void OnResume()
        {
            base.OnResume();

            Criteria locationCriteria = new Criteria();
            locationCriteria.Accuracy = Accuracy.Fine;
            locationCriteria.PowerRequirement = Power.Medium;

            locationProvider = locMgr.GetBestProvider(locationCriteria, true);
            if (locationProvider != null)
            {
                locMgr.RequestLocationUpdates(locationProvider, 2000, 1, this);
            }
            else
            {
                Log.Info("Location", "No location providers available");
            }
        }

        protected override void OnPause()
        {
            base.OnPause();
            locMgr.RemoveUpdates(this);
        }
      
        void GridViewHome_Click(object sender,AdapterView.ItemClickEventArgs args)
        {
            var lstGrid = GridViewHome.getGridViewHome();
            if (currentLocation == null)
            {
                Toast.MakeText(this, "Can't determine the current position", ToastLength.Long).Show();
                return;
            }

            string selectItem = lstGrid[args.Position].Type;
            string longitude = currentLocation.Longitude.ToString().Replace(",", ".");
            string latitude = currentLocation.Latitude.ToString().Replace(",", ".");

            Core core = new Core();
            core.latitude = latitude;
            core.longitude = longitude;
            string query = core.TextSearchRequestsByLocation(longitude, latitude, 2000, selectItem);
            var resultPlaceSearch = core.GetPlaceSearch(query, MAX_WIDTH, MAX_HEIGHT).Result;

            Intent resultActivity = new Intent(this, typeof(ResultActivity));
            resultActivity.PutExtra("SelectType", selectItem);
            var res = Newtonsoft.Json.JsonConvert.SerializeObject(resultPlaceSearch);
            resultActivity.PutExtra("ResultPlaceSearch", res);

            StartActivity(resultActivity);
        }

        public void OnLocationChanged(Location location)
        {
            currentLocation = location;
            currentLocation.Latitude = location.Latitude;
            currentLocation.Longitude = location.Longitude;
        }

        public void OnProviderDisabled(string provider)
        {
            throw new NotImplementedException();
        }

        public void OnProviderEnabled(string provider)
        {
            throw new NotImplementedException();
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
            Toast.MakeText(this, "On status changed", ToastLength.Long).Show();
        }
    }
}


