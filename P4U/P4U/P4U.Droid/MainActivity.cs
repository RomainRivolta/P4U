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
using Android.Support.V4.View;
using Android.Gms.Common.Apis;
using Android.Gms.Location;
using Android.Gms.Common;
using P4U.Droid.MyService;
using Java.Lang;

namespace P4U.Droid
{
    [Activity(Label = "@string/app_name",Icon = "@drawable/Icon", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        const int MAX_WIDTH = 160;
        const int MAX_HEIGHT = 160;
        bool isConfigurationChange = false;
        bool mBound = false;
        GoogleLocationServiceBinder binder;
        GoogleLocationServiceConnection googleConnection;

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_toolbar, menu);
            var item = menu.FindItem(Resource.Id.action_search);
            var searchView = MenuItemCompat.GetActionView(item);
            SearchView _searchView = searchView.JavaCast<SearchView>();
            _searchView.QueryTextSubmit += (s, e)=>{

                string search = e.Query;
                string longitude = SingletonLocation.Instance.currentLocation.Longitude.ToString().Replace(",", ".");
                string latitude = SingletonLocation.Instance.currentLocation.Latitude.ToString().Replace(",", ".");

                Intent resultActivity = new Intent(this, typeof(ResultActivity));
                resultActivity.PutExtra("Search", search);
                resultActivity.PutExtra("longitude", longitude);
                resultActivity.PutExtra("latitude", latitude);
                StartActivity(resultActivity);

            };

            return base.OnCreateOptionsMenu(menu);
        }


        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.action_settings:
                    Intent intentSettings = new Intent(this, typeof(SettingsActivity));
                    StartActivity(intentSettings);
                    break;

            }
            return base.OnOptionsItemSelected(item);
        }

        protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

            StartService(new Intent(this, typeof(MyService.GoogleLocationService)));



            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);
            
            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Home";
            SupportActionBar.SetDisplayShowHomeEnabled(true);
            SupportActionBar.SetLogo(Resource.Drawable.Icon);
            SupportActionBar.SetDisplayUseLogoEnabled(true);

            // Grid view home
            var gridView = FindViewById<GridView>(Resource.Id.gridViewHome);
            var lstGrid = GridViewHome.getGridViewHome();
            gridView.Adapter = new Base_Adapter.GridViewHomeAdapter(this, lstGrid);
            gridView.ItemClick += GridViewHome_Click;

            googleConnection = LastCustomNonConfigurationInstance as GoogleLocationServiceConnection;
            if (googleConnection != null)
                binder = googleConnection.Binder;
        }

        protected override void OnStart()
        {
            base.OnStart();
            Intent intentService = new Intent(this, typeof(GoogleLocationService));
            GoogleLocationServiceConnection mconnection = new GoogleLocationServiceConnection(this);
            ApplicationContext.BindService(intentService, mconnection, Bind.AutoCreate);
        }

        protected override void OnStop()
        {
            base.OnStop();
            
        }

        protected override void OnResume()
        {
            base.OnResume();
        }

        protected override void OnPause()
        {
            base.OnPause();
        }
      
        void GridViewHome_Click(object sender,AdapterView.ItemClickEventArgs args)
        {
            var lstGrid = GridViewHome.getGridViewHome();

            string selectItemType = lstGrid[args.Position].Type;
            string selectItemName = lstGrid[args.Position].Name;


            var a = binder.GetGoogleLocationService();
              var currentLocation = a.getLocation();
            
            string longitude = currentLocation.Longitude.ToString().Replace(",", ".");
            string latitude = currentLocation.Latitude.ToString().Replace(",", ".");

            Intent resultActivity = new Intent(this, typeof(ResultActivity));
            resultActivity.PutExtra("SelectType", selectItemType);
            resultActivity.PutExtra("SelectName", selectItemName);
            resultActivity.PutExtra("longitude", longitude);
            resultActivity.PutExtra("latitude", latitude);

            StartActivity(resultActivity);
        }

        public override Java.Lang.Object OnRetainCustomNonConfigurationInstance()
        {
            base.OnRetainCustomNonConfigurationInstance();
            isConfigurationChange = true;
            return googleConnection;
        }

        class GoogleLocationServiceConnection : Java.Lang.Object, IServiceConnection
        {
            MainActivity activity;
            GoogleLocationServiceBinder binder;

            public GoogleLocationServiceBinder Binder
            {
                get
                {
                    return binder;
                }
            }

            public GoogleLocationServiceConnection(MainActivity activity)
            {
                this.activity = activity;
            }
            public void OnServiceConnected(ComponentName name, IBinder service)
            {
                GoogleLocationServiceBinder googleBinder = service as GoogleLocationServiceBinder;
                if (googleBinder != null)
                {
                    var binder = service as GoogleLocationServiceBinder;
                    activity.binder = binder;
                    activity.mBound = true;
                    this.binder = service as GoogleLocationServiceBinder;
                }
            }

            public void OnServiceDisconnected(ComponentName name)
            {
                activity.mBound = false;
            }
        }
    }
}


