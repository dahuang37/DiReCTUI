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
    public class DebrisFlowViewModel : ViewModelBase
    {
       
        //these three are supposed to be readonly but then I can't set the value
        DebrisFlowRecord _debrisFlowRecord;
        DebrisFlowCollection _debrisFlowCollection;

        private IDialogCoordinator _dialogCoordinator;
        
        private CustomDialog custom;
        private Visibility addButtonContent = Visibility.Collapsed;
        private RelayCommand toggleAddButton;
        private RelayCommand addWindow;

        private MapController mapController;

        public DebrisFlowViewModel(BingMap map, DebrisFlowRecord debrisFlowRecord, DebrisFlowCollection debrisFlowCollection) 
        {
            this._debrisFlowRecord = debrisFlowRecord;
            this._debrisFlowCollection = debrisFlowCollection;

            this._dialogCoordinator = new DialogCoordinator();
            
            //Sop
            var sop = new FakeSOP();
            //LocationSOPs = sop.GetFakeSOP().GetLocationSOP();
            //SetUpSOP();
            mapController = new MapController(map);

            mapController.LocationChanged += OnLocationChanged;
        }
        
        async void OnLocationChanged(object s, LocationChangedEventArgs e)
        {
            var metroWindow = (Application.Current.MainWindow as MetroWindow);
            await metroWindow.ShowMessageAsync("Title","mesg");

        }

        public int CollectionSize
        {
            get { return this._debrisFlowCollection.Count(); }
        }
        
        
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
        
        //public void SetUpSOP()
        //{
        //    if(this.LocationSOPs != null)
        //    {
        //        foreach(LocationSOP item in LocationSOPs)
        //        {
        //            string label = "Task: "+ item.SOPTask + ", ID: " + item.Id;
        //            map.AddSOPPushPin(item.Location, label, radius/1000);
        //        }
        //    }
        //}

        //temporary classes to store different type of SOP
        //TODO:
        // add this part to the SOP class, so it's more dynamic
        ObservableCollection<SOPTypesAndCommand> sopTypes = new ObservableCollection<SOPTypesAndCommand>();
        public ObservableCollection<SOPTypesAndCommand> SOPTypes
        {
            get
            {
                sopTypes.Add(new SOPTypesAndCommand() { Title = "Rock", Command = this.AddWindow });
                sopTypes.Add(new SOPTypesAndCommand() { Title = "Plantation", Command = this.AddWindow });
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

    }
}
