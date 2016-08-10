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

        public IEnumerable<Rock.RockTypes> RockTypeValues
        {
            get
            {
                return Enum.GetValues(typeof(Rock.RockTypes))
                    .Cast<Rock.RockTypes>();
            }
        }
        
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
        private void SaveObject()
        {
            this.debrisFlowCollection.AddRecord(rock);
            Close();

        }
        
        public RockViewModel(Action<DialogBase> closeHandler, DebrisFlowCollection debrisFlowCollection, Rock rock) : base(closeHandler)
        {
           
            this.rock = rock;
            this.debrisFlowCollection = debrisFlowCollection;
            
        }

       
    }
}
