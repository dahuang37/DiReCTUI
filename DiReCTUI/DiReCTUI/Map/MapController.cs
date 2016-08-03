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
    public class MapController : GPSInterface
    {
        private string status;
        private Location location;
        private GeoCoordinateWatcher watcher;
        private DraggablePin currentMarker;
        private MainMap map;

        //Event Hanlder for other class to use when the Location has Changed
        public event EventHandler<LocationChangedEventArgs> LocationChanged;
        protected virtual void OnLocationChanged(LocationChangedEventArgs e)
        {
            this.map.SetCurrentMarkerPosition(e.location);
            if (LocationChanged != null)
                LocationChanged(this, e);
        }
 
        // For display purpose, can be use to show tracking status or if the current marker is in range
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
                    // Add status to Location Event Args if required
                }
            }
        }

        // This indicates user's location
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

        // set up the map and marker
        public MapController(MainMap map)
        {
            this.map = map;
            this.currentMarker = map.GetCurrentMarker();
            if (map != null)
            {
                map.PreviewMouseUp += new MouseButtonEventHandler(SetCurrentPosition);
            }
        }

        // set the Location to where the Touch/Mouse Click
        public void SetCurrentPosition(object s, MouseEventArgs e)
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

        
        // Check if Range is less than radius
        public bool LocationIsInRange(Location location, double radius = 150)
        {
            double result = GetDistance(this.Location.Latitude, location.Latitude, this.Location.Longitude, location.Longitude);
            if (result < radius)
                return true;
            return false;
        }
        // formula to calculate distance between two Location points
        private double GetDistance(double latitude1, double latitude2, double longitude1, double longitude2)
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

       
        /// add SOP pushpin, Pushpin with circle around the center
        public void AddPushPinWithCircle(Location location, List<string> label, double radius = 0.15)
        {
            map.DrawCircle(location, radius);
            string result = "";
            foreach(string str in label)
            {
                result += str + ", ";
            }
            map.AddPushPins(location, result);
        }

    }
}
