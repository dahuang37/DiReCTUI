using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DiReCTUI.Model;
using DiReCTUI.Map;
using DiReCTUI.Controls;
using System.Collections.ObjectModel;
using System.Device.Location;
namespace DiReCTUI.ViewModel
{
    public class DebrisFlowViewModel : ViewModelBase, GPSInterface
    {
        #region Fields
        readonly DebrisFlowRecord _debrisFlowRecord;
        readonly BackgroundInfo _backgroundInfo;
        private ObservableCollection<DraggablePin> _Pushpins;
        private GPSLocation gps;
        #endregion

        #region GPS properties, fields and functions
        #region fields
        private string status;
        private GeoCoordinateWatcher watcher;
        private double longitude;
        private double latitude;

        #endregion

        #region Properties
        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                if(value != status)
                {
                    status = value;
                    OnPropertyChanged("Status");
                }
            }
        }
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
                if (value == latitude) return;
                longitude = value;
                base.OnPropertyChanged("Longitude");
            }
        }
        #endregion

        #region public function
        public void StartTracking()
        {
            this.watcher = new GeoCoordinateWatcher();
            this.watcher.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(watcher_PositionChanged);
            this.watcher.Start();

        }

        public void watcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
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
        #endregion

        #region Constructor
        public DebrisFlowViewModel()
        {
            StartTracking();
            this._debrisFlowRecord = new DebrisFlowRecord();
            this.gps = new GPSLocation();
            this._backgroundInfo = new BackgroundInfo();
            this._backgroundInfo.RivuletName = "Test";
            Pushpins = new ObservableCollection<DraggablePin>();
           

        }
        public DebrisFlowViewModel(DebrisFlowRecord dbr, BackgroundInfo bgi)
        {
            this._debrisFlowRecord = dbr;

            this._backgroundInfo = new BackgroundInfo();
            this._backgroundInfo.RivuletName = "Test";

        }
        #endregion

        #region Properties
        public string RivuletName
        {
            get { return "test";  }
            set
            {
                if (value == _backgroundInfo.RivuletName)
                    return;
                _backgroundInfo.RivuletName = value;
                base.OnPropertyChanged("RivuletName");
            }
        }
        #endregion

        #region Display Properties
        
        #endregion

        #region private helpers

        #endregion

        #region pins functions and properties
       

        public ObservableCollection<DraggablePin> Pushpins
        {
            get { return _Pushpins; }
            set
            {
                _Pushpins = value;
                base.OnPropertyChanged("Pushpins");
            }
        }
        #endregion
    }
}
