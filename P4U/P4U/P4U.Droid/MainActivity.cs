using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;

namespace P4U.Droid
{
	[Activity (Label = "P4U.Droid", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		int count = 1;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            var gridView = FindViewById<GridView>(Resource.Id.gridViewHome);
            var lstGrid = GridViewHome.getGridViewHome();
            gridView.Adapter = new Base_Adapter.GridViewHomeAdapter(this,lstGrid);

            // Get our button from the layout resource,
            // and attach an event to it
            Button buttonSearch = FindViewById<Button> (Resource.Id.search);

            buttonSearch.Click += ButtonSearch_Click;
		}

        void ButtonSearch_Click(object sender, EventArgs eventArgs)
        {
            var result = Core.GetPlaceTextSearch().Result;
            if (result != null)
            {
                ListView lstview = FindViewById<ListView>(Resource.Id.listView1);

                //lstview.Adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, result);
                lstview.Adapter = new Base_Adapter.ListViewAdapter(this, result);
            }
        }
    }
}


