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
using Android.Locations;

namespace P4U.Droid.Singleton
{
    public class SingletonLocation: Java.Lang.Object,ILocationListener
    {
        private static readonly SingletonLocation instance = new SingletonLocation();
        public Location currentLocation;

        private SingletonLocation() { }

        public static SingletonLocation Instance
        {
            get
            {
                return instance;
            }
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
            throw new NotImplementedException();
        }


    }
}