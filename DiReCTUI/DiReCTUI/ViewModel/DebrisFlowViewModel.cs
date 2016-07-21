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
using Microsoft.Maps.MapControl.WPF;
using System.Windows;
using System.Windows.Input;

namespace DiReCTUI.ViewModel
{
    public class DebrisFlowViewModel : ViewModelBase, GPSInterface
    {
        #region Fields
        readonly DebrisFlowRecord _debrisFlowRecord;
        BackgroundInfo _backgroundInfo;
        private ObservableCollection<DraggablePin> _Pushpins;
        private BingMap map;
        private ObservableCollection<Location> SOPLocations;
        private Location currentLocation;
        private DraggablePin currentMarker;
        private Visibility templateVisibility;
        #endregion

        #region GPS properties, fields and functions
        #region fields
        private string status;
        private string template;
        private GeoCoordinateWatcher watcher;
        private double longitude;
        private double latitude;
        

        #endregion

        #region Properties
       
        public string Template
        {
            get
            {
                return template;
            }
            set
            {
                if(value != template)
                {
                    template = value;
                    OnPropertyChanged("Template");
                }
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
                detectCurrentMarker();
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
                detectCurrentMarker();
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
        public DebrisFlowViewModel(BingMap map)
        {

            this._debrisFlowRecord = new DebrisFlowRecord();
            this._backgroundInfo = new BackgroundInfo();
            this._backgroundInfo.RivuletName = "test";

            //map and gps
            //StartTracking();
            Pushpins = new ObservableCollection<DraggablePin>();
            SOPLocations = new ObservableCollection<Location>();
            this.map = map;
            Latitude = 25.04133;
            Longitude = 121.6133;
            Status = "init";
            currentLocation = new Location(Latitude, Longitude);
            this.currentMarker = map.getCurrentMarker();
            this.currentMarker.TouchMove += new EventHandler<TouchEventArgs>(setCurrentMarkerPosition);
            
            //Sop
            setSOPpins();

            //template
            Template = "StatisResource DebrisFlowGeneral";
            TemplateVisibility = Visibility.Hidden;

        }

        

        //not being used 
        //public DebrisFlowViewModel(DebrisFlowRecord dbr, BackgroundInfo bgi)
        //{
        //    this._debrisFlowRecord = dbr;

        //    this._backgroundInfo = new BackgroundInfo();
        //    this._backgroundInfo.RivuletName = "Test";

        //}
        #endregion

        #region Properties
        

        public string RivuletName
        {
            get { return this._backgroundInfo.RivuletName;  }
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
        public Visibility TemplateVisibility
        {
            get { return this.templateVisibility; }
            set
            {
                if (value != this.templateVisibility)
                {
                    templateVisibility = value;
                    OnPropertyChanged("TemplateVisibility");

                }
            }
        }
        #endregion

        #region private helpers
        void setCurrentMarkerPosition(object s, EventArgs e)
        {
            Latitude = 345;
            DraggablePin pin = s as DraggablePin;
            Latitude = pin.Location.Latitude;
            Longitude = pin.Location.Longitude;
            Latitude = 123;
        }

        private void detectCurrentMarker()
        {

            if (map != null)
            {
                Location location = new Location(this.Latitude, this.Longitude);
                map.setCurrentMarkerPosition(location);
                currentLocation = location;
                foreach(Location loc in SOPLocations)
                {
                    if(Math.Abs(loc.Longitude - currentLocation.Longitude) < 0.1)
                    {
                        Status = "yes";
                        this._backgroundInfo = new BackgroundInfo();
                        this._backgroundInfo.RivuletName = "Success";
                        TemplateVisibility = Visibility.Visible;
                        
                    }
                }
            }
            
        }
        #endregion
        
        #region SOP pins
        void setSOPpins()
        {
            SOPLocations.Add(new Location(25.040, 121.6101));
            SOPLocations.Add(new Location(25.043, 121.611));
            SOPLocations.Add(new Location(25.0400233, 121.614));
            char c = 'A';
            foreach(Location location in SOPLocations)
            {
                string label = Char.ToString(c);
                map.addPushPins(location.Latitude, location.Longitude, label);
                c++;
            }
        }

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
