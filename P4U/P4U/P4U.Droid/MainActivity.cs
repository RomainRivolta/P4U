using Android.App;
using Android.Content;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Util;
using Android.Widget;
using System;
using P4U.Droid.Singleton;
using Android.Views;

namespace P4U.Droid
{
    [Activity(Label = "@string/app_name")]
    public class MainActivity : AppCompatActivity
    {
        LocationManager locMgr;
        string locationProvider;
        const int MAX_WIDTH = 160;
        const int MAX_HEIGHT = 160;

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.test, menu);
            var item = menu.FindItem(Resource.Id.action_search);
            return true;
        }


        protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

            // Get a reference to the locationManager
            locMgr = GetSystemService(Context.LocationService) as LocationManager;

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Home";

            // Grid view home
            var gridView = FindViewById<GridView>(Resource.Id.gridViewHome);
            var lstGrid = GridViewHome.getGridViewHome();
            gridView.Adapter = new Base_Adapter.GridViewHomeAdapter(this, lstGrid);
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
                locMgr.RequestLocationUpdates(locationProvider, 2000, 1, SingletonLocation.Instance);
            }
            else
            {
                Log.Info("Location", "No location providers available");
            }
        }

        protected override void OnPause()
        {
            base.OnPause();
            locMgr.RemoveUpdates(SingletonLocation.Instance);
        }
      
        void GridViewHome_Click(object sender,AdapterView.ItemClickEventArgs args)
        {
            var lstGrid = GridViewHome.getGridViewHome();
            if (Singleton.SingletonLocation.Instance.currentLocation == null)
            {
                Toast.MakeText(this, "Can't determine the current position", ToastLength.Long).Show();
                return;
            }
            string selectItem = lstGrid[args.Position].Type;
            string longitude = SingletonLocation.Instance.currentLocation.Longitude.ToString().Replace(",", ".");
            string latitude = SingletonLocation.Instance.currentLocation.Latitude.ToString().Replace(",", ".");

            Intent resultActivity = new Intent(this, typeof(ResultActivity));
            resultActivity.PutExtra("SelectType", selectItem);
            resultActivity.PutExtra("longitude", longitude);
            resultActivity.PutExtra("latitude", latitude);

            StartActivity(resultActivity);
        }
    }
}


