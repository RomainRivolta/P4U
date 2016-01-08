using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace P4U.Droid
{
    [Activity(Label = "DetailActivity")]
    public class DetailActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.DetailsSearch);
            // Create your application here

            var resultMatrix = Intent.GetStringExtra("ResultMatrix");
            var lstResultMatrix = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PlaceDetails>>(resultMatrix).FirstOrDefault();

            FindViewById<TextView>(Resource.Id.textViewDetailsName).Text = lstResultMatrix.Name;
            

        }
    }
}