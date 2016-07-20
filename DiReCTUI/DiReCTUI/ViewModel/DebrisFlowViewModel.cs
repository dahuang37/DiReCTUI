using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DiReCTUI.Model;
using DiReCTUI.Map;
using DiReCTUI.Controls;
using System.Collections.ObjectModel;

namespace DiReCTUI.ViewModel
{
    public class DebrisFlowViewModel : ViewModelBase
    {
        #region Fields
        readonly DebrisFlowRecord _debrisFlowRecord;
        readonly BackgroundInfo _backgroundInfo;
        private ObservableCollection<DraggablePin> _Pushpins;
        private GPSLocation gps;
        #endregion

        #region Constructor
        public DebrisFlowViewModel()
        {
            this._debrisFlowRecord = new DebrisFlowRecord();
            this.gps = new GPSLocation();
            this._backgroundInfo = new BackgroundInfo();
            this._backgroundInfo.RivuletName = "Test";
            Pushpins = new ObservableCollection<DraggablePin>();
           

        }
        public DebrisFlowViewModel(DebrisFlowRecord dbr, BackgroundInfo bgi)
        {
            this._debrisFlowRecord = dbr;

            this._backgroundInfo = new BackgroundInfo();
            this._backgroundInfo.RivuletName = "Test";

        }
        #endregion
        #region Properties
        public string RivuletName
        {
            get { return "test";  }
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
        public double Latitude
        {
            get
            {
                return gps.Latitude;
            }
            set
            {
                if (value == gps.Latitude) return;
                gps.Latitude = value;
                base.OnPropertyChanged("Latitude");
            }
        }
        public double Longitude
        {
            get
            {
                return gps.Longitude;
            }
            set
            {
                if (value == gps.Latitude) return;
                gps.Longitude = value;
                base.OnPropertyChanged("Longitude");
            }
        }
        #endregion

        #region private helpers

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
