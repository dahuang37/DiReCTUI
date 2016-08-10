using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using DiReCTUI.Model;
using DiReCTUI.Model.Observations;

namespace DiReCTUI.ViewModel
{
    public class RockViewModel : DialogBase
    {
        
        private RelayCommand save;
        private Rock.RockTypes selectedRockType;
        private Rock rock;
        private DebrisFlowCollection debrisFlowCollection;

        // properties from Rock class
        public int RockDiameter
        {
            get { return rock.AverageRockDiameter; }
            set
            {
                rock.AverageRockDiameter = value;
                OnPropertyChanged("RockDiameter");
            }
        }

        public string RockPictureDirection
        {
            get { return rock.RockPictureDirection; }
            set
            {
                rock.RockPictureDirection = value;
                OnPropertyChanged("RockPictureDirection");
            }
        }
        
        // indicates the selected item in the combo box
        public Rock.RockTypes SelectedRockType
        {
            get { return selectedRockType; }
            set
            {
                selectedRockType = value;
                //_rock.RecordedRockTypes.Add(selectedRockType.ToString());
                OnPropertyChanged("SelectedRockType");
            }
        }

        // binds with the combo box to display the values
        public IEnumerable<Rock.RockTypes> RockTypeValues
        {
            get
            {
                return Enum.GetValues(typeof(Rock.RockTypes))
                    .Cast<Rock.RockTypes>();
            }
        }
        // end

        // bind with the save button
        public ICommand Save
        {
            get
            {
                if (save == null)
                {
                    save = new RelayCommand(p => this.SaveObject());
                }
                return save;
            }
        }

        // add the record into the collection and close the dialog
        private void SaveObject()
        {
            this.debrisFlowCollection.AddRecord(rock);
            Close();

        }
        
        // takes the close command, collection, and an instance of rock class
        public RockViewModel(Action<DialogBase> closeHandler, DebrisFlowCollection debrisFlowCollection, Rock rock) : base(closeHandler)
        {
           
            this.rock = rock;
            this.debrisFlowCollection = debrisFlowCollection;
            
        }

       
    }
}
