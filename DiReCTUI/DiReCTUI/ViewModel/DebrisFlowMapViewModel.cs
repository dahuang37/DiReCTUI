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
using DiReCTUI.Model.Observations;

namespace DiReCTUI.ViewModel
{
    public class DebrisFlowMapViewModel : ViewModelBase
    {
       
        private DebrisFlowRecord debrisFlowRecord;
        private DebrisFlowCollection debrisFlowCollection;

        // might be removed to another later
        private IDialogCoordinator dialogCoordinator;
        private CustomDialog custom;

        // these are used to load and display sop
        private SOP sop;
        private SOPDisplay sopDisplay;
        private ObservableCollection<SOPDisplay> sopTypes = new ObservableCollection<SOPDisplay>();
       
        // private display variables
        private Visibility addButtonContent = Visibility.Collapsed;
        private RelayCommand toggleAddButton;
        private RelayCommand addDialog;
        private MapController mapController;

        // Displays the SOP Types to be recorded
        public ObservableCollection<SOPDisplay> SOPTypes
        {
            get { return sopTypes; }
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
                if (toggleAddButton == null)
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
        public ICommand AddDialog
        {
            get
            {
                if (addDialog == null)
                {
                    addDialog = new RelayCommand(p => CreateDialog(p));
                }
                return this.addDialog;
            }
        }
        // Base on what user click, generate the corresonding dialog type
        private async void CreateDialog(object parameter)
        {
            var str = parameter as string;
            this.ChangeView();
            switch (str)
            {
                case "Rock":
                    // initialize a basic dialog and set the title
                    custom = new CustomDialog() { Title = str };
                    // initialize rock view model and pass the close capability, collection, and new object.
                    var RockViewModel = new RockViewModel(instance => dialogCoordinator.HideMetroDialogAsync(this, custom),
                        debrisFlowCollection, new Rock());
                    //set the dialog's content to the rock view page
                    custom.Content = new DebrisFlowRecordDialog { DataContext = RockViewModel };
                    // wait for the dialog to finish
                    await dialogCoordinator.ShowMetroDialogAsync(this, custom);
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
        
        // load / initialize all the variables
        public DebrisFlowMapViewModel(MainMap map, DebrisFlowRecord debrisFlowRecord, DebrisFlowCollection debrisFlowCollection) 
        {
            this.debrisFlowRecord = debrisFlowRecord;
            this.debrisFlowCollection = debrisFlowCollection;

            this.dialogCoordinator = new DialogCoordinator();

            //set up map controller
            mapController = new MapController(map);
            mapController.LocationChanged += OnLocationChanged;

            //Set up SOP 
            var debrisflowsop = new DebrisFlowSOP();
            this.sop = debrisflowsop.GetSOP();
            this.sopDisplay = new SOPDisplay();
            AddSOPTypes(debrisflowsop);
            SetUpSOPLocation(sop);
           
        }

        // Load SOP Titles for the add buttons on the button right
        void AddSOPTypes(DebrisFlowSOP sop)
        {
            List<string> titleList = sop.SOPTypes;
            // takes the list of data types from SOP and add it to the list
            foreach (string title in titleList)
            {
                sopTypes.Add(new SOPDisplay() { Title = title, Command = AddDialog });
            }
        }

        // Set up SOP Locations on the map 
        void SetUpSOPLocation(SOP sop)
        {
            if (mapController != null)
            {
                var locationSOP = sop.GetLocationSOP();
                // takes the defined locations from SOP and add pushpins onto map
                foreach (SOP s in locationSOP)
                {
                    mapController.AddPushPinWithCircle(s.Location, s.SOPTask);
                }
            }
        }

        // when the user's location changed, checked if the new location is in range of any designated point
        async void OnLocationChanged(object s, LocationChangedEventArgs e)
        {
            var locationSOP = sop.GetLocationSOP();
            foreach (SOP sop in locationSOP)
            {
                if (mapController.LocationIsInRange(sop.Location))
                {
                    custom = new CustomDialog() { Title = "Please Record" };
                    List<string> sopTask = sop.SOPTask;
                    var ReminderViewModel = new DebrisFlowReminderViewModel(instance => dialogCoordinator.HideMetroDialogAsync(this, custom),
                        sopTask);
                    custom.Content = new DebrisFlowReminderDialog { DataContext = ReminderViewModel };
                    await dialogCoordinator.ShowMetroDialogAsync(this, custom);
                    return;
                }
            }
        }
        
    }
}
