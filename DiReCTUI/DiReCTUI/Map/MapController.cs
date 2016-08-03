using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Device.Location;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static DiReCTUI.Controls.SOP;
using DiReCTUI.Controls;


namespace DiReCTUI.Map
{
    public class MapController : INotifyPropertyChanged, GPSInterface
    {
        private string status;
        private Location location;
        private GeoCoordinateWatcher watcher;
        private DraggablePin currentMarker;
        private BingMap map;

        public event EventHandler<LocationChangedEventArgs> LocationChanged;

        protected virtual void OnLocationChanged(LocationChangedEventArgs e)
        {
            if (LocationChanged != null)
                LocationChanged(this, e);
        }
        
        public double radius { get; private set; }

        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                if (value != status)
                {
                    status = value;
                    OnPropertyChanged("Status");
                }
            }
        }

        public Location Location
        {
            get { return location; }
            set
            {
                if (value == location) return;
                location = value;
                OnLocationChanged(new LocationChangedEventArgs(value));
            }
        }

        public MapController(BingMap map)
        {
            this.map = map;
            this.currentMarker = map.GetCurrentMarker();
            if (map != null)
            {
                map.MouseUp += new MouseButtonEventHandler(SetCurrentMarkerPosition);
            }
        }
        // set the Location to where the Touch/Mouse Click
        public void SetCurrentMarkerPosition(object s, MouseEventArgs e)
        {
            var mouseMapPosition = e.GetPosition(map);
            var mouseGeocode = map.ViewPortToLocation(mouseMapPosition);
            Location location = new Location(mouseGeocode.Latitude, mouseGeocode.Longitude);
            this.Location = location;
        }

        /// <summary>
        /// initialize the watcher
        /// and add eventhandler such that when user's GPS location changed, our app will detect 
        /// </summary>
        public void StartTracking()
        {
            this.watcher = new GeoCoordinateWatcher();
            this.watcher.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(Watcher_PositionChanged);
            this.watcher.Start();
        }
        public void Watcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            var lon = e.Position.Location.Longitude;
            var lat = e.Position.Location.Latitude;
            this.Location = new Location(lat, lon);
            
        }
        public void StopTracking()
        {
            if (this.watcher == null)
            {
                return;
            }
            watcher.Stop();
            watcher.Dispose();
        }

        
        //public void OnLocationChanged(Location location)
        //{
        //    this.map.SetCurrentMarkerPosition(location);
        //    //DetectCurrentMarkerIsInRange(location);
        //}

        /// <summary>
        /// check if the user has get in range of one of the SOPlocations 
        /// </summary>
        /// <param name="loc"></param>
        //public async void DetectCurrentMarkerIsInRange(Location location)
        //{
        //    if (map != null)
        //    {
        //        if (LocationSOPs.Count == 0)
        //        {
        //            Status = "No location";
        //            return;
        //        }
        //        foreach (LocationSOP locationSOP in LocationSOPs)
        //        {
        //            var sopLocation = locationSOP.Location;
        //            double result = RangeLength(this.Location.Latitude, sopLocation.Latitude, this.Location.Longitude, sopLocation.Longitude);
        //            Status = result.ToString();
        //            if (CheckInRange(sopLocation.Latitude, sopLocation.Longitude) == true)
        //            {
        //                Status = "In range";
        //                //pop up the dialog when user's in range
        //                var metroWindow = (Application.Current.MainWindow as MetroWindow);
        //                await metroWindow.ShowMessageAsync("In this location, the tasks to complete are: \n", locationSOP.SOPTask + " " + result);
        //                return;
        //            }

        //        }
        //        Status = "not in range";
        //    }

        //}

        // helper functions for "detectCurrentMarker
        // Check if Range is less than radius
        private bool CheckInRange(double latitude, double longitude)
        {
            double result = RangeLength(this.Location.Latitude, latitude, this.Location.Longitude, longitude);
            if (result < this.radius)
                return true;
            return false;
        }

        private double RangeLength(double latitude1, double latitude2, double longitude1, double longitude2)
        {
            var R = 6371e3; // metres
            var φ1 = ConvertToRadians(latitude1);
            var φ2 = ConvertToRadians(latitude2);
            var Δφ = ConvertToRadians(latitude2 - latitude1);
            var Δλ = ConvertToRadians(longitude2 - longitude1);

            var a = Math.Sin(Δφ / 2) * Math.Sin(Δφ / 2) +
                    Math.Cos(φ1) * Math.Cos(φ2) *
                    Math.Sin(Δλ / 2) * Math.Sin(Δλ / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            var d = R * c;

            return d;
        }

        private double ConvertToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }



        public void VerifyPropertyName(string propertyName)
        {
            //Verify that the property is real, public,instance property on this object
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                string msg = "INvalid property name: " + propertyName;

                if (this.ThrowOnInvalidPropertyName)
                {
                    throw new Exception(msg);
                }
                else
                {
                    Debug.Fail(msg);
                }
            }
        }

        protected virtual bool ThrowOnInvalidPropertyName { get; private set; }
        ///<summary>
        ///Raised when a property on this object has a new value
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        ///<summary>
        ///Raises this object's PropertyChange Event
        /// </summary>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            this.VerifyPropertyName(propertyName);

            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }

        }
       
    }
}
