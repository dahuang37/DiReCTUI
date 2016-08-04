using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DiReCTUI.ViewModel
{
    public class DialogBase : ViewModelBase
    {

        private RelayCommand closeCommand;
        private Action<DialogBase> closeHandler;

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
        protected void Close()
        {
            this.closeHandler(this);
        }

        public DialogBase(Action<DialogBase> closeHandler)
        {
            this.closeHandler = closeHandler;
        }

    }
}
