using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;
using System.ComponentModel;

namespace DiReCTUI.ViewModel 
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        #region Constructor
        //protected ViewModelBase()
        //{

        //}
        #endregion

        #region DisplayName
        ///<summary>
        ///Returns the user-friendly Name
        ///can be override
        /// </summary>
        public virtual string DisplayName { get; protected set; }
        #endregion

        #region Debug Aids
        ///<summary>
        ///Warns the developers when the property is not
        ///included in the specific object
        /// </summary>
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public void VerifyPropertyName(string propertyName)
        {
            //Verify that the property is real, public,instance property on this object
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                string msg = "INvalid property name: " + propertyName;

                if (this.ThrowOnInvalidPropertyName)
                {
                    throw new Exception(msg);
                }else
                {
                    Debug.Fail(msg);
                }
            }
        }

        protected virtual bool ThrowOnInvalidPropertyName { get; private set; }

        #endregion

        #region INotifyPropertyChanged

        ///<summary>
        ///Raised when a property on this object has a new value
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        ///<summary>
        ///Raises this object's PropertyChange Event
        /// </summary>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            this.VerifyPropertyName(propertyName);

            PropertyChangedEventHandler handler = this.PropertyChanged;
            if(handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }

        }
        #endregion

    }
}
