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
using Android.Gms.Common.Apis;
using Android.Gms.Common;
using Android.Gms.Location;
using Android.Locations;

namespace P4U.Droid.Singleton
{
    public class SingletonGoogleLocation:GoogleApiClient.IConnectionCallbacks,GoogleApiClient.IOnConnectionFailedListener
    {
        private static readonly SingletonGoogleLocation instance = new SingletonGoogleLocation();

        private SingletonGoogleLocation() { }
        public Location location;

        public static SingletonGoogleLocation Instance
        {
            get
            {
                return instance;
            }

        }

        public IntPtr Handle
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void OnConnected(Bundle connectionHint)
        {
            //var mLastLocation = LocationServices.FusedLocationApi.GetLastLocation();
            //if (mLastLocation != null)
            //{
            //    location.Latitude = mLastLocation.Latitude;
            //    location.Longitude =  mLastLocation.Longitude;
            //}
        }

        public void OnConnectionFailed(ConnectionResult result)
        {
            throw new NotImplementedException();
        }

        public void OnConnectionSuspended(int cause)
        {
            throw new NotImplementedException();
        }
    }
}