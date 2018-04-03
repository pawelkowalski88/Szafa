using CustomEvents;
using PresentationUtility;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using SzafaEntities;
using SzafaInterfaces;

namespace SettingsViewModule.ViewModels
{
    public class TypesDetailsViewModel : PropertyChangedImplementation
    {
        ClothingType type;
        ICommand acceptCommand, cancelCommand, typeNameChangedCommand;
        ITypesService typesService;
        IEventAggregator eventAggregator;
        bool editMode;
        string oldName;


        public TypesDetailsViewModel(ClothingType t, ITypesService typesService, IEventAggregator eventAggregator)
        {
            Type = t;
            oldName = t.Name;
            this.typesService = typesService;
            this.eventAggregator = eventAggregator;
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

        public ICommand TypeNameChangedCommand
        {
            get
            {
                if(typeNameChangedCommand == null)
                {
                    typeNameChangedCommand = new DelegateCommand(OnTypeNameChanged);
                }
                return typeNameChangedCommand;
            }
        }

        private void OnTypeNameChanged()
        {
            if (!EditMode)
            {
                EditMode = true;
            }
        }

        private void OnAccept()
        {
            try
            {
                typesService.UpdateType(Type);
                oldName = Type.Name;
                eventAggregator.GetEvent<DatabaseConnectionRefreshRequestedEvent>().Publish();
            }
            catch (Exception)
            {
                OnCancel();
            }
        }

        private void OnCancel()
        {
            Type.Name = oldName;
            InvokePropertyChanged("Type");
            EditMode = false;
        }

        public bool EditMode
        {
            get
            {
                return editMode;
            }

            set
            {
                editMode = value;
                InvokePropertyChanged("EditMode");
            }
        }
    }
}
