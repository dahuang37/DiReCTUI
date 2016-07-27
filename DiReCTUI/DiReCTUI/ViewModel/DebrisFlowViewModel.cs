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
using static DiReCTUI.Controls.SOP;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;
using System.Windows.Controls;

namespace DiReCTUI.ViewModel
{
    public class DebrisFlowViewModel : ViewModelBase, GPSInterface
    {
        #region Fields
        DebrisFlowRecord _debrisFlowRecord;
        BackgroundInfo.DebrisFlowRelated _backgroundInfo;
        
        private ObservableCollection<DraggablePin> _Pushpins;
        private BingMap map;
        private ObservableCollection<LocationSOP> LocationSOPs;
        private Location currentLocation;
        private DraggablePin currentMarker;
        private Visibility templateVisibility;
        private double radius;
        //private ContentPresenter content;
        
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
        
        public DebrisFlowViewModel(BingMap map, DebrisFlowRecord dbRecord, BackgroundInfo.DebrisFlowRelated bgInfo)
        {
            this._debrisFlowRecord = dbRecord;
            this._backgroundInfo = bgInfo;
            
            this._backgroundInfo.RivuletName = "test";
            this.LocationSOPs = new ObservableCollection<LocationSOP>();
            this.radius = 0.150;

            //map and gps
            //StartTracking();
            Pushpins = new ObservableCollection<DraggablePin>();
            
            this.map = map;
            Latitude = 25.04;
            Longitude = 121.612;
            //Status = "init";
            currentLocation = new Location(Latitude, Longitude);
            this.currentMarker = map.getCurrentMarker();
            
            this.map.MouseUp += new MouseButtonEventHandler(setCurrentMarkerPosition);

            //Sop
            FakeSOP sop = new FakeSOP();
            LocationSOPs = sop.getFakeSOP().getLocationSOP();
            setUpSOP();

            //template
            TemplateVisibility = Visibility.Collapsed;

            detectCurrentMarker();
            

        }
        #endregion

        #region Properties
        
        // Background Infos
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
        public string RivuletCode
        {
            get { return this._backgroundInfo.RivuletCode; }
            set
            {
                if(value == _backgroundInfo.RivuletCode)
                {
                    return;
                }
                _backgroundInfo.RivuletCode = value;
                base.OnPropertyChanged("RivuletCode");
            }
        }
        public string AdministrativeArea
        {
            get { return this._backgroundInfo.AdministrativeArea; }
            set
            {
                if(value != this._backgroundInfo.AdministrativeArea)
                {
                    this._backgroundInfo.AdministrativeArea = value;
                    OnPropertyChanged("AdministrativeArea");
                }
            }
        }

        // DebrisFlow Model


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
        private bool popUpBool;
        public bool PopUpBool
        {
            get
            {
                return this.popUpBool;
            }
            set
            {
                if(value != popUpBool)
                {
                    popUpBool = value;
                    OnPropertyChanged("PopUpBool");
                }
            }
        }

        private IEnumerable<BackgroundInfo.DebrisFlowRelated.test> testEnum;
        public IEnumerable<BackgroundInfo.DebrisFlowRelated.test> TestEnum
        {
            get
            {
                return Enum.GetValues(typeof(BackgroundInfo.DebrisFlowRelated.test)).Cast<BackgroundInfo.DebrisFlowRelated.test>();
            }
            set
            {
                testEnum = value;
                OnPropertyChanged("TestEnum");
            }
        }
            
        #endregion

        #region private helpers
        void setCurrentMarkerPosition(object s, MouseEventArgs e)
        {
            
            var mouseMapPosition = e.GetPosition(map);
            var mouseGeocode = map.ViewPortToLocation(mouseMapPosition);
            Longitude = mouseGeocode.Longitude;
            Latitude = mouseGeocode.Latitude;
            
        }

        private async void detectCurrentMarker()
        {

            if (map != null)
            {
                Location location = new Location(this.Latitude, this.Longitude);
                map.setCurrentMarkerPosition(location);
                currentLocation = location;
                if(LocationSOPs.Count == 0)
                {
                    Status = "not in range";
                    TemplateVisibility = Visibility.Collapsed;
                }
                foreach(LocationSOP locSop in LocationSOPs)
                {
                    var loc = locSop.location;
                    if (checkInRange(loc.Latitude, loc.Longitude)){
                        Status = "In range";
                        //var test = new BackgroundInfo().DebrisBackgroundInfo;
                        //test.RivuletName = "hey";
                        //this.content.Content = test;

                        TemplateVisibility = Visibility.Visible;
                       

                        return;

                    }
                    else
                    {
                        Status = "not in range";
                        TemplateVisibility = Visibility.Collapsed;
                    }
                   
                }
            }
            
        }
        //check if the loc is in rnage of any SOP pins
        private bool checkInRange(double lat, double lon)
        {
             
            double result = RangeLength(Latitude, lat, Longitude, lon);
            Status = result + "" ;
            if (result < 150)
            {
                return true;
            }
            return false;
        }

        private double RangeLength(double lat1, double lat2, double lon1, double lon2)
        {
            var R = 6371e3; // metres
            var φ1 = ConvertToRadians(lat1);
            var φ2 = ConvertToRadians(lat2);
            var Δφ = ConvertToRadians(lat2 - lat1);
            var Δλ = ConvertToRadians(lon2 - lon1);

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
        #endregion

        #region SOP pins
        public void setUpSOP()
        {
            if(this.LocationSOPs != null)
            {
                foreach(LocationSOP item in LocationSOPs)
                {
                    string label = "Task: "+ item.SOPTask + ", ID: " + item.ID;
                    map.addSOPPushPin(item.location, label, this.radius);
                }
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
