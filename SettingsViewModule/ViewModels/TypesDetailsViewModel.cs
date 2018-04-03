using PresentationUtility;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using SzafaEntities;

namespace SettingsViewModule.ViewModels
{
    public class TypesDetailsViewModel : PropertyChangedImplementation
    {
        ClothingType type;
        ICommand acceptCommand, cancelCommand;

        public TypesDetailsViewModel(ClothingType t)
        {
            Type = t;
        }

        public ClothingType Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
            }
        }

        public ICommand AcceptCommand
        {
            get
            {
                if (acceptCommand == null)
                {
                    acceptCommand = new DelegateCommand(OnAccept);
                }
                return acceptCommand;
            }
        }

        public ICommand CancelCommand
        {
            get
            {
                if(cancelCommand == null)
                {
                    cancelCommand = new DelegateCommand(OnCancel);
                }
                return cancelCommand;
            }
        }

        private void OnAccept()
        {
            MessageBox.Show("Accept");
        }

        private void OnCancel()
        {
            MessageBox.Show("Cancel");
        }
    }
}
