using MainViewModule.Views;
using PresentationUtility;
using Prism.Commands;
using Prism.Events;
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
        IEventAggregator eventAggregator;
        List<TypesDetailsViewModel> typesViewModels;
        ICommand cancelCommand;
        private List<ClothingType> types;
        string version;
        
        public SettingsViewModel(IRegionManager regionManager, ITypesService typesService, IEventAggregator eventAggregator)
        {
            this.regionManager = regionManager;
            this.typesService = typesService;
            TypesViewModels = RefreshTypesViewModels();
            this.typesService.TypesListUpdated += TypesService_TypesListUpdated;
            this.eventAggregator = eventAggregator;
            Version = "Wersja: " + Application.ResourceAssembly.GetName().Version.ToString();
        }

        private void TypesService_TypesListUpdated(object sender, EventArgs e)
        {
            Types = typesService.TypesList;
            TypesViewModels = RefreshTypesViewModels();
        }

        private List<TypesDetailsViewModel> RefreshTypesViewModels()
        {
            var output = new List<TypesDetailsViewModel>();
            if (typesService != null)
            {
                if (typesService.TypesList != null)
                {
                    foreach (var t in typesService.TypesList)
                    {
                        output.Add(new TypesDetailsViewModel(t, typesService, eventAggregator));
                    }
                }
            }
            return output;
        }

        private void CancelSettings()
        {
            IRegion region = regionManager.Regions["MainRegion"];
            var view = region.ActiveViews.ToList().First();
            region.Deactivate(view);
            view = region.Views.ToList().Single(x => x.GetType() == typeof(MainView));
            region.Activate(view);
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

        public List<TypesDetailsViewModel> TypesViewModels
        {
            get
            {
                return typesViewModels;
            }

            set
            {
                typesViewModels = value;
                InvokePropertyChanged("TypesViewModels");
            }
        }
    }
}
