using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationUtility
{
    public class PropertyChangedImplementation : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void InvokePropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
