using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using DiReCTUI.Model;

namespace DiReCTUI.ViewModel
{
    public class RockViewModel : DialogBase
    {
        
        private RelayCommand save;
        private DebrisFlowRecord.Rock.RockTypes selectedRockType;
        private DebrisFlowRecord.Rock _rock;
        private DebrisFlowCollection _debrisFlowCollection;

        public int RockDiameter
        {
            get { return _rock.AverageRockDiameter; }
            set
            {
                _rock.AverageRockDiameter = value;
                OnPropertyChanged("RockDiameter");
            }
        }

        public string RockPicture
        {
            get { return _rock.RockPicturePath; }
            set
            {
                _rock.RockPicturePath = value;
                OnPropertyChanged("RockPicture");
            }
        }
        
        public DebrisFlowRecord.Rock.RockTypes SelectedRockType
        {
            get { return selectedRockType; }
            set
            {
                selectedRockType = value;
                //_rock.RecordedRockTypes.Add(selectedRockType.ToString());
                OnPropertyChanged("SelectedRockType");
            }
        }

        public IEnumerable<DebrisFlowRecord.Rock.RockTypes> RockTypeValues
        {
            get
            {
                return Enum.GetValues(typeof(DebrisFlowRecord.Rock.RockTypes))
                    .Cast<DebrisFlowRecord.Rock.RockTypes>();
            }
        }

        public string RockNote
        {
            get { return _rock.RockNotes; }
            set
            {
                _rock.RockNotes = value;
                OnPropertyChanged("RockNote");
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
            this._debrisFlowCollection.AddRecord(_rock);
            Close();

        }
        
        public RockViewModel(Action<DialogBase> closeHandler, DebrisFlowCollection debrisFlowCollection, DebrisFlowRecord.Rock Rock) : base(closeHandler)
        {
           
            _rock = Rock;
            RockPicture = "Heyhy";
            _debrisFlowCollection = debrisFlowCollection;
            
        }

       
    }
}
