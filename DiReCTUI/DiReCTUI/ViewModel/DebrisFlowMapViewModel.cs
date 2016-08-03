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
    public class DebrisFlowMapViewModel : ViewModelBase
    {
       
        // these two are supposed to be readonly but then I can't set the value
        DebrisFlowRecord _debrisFlowRecord;
        DebrisFlowCollection _debrisFlowCollection;

        // might be removed to another later
        private IDialogCoordinator _dialogCoordinator;
        private CustomDialog custom;
        private SOP sop;

        private Visibility addButtonContent = Visibility.Collapsed;
        private RelayCommand toggleAddButton;
        private RelayCommand addWindow;
        private MapController mapController;

        public DebrisFlowMapViewModel(MainMap map, DebrisFlowRecord debrisFlowRecord, DebrisFlowCollection debrisFlowCollection) 
        {
            this._debrisFlowRecord = debrisFlowRecord;
            this._debrisFlowCollection = debrisFlowCollection;

            this._dialogCoordinator = new DialogCoordinator();

            //set up map controller
            mapController = new MapController(map);
            mapController.LocationChanged += OnLocationChanged;

            //Set up SOP 
            var debrisflowsop = new DebrisFlowSOP();
            this.sop = debrisflowsop.GetSOP();
            AddSOPTypesAndCommand(debrisflowsop);
            SetUpSOPLocation(sop);
           
        }

        async void OnLocationChanged(object s, LocationChangedEventArgs e)
        {
            var locationSOP = sop.GetLocationSOP();
            foreach (SOP sop in locationSOP)
            {
                if (mapController.LocationIsInRange(sop.Location))
                {
                    custom = new CustomDialog() { Title = "Please Record" };
                    List<string> sopTask = sop.SOPTask;
                    var ReminderViewModel = new DebrisFlowReminderViewModel(instance => _dialogCoordinator.HideMetroDialogAsync(this, custom),
                        sopTask);
                    custom.Content = new DebrisFlowReminderDialog { DataContext = ReminderViewModel };
                    await _dialogCoordinator.ShowMetroDialogAsync(this, custom);
                    return;
                }
            }
        }

        public int CollectionSize
        {
            get { return this._debrisFlowCollection.Count(); }
        }
        
        // Controls the Add Button's content visibility
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
        // Command to toggle the visibility of Add Button's content visibility
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

        // Add dialog for user to input
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
                    var RockViewModel = new RockViewModel(instance => _dialogCoordinator.HideMetroDialogAsync(this, custom), 
                        _debrisFlowCollection, new DebrisFlowRecord.Rock());
                    custom.Content = new DebrisFlowRecordDialog { DataContext = RockViewModel };
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

       
        // hope to move the part below to a new class
        // to control the list of SOP Tasks according to the SOP class
        
        //temporary classes to store different type of SOP
        //TODO:
        // add this part to the SOP class, so it's more dynamic
        ObservableCollection<SOPTypesAndCommand> sopTypes = new ObservableCollection<SOPTypesAndCommand>();
        public ObservableCollection<SOPTypesAndCommand> SOPTypes
        {
            get{ return sopTypes; }
        }
        public class SOPTypesAndCommand
        {
            public string Title { get; set; }
            public ICommand Command { get; set; }
        }
        void AddSOPTypesAndCommand(DebrisFlowSOP sop)
        {
            List<string> titleList = sop.SOPTypes;
            foreach(string title in titleList)
            {
                sopTypes.Add(new SOPTypesAndCommand() { Title = title, Command = AddWindow });
            }
        }

        void SetUpSOPLocation(SOP sop)
        {
            if (mapController != null)
            {
                var locationSOP = sop.GetLocationSOP();
                foreach (SOP s in locationSOP)
                {
                    mapController.AddPushPinWithCircle(s.Location, s.SOPTask);
                }
            }
        }


    }
}
