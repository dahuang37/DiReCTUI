using DiReCTUI.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DiReCTUI.ViewModel
{
    public class DebrisFlowReminderViewModel
    {
        private RelayCommand closeCommand;
        private Action<DebrisFlowReminderViewModel> closeHandler;
        private ObservableCollection<DebrisFlowReminderViewModel> recordList = new ObservableCollection<DebrisFlowReminderViewModel>();
        
        public ObservableCollection<DebrisFlowReminderViewModel> RecordList
        {
            get { return recordList; }
        }

        public string Title { get; set; }

        public ICommand CloseCommand
        {
            get
            {
                if (closeCommand == null)
                {
                    closeCommand = new RelayCommand(p => this.Close());
                }
                return closeCommand;
            }
        }

        void Close()
        {
            this.closeHandler(this);
        }

        public DebrisFlowReminderViewModel()
        {
            
        }

        public DebrisFlowReminderViewModel(Action<DebrisFlowReminderViewModel> closeHandler, List<string> sopTask)
        {
            this.closeHandler = closeHandler;
            
            
            foreach(string str in sopTask)
            {
                recordList.Add(new DebrisFlowReminderViewModel() { Title = str });
            }
            

        }

        
       

    }
}
