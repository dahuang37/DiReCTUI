using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using DiReCTUI.Model;

namespace DiReCTUI.ViewModel
{
    public class RockViewModel : ViewModelBase
    {
        private ICommand _closeCommand;
        private ICommand _save;
        
        DebrisFlowRecord.Rock _rock;
        
        DebrisFlowCollection _dbCollection;
        private Action<RockViewModel> _closeHandler;
        

        public RockViewModel(Action<RockViewModel> closeHandler, DebrisFlowCollection dbCollection, DebrisFlowRecord.Rock Rock)
        {
            _closeHandler = closeHandler;
           
            _rock = Rock;
            

            RockPicture = "Heyhy";
            _dbCollection = dbCollection;

            
        }

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

        private DebrisFlowRecord.Rock.RockTypes _selectedMyEnumType;
        public DebrisFlowRecord.Rock.RockTypes SelectedMyEnumType
        {
            get { return _selectedMyEnumType; }
            set
            {
                _selectedMyEnumType = value;
                OnPropertyChanged("SelectedMyEnumType");
            }
        }

        public IEnumerable<DebrisFlowRecord.Rock.RockTypes> MyEnumTypeValues
        {
            get
            {
                return Enum.GetValues(typeof(DebrisFlowRecord.Rock.RockTypes))
                    .Cast<DebrisFlowRecord.Rock.RockTypes>();
            }
        }

        public string RockNote
        {
            get { return _rock.RockNotes;}
            set
            {
                _rock.RockNotes = value;
                OnPropertyChanged("RockNote");
            }
        }

        public ICommand CloseCommand
        {
            get { if (_closeCommand == null)
                {
                    _closeCommand = new RelayCommand(p => this.Close());
                }
                return _closeCommand;
            }
        }
        void Close()
        {
            this._closeHandler(this);
        }

        RelayCommand save;
        public ICommand Save
        {
            get
            {
                if (save == null)
                {
                    save = new RelayCommand(p => this.SaveObj());
                }
                return save;
            }
        }
        public void SaveObj()
        {
            this._dbCollection.AddRecord(_rock);
            this._closeHandler(this);

        }
    }
}
