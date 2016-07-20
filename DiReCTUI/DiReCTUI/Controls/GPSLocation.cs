using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Device.Location;
using System.ComponentModel;

namespace DiReCTUI.Controls
{
    public class GPSLocation : INotifyPropertyChanged
    {

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Properties
        private GeoCoordinateWatcher watcher;
        private double latitude;
        private double longitude;
        private string status;
        public double Latitude
        {
            get
            {
                return latitude;
            }
            set
            {
                if (value == latitude) return;
                latitude = value;
                OnPropertyChanged("Latitude");
            }
        }
        public double Longitude
        {
            get
            {
                return longitude;
            }
            set
            {
                if (value == longitude) return;
                longitude = value;
                OnPropertyChanged("Longitude");
            }
        }
        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                if (status == value) return;
                status = value;
                OnPropertyChanged("Status");
            }
        }

        #endregion

        #region public functions
        public void StartTracking()
        {
            this.watcher = new GeoCoordinateWatcher();
            this.watcher.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(watcher_PositionChanged);
            this.watcher.Start();
            
        }

        void watcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            Longitude = e.Position.Location.Longitude;
            Latitude = e.Position.Location.Latitude;
            Status = "Tracking";
        }

        public void StopTracking()
        {
            if (this.watcher == null)
            {
                return;
            }
            watcher.Stop();
            watcher.Dispose();
            watcher = null;
            latitude = 0;
            longitude = 0;
            status = null;

        }
        #endregion

        #region Constructor
        public GPSLocation()
        {
            StartTracking();
        }
        #endregion



    }
}
