using MainViewModule.Views;
using PresentationUtility;
using Prism.Commands;
using Prism.Regions;
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
    public class SettingsViewModel : PropertyChangedImplementation
    {
        IRegionManager regionManager;
        ITypesService typesService;
        List<TypesDetailsViewModel> typesViewModels;
        ICommand cancelCommand, typeNameChangedCommand;
        private List<ClothingType> types;
        string version;
        
        public SettingsViewModel(IRegionManager regionManager, ITypesService typesService)
        {
            this.regionManager = regionManager;
            this.typesService = typesService;
            //TypesViewModels = new List<TypesDetailsViewModel>();
            //foreach(var t in typesService.TypesList)
            //{
            //    TypesViewModels.Add(new TypesDetailsViewModel(t));
            //}
            this.typesService.TypesListUpdated += TypesService_TypesListUpdated;
            Version = "Wersja: " + Application.ResourceAssembly.GetName().Version.ToString();
        }

        private void TypesService_TypesListUpdated(object sender, EventArgs e)
        {
            Types = typesService.TypesList;
        }

        private void CancelSettings()
        {
            IRegion region = regionManager.Regions["MainRegion"];
            var view = region.ActiveViews.ToList().First();
            region.Deactivate(view);
            view = region.Views.ToList().Single(x => x.GetType() == typeof(MainView));
            region.Activate(view);
        }

        private void OnTypeNameChanged()
        {
        }

        public ICommand CancelCommand
        {
            get
            {
                if (cancelCommand == null)
                {
                    cancelCommand = new DelegateCommand(CancelSettings);
                }
                return cancelCommand;
            }
        }

        public string Version
        {
            get
            {
                return version;
            }

            private set
            {
                version = value;
            }
        }

        public List<ClothingType> Types
        {
            get
            {
                return types;
            }
            set
            {
                types = value;
                InvokePropertyChanged("Types");
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

        public List<TypesDetailsViewModel> TypesViewModels
        {
            get
            {
                return typesViewModels;
            }

            set
            {
                typesViewModels = value;
            }
        }
    }
}
