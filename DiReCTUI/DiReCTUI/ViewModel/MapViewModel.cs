using DiReCTUI.Map;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Collections.ObjectModel;
using static DiReCTUI.Controls.SOP;
using DiReCTUI.Controls;

namespace DiReCTUI.ViewModel
{
    public class MapViewModel : ViewModelBase, GPSInterface
    {
        private string status;
        private Location location;
        private GeoCoordinateWatcher watcher;

        protected BingMap map;
        public ObservableCollection<LocationSOP> LocationSOPs;
        protected double radius;

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
                LocationChanged(value);
                base.OnPropertyChanged("Location");
            }
        }
        
        protected MapViewModel(BingMap map)
        {
            this.map = map;
            if(this.map != null)
            {
                map.MouseUp += new MouseButtonEventHandler(SetCurrentMarkerPosition);
            }
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
            //Status = "Tracking";
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

        /// <summary>
        /// this functioned is called whenever user's location has changed
        /// right now the change is detected by the MouseUp trigger,
        /// later it should be added to the GeocoordinateWatcher position change event
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        /// 
        
        public void SetCurrentMarkerPosition(object s, MouseEventArgs e)
        {
            var mouseMapPosition = e.GetPosition(map);
            var mouseGeocode = map.ViewPortToLocation(mouseMapPosition);
            Location location = new Location(mouseGeocode.Latitude, mouseGeocode.Longitude);
            this.Location = location;
        }

        public void LocationChanged(Location location)
        {
            this.map.SetCurrentMarkerPosition(location);
            DetectCurrentMarkerIsInRange(location);
        }

        /// <summary>
        /// check if the user has get in range of one of the SOPlocations 
        /// </summary>
        /// <param name="loc"></param>
        public async void DetectCurrentMarkerIsInRange(Location location)
        {
            if (map != null)
            {
                if (LocationSOPs.Count == 0)
                {
                    Status = "No location";
                    return;
                }
                foreach (LocationSOP locationSOP in LocationSOPs)
                {
                    var sopLocation = locationSOP.Location;
                    double result = RangeLength(this.Location.Latitude, sopLocation.Latitude, this.Location.Longitude, sopLocation.Longitude);
                    Status = result.ToString();
                    if (CheckInRange(sopLocation.Latitude, sopLocation.Longitude) == true)
                    {
                        Status = "In range";
                        //pop up the dialog when user's in range
                        var metroWindow = (Application.Current.MainWindow as MetroWindow);
                        await metroWindow.ShowMessageAsync("In this location, the tasks to complete are: \n", locationSOP.SOPTask + " " + result);
                        return;
                    }

                }
                Status = "not in range";
            }

        }
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
       
    }
}
