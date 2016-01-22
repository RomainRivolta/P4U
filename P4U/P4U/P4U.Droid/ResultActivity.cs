using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;

namespace P4U.Droid
{
    [Activity(Label = "P4U")]
    public class ResultActivity : AppCompatActivity
    {
        private string SELECT_TYPE;
        private string QUERY;
        private const int MAX_WIDTH=160;
        private const int MAX_HEIGHT= 160;
        private int RADIUS;
        private List<PlaceSearch> RESULT_PLACE_SEARCH;
        private Core core = new Core();
        private int settingsPerimeter;
        private string settingsTransport;

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_toolbar, menu);
            var item = menu.FindItem(Resource.Id.action_search);
            var searchView = MenuItemCompat.GetActionView(item);
            SearchView _searchView = searchView.JavaCast<SearchView>();
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

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ResultSearch);

            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayShowHomeEnabled(false);
            SupportActionBar.SetLogo(Resource.Drawable.Icon);
            SupportActionBar.SetDisplayUseLogoEnabled(true);
            string query = string.Empty;

            core.latitude = Intent.GetStringExtra("latitude");
            core.longitude = Intent.GetStringExtra("longitude");

            ISharedPreferences sharedPref = PreferenceManager.GetDefaultSharedPreferences(this);
            settingsPerimeter = sharedPref.GetInt("keyPerimetre", 1);
            RADIUS = settingsPerimeter* 1000;
            settingsTransport = sharedPref.GetString("keyTransport", null);


            if (string.IsNullOrEmpty(Intent.GetStringExtra("SelectType")))
            {
                QUERY = Intent.GetStringExtra("Search");
                query = core.TextSearchRequestsBySearch(QUERY);
            }

            else
            {
                SELECT_TYPE = Intent.GetStringExtra("SelectType");
                QUERY = SELECT_TYPE;
                SupportActionBar.Title = Intent.GetStringExtra("SelectName");
                query = core.TextSearchRequestsByLocation(settingsPerimeter, QUERY, SELECT_TYPE);
            }

            
            RESULT_PLACE_SEARCH = core.GetPlaceSearch(query, MAX_WIDTH, MAX_HEIGHT,settingsTransport,"200x200").Result;
            FillListView(RESULT_PLACE_SEARCH);

            ListView lstView = FindViewById<ListView>(Resource.Id.listViewResult);
            lstView.ItemClick += ItemListView_Click;
        }

        private void ItemListView_Click(object sender, AdapterView.ItemClickEventArgs e)
        {
            if (e.Position != RESULT_PLACE_SEARCH.Count)
            {
                Intent resultActivity = new Intent(this, typeof(DetailActivity));
                resultActivity.PutExtra("ResulPlaceId", RESULT_PLACE_SEARCH[e.Position].PlaceId);
                resultActivity.PutExtra("ResulPicture", RESULT_PLACE_SEARCH[e.Position].Picture);
                StartActivity(resultActivity);
            }
            else
            {
                string query = core.TextSearchRequestsByLocation(RADIUS, QUERY, SELECT_TYPE, pagetoken: RESULT_PLACE_SEARCH.FirstOrDefault().PageToken);
                RESULT_PLACE_SEARCH = core.GetPlaceSearch(query, MAX_WIDTH, MAX_HEIGHT,settingsTransport,"200x200").Result;
                RefreshListView(RESULT_PLACE_SEARCH);
            }
        }


        private void FillListView(List<PlaceSearch> result)
        {
            if (result != null)
            {
                ListView lstview = FindViewById<ListView>(Resource.Id.listViewResult);
                lstview.Adapter = new Base_Adapter.ListViewAdapter(this, result);
            }
        }

        private void RefreshListView(List<PlaceSearch> result)
        {
            if (result != null)
            {
                ListView lstview = FindViewById<ListView>(Resource.Id.listViewResult);
                ((Base_Adapter.ListViewAdapter)lstview.Adapter).AddIListItems(result);
            }
        }
    }
}