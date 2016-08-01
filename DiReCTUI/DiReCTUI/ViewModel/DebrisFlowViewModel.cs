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
using DiReCTUI.Views;

namespace DiReCTUI.ViewModel
{
    public class DebrisFlowViewModel : ViewModelBase, GPSInterface
    {
        #region Fields

        
        DebrisFlowRecord _debrisFlowRecord;
        BackgroundInfo.DebrisFlowRelated _backgroundInfo;
        DebrisFlowCollection _debrisFlowCollection;

        IDialogCoordinator _dialogCoordinator;
        
        private BingMap map;
        private ObservableCollection<LocationSOP> LocationSOPs;
        private Location currentLocation;
        private DraggablePin currentMarker;
        private double radius;
        
        #endregion

        #region GPS properties, fields and functions
        #region fields
        private string status;
        private Location location;
        private GeoCoordinateWatcher watcher;
        
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
        
        public Location Location
        {
            get { return location; }
            set
            {
                if (value == location) return;
                location = value;

                detectCurrentMarkerIsInRange(value);
                base.OnPropertyChanged("Location");
            }
        }
        #endregion

        #region public function

        /// <summary>
        /// initialize the watcher
        /// and add eventhandler such that when user's GPS location changed, our app will detect 
        /// </summary>
        public void StartTracking()
        {
            this.watcher = new GeoCoordinateWatcher();
            this.watcher.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(watcher_PositionChanged);
            this.watcher.Start();

        }

        public void watcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
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
        #endregion
        #endregion

        #region Constructor
        
        public DebrisFlowViewModel(BingMap map, DebrisFlowRecord dbRecord, BackgroundInfo.DebrisFlowRelated bgInfo, DebrisFlowCollection dbCollection)
        {
            this._debrisFlowRecord = dbRecord;
            this._backgroundInfo = bgInfo;
            this._debrisFlowCollection = dbCollection;

            this._dialogCoordinator = new DialogCoordinator();

            this.LocationSOPs = new ObservableCollection<LocationSOP>();
            this.radius = 150.0;
            
            //map and gps
            //StartTracking();
            this.map = map;
            this.Location = new Location(25.04, 121.612);
            currentLocation = this.Location;
            this.currentMarker = map.getCurrentMarker();
            this.map.MouseUp += new MouseButtonEventHandler(setCurrentMarkerPosition);

            //Sop
            FakeSOP sop = new FakeSOP();
            LocationSOPs = sop.getFakeSOP().getLocationSOP();
            setUpSOP();
            
            //detect if user is already in range
            detectCurrentMarkerIsInRange(this.Location);
            
        }

        //temporary classes to store different type of SOP
        //TODO:
        // add this part to the SOP class, so it's more dynamic
        ObservableCollection<SOPTypesAndCommand> sopTypes = new ObservableCollection<SOPTypesAndCommand>();
        public ObservableCollection<SOPTypesAndCommand> SOPTypes
        {
            get
            {
                sopTypes.Add(new SOPTypesAndCommand() { Title = "Rock", Command = this.AddWindow });
                sopTypes.Add(new SOPTypesAndCommand() { Title = "Plantation", Command=this.AddWindow });
                sopTypes.Add(new SOPTypesAndCommand() { Title = "Protected Object", Command = this.AddWindow });
                sopTypes.Add(new SOPTypesAndCommand() { Title = "Slope", Command = this.AddWindow });
                sopTypes.Add(new SOPTypesAndCommand() { Title = "Catchment", Command = this.AddWindow });
                sopTypes.Add(new SOPTypesAndCommand() { Title = "Basic Info", Command = this.AddWindow });
                
                return sopTypes;
            }
        }
        public class SOPTypesAndCommand
        {
            public string Title { get; set; }
            public ICommand Command { get; set; }
            
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

        public int CollectionSize
        {
            get { return this._debrisFlowCollection.Size(); }
        }
        #endregion

        #region Display Properties
        // Variable for showing and hiding the add button 
        Visibility addButtonContent = Visibility.Collapsed;
        public Visibility AddButtonContent
        {
            get { return addButtonContent; }
            set
            {
                if (value != addButtonContent)
                {
                    addButtonContent = value;
                    OnPropertyChanged("AddButtonContent");
                }
            }
        }
        // command to trigger toggling the add button 
        RelayCommand toggleAddButton;
        public ICommand ToggleAddButton
        {
            get
            {
                if(toggleAddButton == null)
                {
                    toggleAddButton = new RelayCommand(
                        param => this.ChangeView());
                }
                return this.toggleAddButton;
            }
        }
        
        //helper to change the visibility of add button content
        void ChangeView()
        {
            AddButtonContent = (AddButtonContent == Visibility.Visible) ? Visibility.Collapsed : Visibility.Visible;
        }

        RelayCommand addWindow;
        public ICommand AddWindow
        {
            get
            {
                if(addWindow == null)
                {
                    addWindow = new RelayCommand(p => CreateWindow(p) );
                 
                }
                return this.addWindow;
            }
        }
        
       

        private async void CreateWindow(object parameter)
        {
            var str = parameter as string;
            this.ChangeView();
            switch (str)
            {
                case "Rock":
                    custom = new CustomDialog() { Title = str };
                    var RockViewModel = new RockViewModel(instance => _dialogCoordinator.HideMetroDialogAsync(this, custom), _debrisFlowCollection, new DebrisFlowRecord.Rock());
                    custom.Content = new test { DataContext = RockViewModel };
                    await _dialogCoordinator.ShowMetroDialogAsync(this, custom);
                    break;
                case "Slope":
                    break;
                case "Plantation":
                    break;
                case "Protected Object":
                    break;
                case "Basic Info":
                    break;
                case "Catchment":
                    break;
            }
            
            
        }
        private CustomDialog custom;
       
        
        #endregion

        #region private helpers
        /// <summary>
        /// this functioned is called whenever user's location has changed
        /// right now the change is detected by the MouseUp trigger,
        /// later it should be added to the GeocoordinateWatcher position change event
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        void setCurrentMarkerPosition(object s, MouseEventArgs e)
        {

            var mouseMapPosition = e.GetPosition(map);
            var mouseGeocode = map.ViewPortToLocation(mouseMapPosition);
            Location loc = new Location(mouseGeocode.Latitude, mouseGeocode.Longitude);
            this.Location = loc;
            
        }
        
        /// <summary>
        /// check if the user has get in range of one of the SOPlocations 
        /// </summary>
        /// <param name="loc"></param>
        private async void detectCurrentMarkerIsInRange(Location loc)
        {
            if (map != null)
            {
                Location new_location = loc;
                map.setCurrentMarkerPosition(new_location);
                currentLocation = new_location;
                if(LocationSOPs.Count == 0)
                {
                    Status = "not in range";
                    return;
                }
                foreach(LocationSOP locSop in LocationSOPs)
                {
                    var sopLoc = locSop.location;
                    double result = RangeLength(this.Location.Latitude, sopLoc.Latitude, this.Location.Longitude, sopLoc.Longitude);
                    Status = result.ToString();
                    if (checkInRange(sopLoc.Latitude, sopLoc.Longitude) == true)
                    {

                        Status = "In range";
                        
                        //pop up the dialog when user's in range
                        var metroWindow = (Application.Current.MainWindow as MetroWindow);
                        await metroWindow.ShowMessageAsync("In this location, the tasks to complete are: \n", locSop.SOPTask + " " + result);
                        return;

                    }
                   
                }
                Status = "not in range";
            }
            
        }
        // helper functions for "detectCurrentMarker
        // Check if Range is less than radius
        private bool checkInRange(double lat, double lon)
        {
             
            double result = RangeLength(this.Location.Latitude, lat, this.Location.Longitude, lon);
            if (result < this.radius)   
                return true;
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
                    map.addSOPPushPin(item.location, label, this.radius/1000);
                }
            }

        }

        #endregion

    }
}
